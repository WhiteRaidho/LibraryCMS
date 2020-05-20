import Vue from "vue";
import App from "./App.vue";
import router from "./controls/router";
import store from "@/store";
import "bootstrap";

//Controls
import "@/controls/store";
import "@/controls/router";
import "@/controls/axios";
import vuetify from "./plugins/vuetify";

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
