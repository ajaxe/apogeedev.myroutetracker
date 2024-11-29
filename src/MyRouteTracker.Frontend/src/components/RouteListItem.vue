<style lang="sass" scoped>
.my-card
  margin-top: 5px

  .mode
    font-size: 1.75rem
    opacity: 0.6
</style>
<template>
  <q-card class="my-card" flat bordered>
    <q-card-section horizontal>
      <q-card-section class="q-mr-auto">
        <div class="text-h6">{{ name }}</div>
        <div class="text-subtitle text-muted">
          <q-icon class="mode" :name="modeIcon" />
        </div>
      </q-card-section>

      <q-card-section>
        <q-skeleton width="50px" height="50px" />
      </q-card-section>

      <q-card-actions class="justify-around q-px-md" align="left">
        <q-btn
          flat
          round
          color="red"
          icon="delete_outline"
          @click="deleteRoute"
        />
      </q-card-actions>
    </q-card-section>
  </q-card>
</template>
<script setup>
import { computed } from "vue";
import { useRouteStore } from "src/stores/route-store";
import { Helpers } from "src/stores/constants";

//const { id, userIdentifier, name, mode } | ["id", "userIdentifier", "name", "mode"]
const props = defineProps(["id", "userIdentifier", "name", "mode"]);
const { id, userIdentifier, name, mode } = props;

const routeStore = useRouteStore();

const modeIcon = computed(() => mapIcons());

const deleteRoute = () => {
  routeStore.deleteRoute(id);
};

const mapIcons = () => Helpers.mapModeToIcon(mode);
</script>
