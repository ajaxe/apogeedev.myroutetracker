<template>
  <q-page>
    <div class="row">
      <div
        class="col-12 col-md-8 offset-md-2 col-lg-6 offset-lg-3 q-px-sm q-px-md-none"
      >
        <div class="row">
          <div class="col-12">
            <h4 class="q-mt-lg q-mb-sm">Routes</h4>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <RouteListActions class="q-my-md" />
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <RouteList />
          </div>
        </div>
      </div>
    </div>
  </q-page>
</template>
<script setup lang="js">

import RouteListActions from "src/components/RouteListActions.vue";
import RouteList from "src/components/RouteList.vue";

defineOptions({
  name: "RouteListPage",
});

const columnDefinition = [{
  name: 'rname'
}]
</script>
