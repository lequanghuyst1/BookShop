<template>
  <div id="app">
    <the-header></the-header>
    <div class="container">
      <the-siderbar></the-siderbar>
      <the-content></the-content>
    </div>
  </div>
</template>
<script>
import TheContent from "../TheContent.vue";
import TheHeader from "../TheHeader.vue";
import TheSiderbar from "../TheSiderbar.vue";
export default {
  name: "LayoutAdmin",
  components: { TheHeader, TheSiderbar, TheContent },
};
</script>
<style scoped>
.container {
  display: flex;
  height: calc(100vh - var(--header-height-admin));
  background-color: var(--bgc-container-admin);
}
</style>
