import Vue from 'vue';

export default class AuthService 
{
    /**
     * @param username 
     * @param password 
     * 
     * @returns Promise<TokenModel>
     */
    public static async login(username: string, password: string) : Promise<AuthModel>
    {
        return (await Vue.axios.post<AuthModel>('users/authenticate', {
            Username: username,
            Password: password
        })).data;
    }

    /**
     * @returns Primise<TokenModel>
     */
    public static async refreshToken() : Promise<TokenModel>
    {
        return (await Vue.axios.get<TokenModel>('users/authenticate/refresh')).data;
    }

    /**
     * @param refreshToken 
     * 
     * @returns Promise<TokenModel>
     */
    public static async recoverToken(refreshToken: string) : Promise<TokenModel>
    {
        return (await Vue.axios.post<TokenModel>('users/authenticate/recover', {
            token: refreshToken
        })).data;
    }

    /**
     * @param refreshToken 
     * 
     * @returns Promise<Statement>
     */
    public static async revokeToken(refreshToken: string) : Promise<Statement>
    {
        return (await Vue.axios.post<Statement>('users/authenticate/revoke', {
            token: refreshToken
        })).data;
    }

    public static async getIdentity() : Promise<AuthModel>
    {
        return (await Vue.axios.get<AuthModel>('users/me')).data;
    }

}

export interface TokenModel
{
    token: string;
    refresh: string;
    expires: string;
}

export interface Statement
{
    code: number;
    message: string;
}

export interface AuthModel
{
    userId: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    token: string; //DELETE this
}