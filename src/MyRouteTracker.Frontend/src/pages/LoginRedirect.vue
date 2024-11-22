<template>
  <q-page class="flex flex-center">
    <q-card class="my-card">
      <q-card-section> Redirecting to login ... </q-card-section>
    </q-card>
  </q-page>
</template>
<script setup>
import { useRouter } from "vue-router";
import { useSessionStore } from "src/stores/session-store";
import { RouteNames } from "src/stores/constants";

defineOptions({
  name: "LoginRedirect",
});

const sessionStore = useSessionStore();
const router = useRouter();

if (sessionStore.isLoggedIn) {
  router.push({ name: RouteNames.RouteList });
} else {
  setTimeout(
    () => window.location.replace(`${sessionStore.loginUrl}?returnUrl=/`),
    100
  );
}
</script>
