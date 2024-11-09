<script setup>
import { ref } from "vue";
import { useSessionStore } from "src/stores/session-store";

defineOptions({
  name: "UserProfile",
});

const sessionStore = useSessionStore();

const pic = ref(sessionStore.profilePic);
const name = ref(sessionStore.displayName);
const email = ref(sessionStore.email);
</script>

<template>
  <q-btn round color="white">
    <q-avatar> <img :src="pic" /></q-avatar>
    <q-menu>
      <q-list style="min-width: 100px">
        <q-item v-close-popup class="text-center">
          <q-item-section>
            <q-item-label>{{ name }}</q-item-label>
            <q-item-label caption>{{ email }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-separator />
        <q-item clickable v-close-popup>
          <q-item-section>Tracked routes</q-item-section>
        </q-item>
        <q-separator />
        <q-item clickable v-close-popup>
          <q-item-section>
            <q-btn href="logout?returnUrl=/">Logout</q-btn>
          </q-item-section>
        </q-item>
      </q-list>
    </q-menu>
  </q-btn>
</template>
