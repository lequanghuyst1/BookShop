<template>
  <div id="container-main">
    <TheHeader></TheHeader>
    <router-view name="ViewRouterContainer"></router-view>
    <df-messenger
      intent="WELCOME"
      chat-title="Chăm sóc khách hàng"
      agent-id="6c4ef73c-2758-49d4-8582-d469aa2b75d9"
      language-code="vi"
      
    ></df-messenger>

    <TheFooter></TheFooter>
  </div>
</template>
<script>
import TheFooter from "../TheFooter.vue";
import TheHeader from "../TheHeader.vue";
export default {
  name: "LayoutHeaderAndFooter",
  components: { TheHeader, TheFooter },
};
</script>
<style scoped>
#container-main {
  background-color: #f0f0f0;
}
</style>
