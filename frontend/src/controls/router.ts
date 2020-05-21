import Vue from "vue";
import Router, { RouteConfig } from "vue-router";
import Home from "@/views/Home.vue";

Vue.use(Router);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/about",
    name: "About",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: "/books",
    name: "Books",
    component: () => import("../views/Books.vue")
  },
  {
    path: "/profile",
    name: "Profile",
    component: () => import("../views/Profile.vue")
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/Login.vue")
  }
];

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

Vue.router = router;

export default router;
