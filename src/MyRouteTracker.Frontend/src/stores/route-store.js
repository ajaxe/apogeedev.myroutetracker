import { defineStore } from "pinia";
import { useSessionStore } from "./session-store";
import { api } from "boot/axios";
import { ApiRoutes } from "./constants";
export const useRouteStore = defineStore("routes", {
  state: () => ({
    /** @type {import('./typedefs').RouteList} */
    routes: [],
    currentPage: 0,
    pageSize: 10,
    selected: null,
  }),
  actions: {
    async fetchRoutes() {
      const sessionStore = useSessionStore();

      if (!sessionStore.isLoggedIn) {
        throw new Error("User not logged-in");
      }

      const response = await api.get(
        ApiRoutes.routeList(sessionStore.tzOffset)
      );
      this.routes = response.data.routes;

      console.log(`route count: ${this.routes.length}`);

      return this.routes;
    },
  },
});
