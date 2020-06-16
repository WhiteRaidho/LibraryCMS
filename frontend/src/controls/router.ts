import Vue from "vue";
import Router, { RouteConfig } from "vue-router";
import Home from "@/views/Home.vue";
import { component } from 'vue/types/umd';

Vue.use(Router);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/forbidden",
    name: "Fobidden",
    component: () => import("../views/Forbidden.vue")
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
    path: "/books/:author/:title",
    name: "Book",
    component: () => import("../views/Books/Book.vue")
  },
  {
    path: "/profile",
    name: "Profile",
    component: () => import("../views/Profile.vue"),
    meta: {
      auth: true
    }
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/Login.vue"),
    meta: {
      auth: false
    }
  },
  {
    path: "/Register",
    name: "Register",
    component: () => import("../views/Register.vue"),
    meta: {
      auth: false
    }
  },
  //Biblioteki
  {
    path: "/libraries",
    name: "Libraries",
    component: () => import("../views/Libraries/Libraries.vue")
  },
  //Admin pages
  {
    path: "/admin",
    name: "Admin Page",
    component: () => import("../views/Admin/Main.vue"),
    meta: {
      auth: true,
      adminAccess: true
    },
    children: [
      //Books
      {
        path : "books/new",
        name: "Admin - New Book",
        component: () => import("../views/Admin/Book.vue")
      },
      {
        path : "books/:author/:title",
        name: "Admin - Book",
        component: () => import("../views/Admin/Book.vue")
      },
      {
        path : "books",
        name: "Admin - Books",
        component: () => import("../views/Admin/Books.vue")
      },
      //Borrows
      {
        path : "borrows",
        name: "Admin - Borrows",
        component: () => import("../views/Admin/Borrows.vue")
      },
      //Libraries
      {
        path : "libraries/new",
        name: "Admin - New Library",
        component: () => import("../views/Admin/Library.vue")
      },
      {
        path : "libraries/:id",
        name: "Admin - Library",
        component: () => import("../views/Admin/Library.vue")
      },
      {
        path : "libraries",
        name: "Admin - Libraries",
        component: () => import("../views/Admin/Libraries.vue")
      },
      //Locations
      {
        path: "locations/new",
        name: "Admin - New Location",
        component: () => import("../views/Admin/Location.vue")
      },
      {
        path : "locations/:id",
        name: "Admin - Location",
        component: () => import("../views/Admin/Location.vue")
      },
      {
        path : "locations",
        name: "Admin - Locations",
        component: () => import("../views/Admin/Locations.vue")
      },
      //Librarians
      {
        path : "librarians/new",
        name: "Admin - New Librarian",
        component: () => import("../views/Admin/Librarian.vue")
      },
      {
        path : "librarians/:id",
        name: "Admin - Librarian",
        component: () => import("../views/Admin/Librarian.vue")
      },
      {
        path : "librarians",
        name: "Admin - Librarians",
        component: () => import("../views/Admin/Librarians.vue")
      }
    ]
  }
];

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

Vue.router = router;

export default router;
