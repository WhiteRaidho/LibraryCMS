<template>
  <div class="semi-transparent center-absolute-tab">
    <div class="flex-container flex-column">
        <div class="p8">
            <input v-model="username" placeholder="Login" />
        </div>
        <div class="p8">
            <input v-model="username" placeholder="Hasło" type="password" />
        </div>
        <button class="button login-button">Zaloguj się</button>
        <div class="p8">Jesteś nowym użytkownikiem? <router-link to="/register">Zarejestruj się</router-link></div> 
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
// import { Prop } from "vue-property-decorator";
@Component({
  components: {}
})
export default class Profile extends Vue {

}
</script>

<style scoped>
.login-button {
    background-color: var(--info-color);
    color: var(--main-white-color);
    margin: 8px;
}
</style>
