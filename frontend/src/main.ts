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

import { library } from '@fortawesome/fontawesome-svg-core';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { dom } from '@fortawesome/fontawesome-svg-core';

// library.add(faUserSecret) //HACK Method to add ico to project
library.add(faSearch);
Vue.component('ico', FontAwesomeIcon)
dom.watch();

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
