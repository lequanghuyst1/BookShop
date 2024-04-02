<template>
  <div id="container-main">
    <TheHeader></TheHeader>
    <div class="container">
      <div class="row">
        <div class="col-3">
          <TheSidebar></TheSidebar>
        </div>
        <div class="col-9">
          <TheContent></TheContent>
        </div>
      </div>
    </div>
    <TheFooter></TheFooter>
  </div>
</template>
<script>
import TheContent from "../TheContent.vue";
import TheFooter from "../TheFooter.vue";
import TheHeader from "../TheHeader.vue";
import TheSidebar from "../TheSidebar.vue";
export default {
  name: "LayoutUser",
  components: { TheHeader, TheContent, TheSidebar, TheFooter },
};
</script>
<style scoped>
#container-main {
  background-color: #f9f5ee;
}
</style>
