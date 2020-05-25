import Vue from 'vue';
import { AuthPlugin } from '@/helpers/Auth';
import store from '@/store';

Vue.use(AuthPlugin, {
    store: store,
    routes: {
        homePage: '/',
        loginPage: '/login'
        //HACK add pages here if you want to route from Auth 
    }
});