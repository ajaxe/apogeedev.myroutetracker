import { defineStore } from "pinia";
import { useSessionStore } from "./session-store";
import { api } from "boot/axios";
import { ApiRoutes, Helpers } from "./constants";

export const useRouteTracker = defineStore("routeTracker", {
  state: () => ({
    /** @type {import('./typedefs').Route} */
    current: null,
    visible: false,
  }),
  getters: {
    showTracker: (state) => state.visible,
    showTrackerAction: (state) => !state.visible,
    modeIcon: (state) =>
      Helpers.mapModeToIcon(state.current?.mode.toLowerCase()),
  },
  actions: {
    async startNew() {
      const sessionStore = useSessionStore();

      if (!sessionStore.isLoggedIn) {
        throw new Error("User not logged-in");
      }

      const response = await api.post(ApiRoutes.routePost());

      this.current = response.data;

      if (this.current) {
        this.visible = true;
      }
      return this.current;
    },
    hideTracker() {
      this.visible = false;
    },
  },
});
