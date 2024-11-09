<template>
  <UserProfile v-if="isLoggedIn" />
  <div v-else>
    <q-btn href="login?returnUrl=/">Login</q-btn>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useSessionStore } from "src/stores/session-store";
import UserProfile from "./UserProfile.vue";

defineOptions({
  name: "UserProfileContainer",
});

var sessionStore = useSessionStore();
const isLoggedIn = ref(false);

onMounted(async () => {
  await sessionStore.checkSession();
  isLoggedIn.value = sessionStore.isLoggedIn;
});
</script>
