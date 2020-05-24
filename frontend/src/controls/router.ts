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
  //Ksiazki
  {
    path: "/books",
    name: "Books",
    component: () => import("../views/Books/Books.vue")
  },
  {
    path: "/library/:libraryId/books",
    name: "LibraryBooks",
    component: () => import("../views/Books/BooksLibraryList.vue")
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
  },
  {
    path: "/Register",
    name: "Register",
    component: () => import("../views/Register.vue")
  },
  //Biblioteki
  {
    path: "/libraries",
    name: "Libraries",
    component: () => import("../views/Libraries/Libraries.vue")
  }
];

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

Vue.router = router;

export default router;
