import { defineStore } from "pinia";
import { useSessionStore } from "./session-store";
import { useRouteStore } from "./route-store";
import { api } from "boot/axios";
import { ApiRoutes, Helpers } from "./constants";

export const useRouteTracker = defineStore("routeTracker", {
  state: () => ({
    /** @type {import('./typedefs').Route} */
    current: null,
    visible: false,
    paused: false,
    wakeLock: null,
  }),
  getters: {
    showTracker: (state) => state.visible,
    showTrackerAction: (state) => !state.visible,
    modeIcon: (state) => Helpers.mapModeToIcon(state.current?.mode),
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
        const routeStore = useRouteStore();
        routeStore.routes.push(this.current);

        this.wakeLock = await requestWakeLock();
        this.wakeLock = null;
      }
      return this.current;
    },

    async hideTracker() {
      this.visible = false;
      await this.toggleWakeLock(false);
    },

    async toggleWakeLock(createWakeLock) {
      if (createWakeLock && !this.wakeLock) {
        this.wakeLock = await requestWakeLock();
      } else {
        await releaseWakeLock(this.wakeLock);
        this.wakeLock = null;
      }
    },
  },
});

const requestWakeLock = async () => {
  if ("wakeLock" in navigator) {
    console.log("acquiring wake lock.");
    return await navigator.wakeLock.request("screen");
  }
  console.log("wake lock not available.");
  return null;
};

/**
 *
 * @param {WakeLockSentinel} wakeLock
 */
const releaseWakeLock = async (wakeLock) => {
  if (!!wakeLock) {
    console.log("releasing wake lock.");
    await wakeLock.release();
  } else {
    console.log("null wake lock.");
  }
};
