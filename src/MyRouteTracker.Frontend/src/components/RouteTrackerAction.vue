<template>
  <q-page-sticky
    position="bottom-right"
    :offset="[18, 18]"
    v-if="isLoggedIn && showAction"
  >
    <q-btn fab icon="track_changes" color="secondary" ripple @click="show" />
  </q-page-sticky>
</template>
<script setup>
import { computed } from "vue";
import { useSessionStore } from "src/stores/session-store";
import { useRouteTracker } from "src/stores/route-tracker-store";

defineOptions({
  name: "RouteTrackerAction",
});
const sessionStore = useSessionStore();
const trackerStore = useRouteTracker();

const isLoggedIn = computed(() => !!sessionStore.isLoggedIn);
const showAction = computed(() => trackerStore.showTrackerAction);

const show = () => trackerStore.startNew();
</script>
