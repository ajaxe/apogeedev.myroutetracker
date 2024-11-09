import { defineStore } from "pinia";
import { api } from "boot/axios";

export const useSessionStore = defineStore("session", {
  state: () => ({
    isLoggedIn: false,
    email: "",
    displayName: "",
    profilePic: "",
    userIdentifier: "",
  }),
  getters: {
    doubleCount: (state) => state.counter * 2,
  },
  actions: {
    async checkSession() {
      try {
        const response = await api.get("api/session");
        this.isLoggedIn = true;
        this.email = response.data.email;
        this.displayName = response.data.name;
        this.profilePic = response.data.profileImageUrl;
        this.userIdentifier = response.data.userIdentifier;
      } catch (err) {
        this.isLoggedIn = false;
      }
    },
  },
});
