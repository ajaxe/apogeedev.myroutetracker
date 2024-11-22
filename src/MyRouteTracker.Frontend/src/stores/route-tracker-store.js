import { defineStore } from "pinia";
export const useRouteTracker = defineStore("routeTracker", {
  state: () => ({
    current: {},
    visible: false,
  }),
  getters: {
    showTracker: (state) => state.visible,
    showTrackerAction: (state) => !state.visible,
  },
  actions: {
    startNew() {
      this.visible = true;
    },
    hideTracker() {
      this.visible = false;
    },
  },
});
