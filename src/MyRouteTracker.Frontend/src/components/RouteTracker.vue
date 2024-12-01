<template>
  <q-page-sticky position="bottom" expand v-if="isLoggedIn && showTracker">
    <div class="col-12 col-md-8 col-lg-6 q-px-sm q-px-md-none">
      <q-card class="my-card" flat bordered>
        <q-card-section horizontal>
          <q-card-actions class="justify-around q-px-md" align="right">
            <q-btn
              :outline="screenLockActive"
              round
              :color="screenLockColor"
              icon="screen_lock_portrait"
              @click="toggleScreenLock"
            >
            </q-btn>
          </q-card-actions>
          <q-card-section class="q-mr-auto">
            <div class="text-h6">{{ currentName }}</div>
            <div class="text-subtitle text-muted">
              <q-icon class="mode" :name="modeIcon" />
            </div>
          </q-card-section>

          <q-card-actions class="justify-around q-px-md" align="left">
            <q-btn
              flat
              round
              icon="play_arrow"
              v-if="paused"
              @click="continueTracking"
            />
            <q-btn
              flat
              round
              icon="pause"
              v-if="!paused"
              @click="pauseTracking"
            />
            <q-btn flat round icon="close" @click="close" />
          </q-card-actions>
        </q-card-section>
      </q-card>
    </div>
  </q-page-sticky>
</template>
<script setup>
import { computed, ref } from "vue";
import { useSessionStore } from "src/stores/session-store";
import { useRouteTracker } from "src/stores/route-tracker-store";

defineOptions({
  name: "RouteTracker",
});

const sessionStore = useSessionStore();
const trackerStore = useRouteTracker();

const isLoggedIn = computed(() => !!sessionStore.isLoggedIn);
const showTracker = computed(() => trackerStore.showTracker);
const paused = computed(() => trackerStore.paused);
const screenLockActive = ref(true);

const currentName = computed(() => trackerStore.current.name);
const modeIcon = computed(() => trackerStore.modeIcon);
const screenLockColor = computed(() =>
  screenLockActive.value ? "secondary" : ""
);

const close = () => trackerStore.hideTracker();

const continueTracking = () => (trackerStore.paused = false);
const pauseTracking = () => (trackerStore.paused = true);
const toggleScreenLock = async () => {
  screenLockActive.value = !screenLockActive.value;
  await trackerStore.toggleWakeLock(screenLockActive.value);
};
</script>
