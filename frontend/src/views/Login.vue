<template>
  <div class="semi-transparent center-absolute-tab">
    <div class="flex-container flex-column">
        <div class="p8">
            <input v-model="username" placeholder="Login" type="text"/>
        </div>
        <div class="p8">
            <input v-model="password" placeholder="Hasło" type="password" />
        </div>
        <button class="button login-button" v-on:click="login()">Zaloguj się</button>
        <label><input type="checkbox" name="remember" id="remember">Zapamiętaj wybór (WIP)</label>
        <div class="p8">Jesteś nowym użytkownikiem? <router-link to="/register">Zarejestruj się</router-link></div> 
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
// import { Prop } from "vue-property-decorator";

@Component({
  components: {}
})
export default class Profile extends Vue {
    private username = "";
    private password = "";
    private remember: boolean;

    async login()
    {
        await this.$auth.login(this.username, this.password, true);
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
