<template>
  <div class="semi-transparent center-absolute-tab">
    <div v-if="this.message" class="warning">{{ message }}</div>
    <form @submit.prevent="onSubmit" class="flex-container flex-column" v-if="!registered">
        <div class="p8">
            <input v-model="form.username" type="text" id="username" placeholder="Login"/>
            <input-error v-bind:errors="err.UserName" />
        </div>
        <div class="p8">
            <input v-model="form.email" type="text" id="email" placeholder="E-mail"/>
            <input-error v-bind:errors="err.Email" />
        </div>
        <div class="p8">
            <input v-model="form.password" type="password" id="password" placeholder="Hasło"/>
            <input-error v-bind:errors="err.Password" />
        </div>
        <div class="p8">
            <input v-model="form.firstName" type="text" id="firstName" placeholder="Imię"/>
            <input-error v-bind:errors="err.FirstName" />
        </div>
        <div class="p8">
            <input v-model="form.lastName" type="text" id="lastName" placeholder="Nazwisko"/>
            <input-error v-bind:errors="err.LastName" />
        </div>
        <div class="p8 font-sm">Klikając Zarejestruj, wyrażasz zgodę na na: Umowę użytkownika, Politykę ochrony prywatności i Zasady korzystania z plików cookie. </div>
        <div class="p8">
            <input  type="submit" value="Zarejestruj" class="register-button" />
        </div> 
        <!-- <div class="p8">
            <input v-model="username" placeholder="Login" type="text"/>
        </div>
        <div class="p8">
            <input v-model="password" placeholder="Hasło" type="password" />
        </div>
        <button class="button login-button" v-on:click="login()">Zaloguj się</button>
        <label><input type="checkbox" name="remember" id="remember">Zapamiętaj wybór (WIP)</label> -->
    </form>
    <div v-else class="font-2x">
        <div class="p16">
            Dziękujemy za rejestrację, teraz możesz przejść do logowania.
        </div>
        <div>
            <router-link to="/login">
                <button class="register-button">Zaloguj się</button>
            </router-link>
        </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import AuthService, { RegisterModel } from "@/services/AuthService";
// import { Prop } from "vue-property-decorator";
import InputError from "@/components/InputError.vue";

@Component({
  components: {
      InputError
  }
})
export default class Register extends Vue {

    private registered = false;

    private form: RegisterModel = {
        username: "",
        email: "",
        password: "",
        firstName: "",
        lastName: ""
    };

    private err: any;

    private message = "";

    created()
    {
        this.err = {
            UserName: [],
            Email: [],
            Password: [],
            FirstName: [],
            LastName: []
        };
    }

    async onSubmit()
    {
        try {
            const response = await AuthService.register(this.form);
            this.registered = true;
            // this.$forceUpdate();
            
        } catch (ex) {
            this.err = ex.errors;
            this.message = ex.message;
            this.$forceUpdate();
        }
    }
}
</script>

<style scoped>
.register-button {
    background-color: var(--info-color);
    color: var(--main-white-color);
    width: 100%;
}

.input-data {
    padding-bottom: 0px;
}
</style>
