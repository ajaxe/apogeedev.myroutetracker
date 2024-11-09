<template>
  <q-layout view="lHr lpR lFr">
    <q-header reveal elevated class="bg-primary text-white" height-hint="98">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar color="teal"> R </q-avatar>
          My Route Tracker
        </q-toolbar-title>
        <UserProfileContainer />
      </q-toolbar>

      <!--<q-tabs align="left">
        <q-route-tab to="/page1" label="Page One" />
        <q-route-tab to="/page2" label="Page Two" />
        <q-route-tab to="/page3" label="Page Three" />
      </q-tabs>-->
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer reveal elevated class="bg-grey-8 text-white">
      <q-toolbar>
        <div class="text-weight-regular">
          <q-icon name="copyright" /> 2024 - My Route Tracker -
          <a href="https://www.apogee-dev.com/privacy/"
            ><span>Privacy</span><q-icon name="open_in_new"
          /></a>
        </div>
      </q-toolbar>
    </q-footer>
  </q-layout>
</template>

<script setup>
import { ref } from "vue";
import UserProfileContainer from "components/UserProfileContainer.vue";

defineOptions({
  name: "MainLayout",
});
</script>
