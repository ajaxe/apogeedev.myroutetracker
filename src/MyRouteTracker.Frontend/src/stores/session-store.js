import { defineStore } from "pinia";
import { api } from "boot/axios";
import { ApiRoutes } from "./constants";

export const useSessionStore = defineStore("session", {
  state: () => ({
    /**
     * @type {boolean}
     */
    __isLoggedIn: null,
    email: "",
    displayName: "",
    profilePic: "",
    userIdentifier: "",
    loginUrl: "",
    logoutUrl: "",
    tzOffset: new Date().getTimezoneOffset(),
  }),
  getters: {
    /**
     * Returns the 'true' if user is logged-in otherwise 'false'.
     *
     * @param {*} state
     * @returns {boolean}
     */
    isLoggedIn: (state) => !!state.__isLoggedIn,
    /**
     * Checks if session was checked with the backend.
     *
     * @param {*} state
     * @returns {boolean}
     */
    isSessionChecked: (state) => state.__isLoggedIn !== null,
  },
  actions: {
    async checkSession(forceCheck) {
      if (forceCheck) {
        sessionApi = null;
      }
      sessionApi = sessionApi || api.get(ApiRoutes.session);
      try {
        const response = await sessionApi;
        this.__isLoggedIn = response.data.isLoggedIn;
        this.email = response.data.email;
        this.displayName = response.data.name;
        this.profileImageUrl = response.data.profileImageUrl;
        this.userIdentifier = response.data.userIdentifier;
        this.loginUrl = response.data.loginUrl;
        this.logoutUrl = response.data.logoutUrl;
      } catch (err) {
        this.__isLoggedIn = false;
      }
    },
  },
});

let sessionApi = null;
