import { RouteNames } from "src/stores/constants";

const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "", component: () => import("pages/AboutPage.vue") },
      {
        path: "about",
        name: RouteNames.About,
        component: () => import("pages/AboutPage.vue"),
      },
      {
        path: "login/redirect",
        name: RouteNames.Login,
        component: () => import("src/pages/LoginRedirect.vue"),
      },
      {
        path: "route",
        name: RouteNames.RouteList,
        component: () => import("pages/RouteListPage.vue"),
        children: [
          {
            path: "list",
            name: RouteNames.RouteList,
            component: () => import("pages/RouteListPage.vue"),
          },
        ],
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
];

export default routes;
