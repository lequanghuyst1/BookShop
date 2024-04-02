<template>
    <the-header></the-header>
    <div class="container-fulid d-flex">
      <the-siderbar></the-siderbar>
      <the-content></the-content>
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
.container-fulid {
  height: calc(100vh - var(--header-height-admin));
  background-color: var(--bgc-container-admin);
}
</style>
