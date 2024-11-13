
<template>
  <div>
    <RouteListItem
      v-for="r in routes"
      :key="r.id"
      :id="r.id"
      :name="r.name"
      :mode="r.mode"
    />
  </div>
</template>

<script setup>
import { useRouteStore } from "src/stores/route-store";
import RouteListItem from "./RouteListItem.vue";
import { ref, onMounted } from "vue";
defineOptions({
  name: "RouteList",
});

const routeStore = useRouteStore();
const routes = ref([]);

onMounted(async () => {
  routes.value = await routeStore.fetchRoutes();
});
</script>
