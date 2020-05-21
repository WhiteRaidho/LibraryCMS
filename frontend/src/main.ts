import Vue from "vue";
import App from "./App.vue";
import router from "@/controls/router";
import store from "@/store";
import "bootstrap";

//Controls
import '@/controls/store';
import '@/controls/router';
import '@/controls/axios';
import '@/controls/auth';

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
