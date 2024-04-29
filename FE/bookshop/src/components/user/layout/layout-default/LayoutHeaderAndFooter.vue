<template>
    <div id="container-main">
      <TheHeader></TheHeader>
        <router-view name="ViewRouterContainer"></router-view>
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
  