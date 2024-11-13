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
        <q-btn flat round color="red" icon="delete_outline" />
      </q-card-actions>
    </q-card-section>
  </q-card>
</template>
<script setup>
import { computed } from "vue";

//const { id, userIdentifier, name, mode } | ["id", "userIdentifier", "name", "mode"]
const props = defineProps(["id", "userIdentifier", "name", "mode"]);
const { id, userIdentifier, name, mode } = props;

const modeIcon = computed(() => mapIcons());

const mapIcons = () => {
  switch (mode) {
    case "Walk":
      return "directions_walk";
    default:
      return "info";
  }
};
</script>
