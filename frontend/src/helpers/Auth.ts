import Vue, { PluginObject } from "vue";
import { Location, Route } from "vue-router";
import { AxiosInstance } from "axios";
import { Store } from "vuex";
import AuthService, { AuthModel, TokenModel } from "@/services/AuthService";

interface AuthState {
  loaded: boolean;
  authenticated: boolean;
  identity: AuthModel;
}

interface Auth {
  login(username: string, password: string, rememberMe?: boolean): void;
  logout(): void;
  token(): string;
  fetch(): void;
  user(): AuthModel;
  check(): boolean;
  ready(): boolean;
}

interface AuthRedirect {
  homePage: Location;
  loginPage: Location;
}

export class AuthOptions {
  public store: Store<any>;
  public routes: AuthRedirect;
}

export class AuthHelper implements Auth {
  private axios: AxiosInstance;
  private router: any;
  private store: Store<any>;
  private options: AuthOptions;

  public constructor(axios: AxiosInstance, router: any, options: AuthOptions) {
    this.axios = axios;
    this.router = router;
    this.store = options.store;
    this.options = options;

    this.initStore();
    this.initAxios();
    this.initRouter();
    this.initSession();
  }

  private initStore(): void {
    this.store.registerModule(
      "auth",
      {
        namespaced: true,
        state: {
          loaded: false,
          authenticated: false,
          identity: null as any
        },
        mutations: {
          setLoaded(state, value: boolean) {
            state.loaded = value;
          },
          setAuthenticated(state, value: boolean) {
            state.authenticated = value;
          },
          setIdentity(state, identity: AuthModel) {
            state.identity = identity;
          },
          clearIdentity(state) {
            state.identity = null;
          }
        }
      },
      {
        preserveState: false
      }
    );

    const tokens: TokenModel = {
      token: this.accessToken,
      refresh: this.refreshToken,
      expires: ""
    };

    this.setTokens(tokens, tokens.refresh.length > 0);
  }

  private initAxios(): void {
    this.axios.interceptors.request.use(request => {
      request.headers.Authorization = `Bearer ${this.accessToken}`;

      return request;
    });
  }

  private initRouter(): void {
    this.router.beforeEach(async (to: Route, from: Route, next: any) => {
      if (this.state.loaded === false) {
        // Uzyskanie nowego access tokena za pomocą refresh tokena (opcja "remember me")
        if (this.accessToken.length === 0 && this.refreshToken.length > 0) {
          try {
            const result = await AuthService.recoverToken(this.refreshToken);
            this.setTokens(result, true);
          } catch (ex) {
            this.clearTokens();
          }
        }

        // Pobranie danych użytkownika za pomocą access tokena
        if (this.accessToken.length > 0) {
          await this.fetch();
        }

        this.store.commit("auth/setLoaded", true);
      }

      next();
    });
  }

  private initSession(): void {
    const sessionLifetime = 60; // minuty
    const activityInterval = 5; // sekundy

    document.body.addEventListener(
      "click",
      () => (this.lastActivity = Date.now())
    );

    setInterval(() => {
      // Z włączoną opcją "remember me" sesja nigdy nie wygasa
      if (this.refreshToken.length === 0) {
        const timeLeft =
          this.lastActivity + sessionLifetime * 60 * 1000 - Date.now();

        if (timeLeft < 0 && this.check()) {
          this.logout();
        }
      }
    }, activityInterval * 1000);
  }

  private get lastActivity(): number {
    return Number(sessionStorage.getItem("last-activity") || 0);
  }

  private set lastActivity(value: number) {
    sessionStorage.setItem("last-activity", value.toString());
  }

  private get state(): AuthState {
    return this.store.state.auth as AuthState;
  }

  private get accessToken(): string {
    return sessionStorage.getItem("access-token") || "";
  }

  private set accessToken(token: string) {
    sessionStorage.setItem("access-token", token);
  }

  private get refreshToken(): string {
    return localStorage.getItem("refresh-token") || "";
  }

  private set refreshToken(token: string) {
    localStorage.setItem("refresh-token", token);
  }

  private setTokens(tokens: TokenModel, rememberMe: boolean): void {
    if (rememberMe === true) {
      this.refreshToken = tokens.refresh;
    }

    if (tokens.token.length > 0) {
      this.accessToken = tokens.token;

      const base64 = tokens.token
        .split(".")[1]
        .replace(/-/g, "+")
        .replace(/_/g, "/");
      const payload = JSON.parse(window.atob(base64));
      const delay = Number(payload.exp) * 1000 - new Date().getTime();

      if (delay > 0) {
        setTimeout(async () => {
          try {
            const result = rememberMe
              ? await AuthService.recoverToken(this.refreshToken)
              : await AuthService.refreshToken();

            this.setTokens(result, rememberMe);
          } catch (ex) {
            this.logout();
          }
        }, delay);
      }
    }
  }

  private clearTokens() {
    sessionStorage.removeItem("access-token");
    localStorage.removeItem("refresh-token");
  }

  private clearTimer() {
    sessionStorage.removeItem("last-activity");
  }

  public async login(
    username: string,
    password: string,
    rememberMe = false
  ): Promise<void> {
    try {
      const result = await AuthService.login(username, password);
      this.setTokens(result, rememberMe);
      await this.fetch();

      this.router.push(this.options.routes.homePage);
    } catch (ex) {
      this.logout();
      throw ex;
    }
  }

  public logout(): void {
    this.store.commit("auth/setAuthenticated", false);
    this.store.commit("auth/clearIdentity");
    this.clearTokens();
    this.clearTimer();

    this.router.push(this.options.routes.loginPage);
  }

  public token(): string {
    return this.accessToken;
  }

  public async fetch(): Promise<void> {
    try {
      const result = await AuthService.getIdentity();
      this.store.commit("auth/setIdentity", result);
      this.store.commit("auth/setAuthenticated", true);
    } catch (ex) {
      this.store.commit("auth/clearIdentity");
      this.store.commit("auth/setAuthenticated", false);
    }
  }

  public user(): AuthModel {
    return this.state.identity;
  }

  public check(): boolean {
    return this.state.authenticated;
  }

  public ready(): boolean {
    return this.state.loaded;
  }
}

export const AuthPlugin: PluginObject<AuthOptions> = {
  install(Vue, options) {
    console.log(Vue.router);
    if (!Vue.axios) {
      throw new Error("Vue.axios must be set.");
    }
    if (!Vue.router) {
      throw new Error("Vue.router must be set.");
    }
    if (!options || !options.store) {
      throw new Error("AuthOptions.store must be set.");
    }

    Vue.auth = Vue.prototype.$auth = new AuthHelper(
      Vue.axios,
      Vue.router,
      options
    );
  }
};

declare module "vue/types/vue" {
  interface Vue {
    $auth: Auth;
  }
  interface VueConstructor {
    auth: Auth;
  }
}
