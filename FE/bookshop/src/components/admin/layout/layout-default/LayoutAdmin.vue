<template>
  <the-header></the-header>
  <the-content></the-content>
  <the-siderbar></the-siderbar>
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
<style></style>
