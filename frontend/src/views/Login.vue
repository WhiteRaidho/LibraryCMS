<template>
  <div class="semi-transparent center-absolute-tab">
    <div v-if="this.message" class="warning">{{ message }}</div>
    <form @submit.prevent="onSubmit" class="flex-container flex-column">
        <div class="p8">
            <input v-model="username" placeholder="Login" type="text"/>
        </div>
        <div class="p8">
            <input v-model="password" placeholder="Hasło" type="password" />
        </div>
        <div>
          <input type="submit" class="login-button" value="Zaloguj się" />
        </div>
        <label><input type="checkbox" name="remember" v-model="remember"/>Zapamiętaj wybór</label>
        <div class="p8">Jesteś nowym użytkownikiem? <router-link to="/register">Zarejestruj się</router-link></div> 
  </form>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
// import { Prop } from "vue-property-decorator";

@Component({
  components: {}
})
export default class Login extends Vue {
    private username = "";
    private password = "";
    private remember = false;

    private message = "";

    async onSubmit()
    {
      try {
        let redirect = "";
        if(this.$route.query.redirect)
        {
          redirect = this.$route.query.redirect.toString();
        }
        await this.$auth.login(this.username, this.password, this.remember, redirect);
      } catch (ex) {
        this.message = "Nie udało się zalgować na twoje konto.";
      }
    }
}
</script>

<style scoped>
.login-button {
    background-color: var(--info-color);
    color: var(--main-white-color);
    margin: 8px;
}
</style>
