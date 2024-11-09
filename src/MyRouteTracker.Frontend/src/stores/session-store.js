import { defineStore } from "pinia";
import { api } from "boot/axios";

export const useSessionStore = defineStore("session", {
  state: () => ({
    isLoggedIn: false,
    email: "",
    displayName: "",
    profilePic: "",
    userIdentifier: "",
    loginUrl: "",
    logoutUrl: "",
  }),
  getters: {
    doubleCount: (state) => state.counter * 2,
  },
  actions: {
    async checkSession() {
      try {
        const response = await api.get("api/session");
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
