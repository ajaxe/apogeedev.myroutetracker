<template>
  <q-page-sticky position="bottom" expand>
    <div class="col-12 col-md-8 col-lg-6 q-px-sm q-px-md-none">
      <q-card class="my-card" flat bordered>
        <q-card-section horizontal>
          <q-card-actions class="justify-around q-px-md" align="right">
            <q-btn flat round color="secondary" icon="screen_lock_portrait">
            </q-btn>
          </q-card-actions>
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
            <q-btn flat round icon="play_arrow" />
            <q-btn flat round icon="pause" />
            <q-btn flat round icon="close" />
          </q-card-actions>
        </q-card-section>
      </q-card>
    </div>
  </q-page-sticky>
</template>
<script setup>
defineOptions({
  name: "RouteTracker",
});
const name = "Test";
const modeIcon = "walk";
</script>
