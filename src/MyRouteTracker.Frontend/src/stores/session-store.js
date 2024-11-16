import { defineStore } from "pinia";
import { api } from "boot/axios";
import { ApiRoutes } from "./constants";

export const useSessionStore = defineStore("session", {
  state: () => ({
    /**
     * @type {boolean}
     */
    isLoggedIn: null,
    email: "",
    displayName: "",
    profilePic: "",
    userIdentifier: "",
    loginUrl: "",
    logoutUrl: "",
    tzOffset: new Date().getTimezoneOffset(),
  }),
  getters: {
    doubleCount: (state) => state.counter * 2,
  },
  actions: {
    async checkSession(forceCheck) {
      if (forceCheck) {
        sessionApi = null;
      }
      sessionApi = sessionApi || api.get(ApiRoutes.session);
      try {
        const response = await sessionApi;
        this.isLoggedIn = response.data.isLoggedIn;
        this.email = response.data.email;
        this.displayName = response.data.name;
        this.profileImageUrl = response.data.profileImageUrl;
        this.userIdentifier = response.data.userIdentifier;
        this.loginUrl = response.data.loginUrl;
        this.logoutUrl = response.data.logoutUrl;
      } catch (err) {
        this.isLoggedIn = false;
      }
    },
  },
});

let sessionApi = null;
