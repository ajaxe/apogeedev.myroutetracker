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

      sessionStore.validateSession();

      const response = await api.get(
        ApiRoutes.routeList(sessionStore.tzOffset)
      );

      this.routes = response.data.routes;

      console.log(`route count: ${this.routes.length}`);

      return this.routes;
    },

    async deleteRoute(trackerId) {
      if (!trackerId) {
        return;
      }
      const sessionStore = useSessionStore();
      sessionStore.validateSession();

      await api.delete(ApiRoutes.routeDelete(trackerId));

      const index = this.routes.findIndex((r) => r.id === trackerId);
      this.routes.splice(index, 1);
    },
  },
});
