<template>
  <UserProfile v-if="isLoggedIn" />
  <div v-else>
    <q-btn @click="redirectToLogin">Login</q-btn>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useSessionStore } from "src/stores/session-store";
import UserProfile from "./UserProfile.vue";
import { useRouter } from "vue-router";
import { RouteNames } from "src/stores/constants";

defineOptions({
  name: "UserProfileContainer",
});

const router = useRouter();
const sessionStore = useSessionStore();
const isLoggedIn = ref(false);

const redirectToLogin = () => {
  router.push({ name: RouteNames.Login });
};

onMounted(async () => {
  await sessionStore.checkSession();
  isLoggedIn.value = sessionStore.isLoggedIn;
});
</script>
