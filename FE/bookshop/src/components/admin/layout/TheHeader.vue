<template>
  <div>
    TheHeader
    <button @click="increment">
      {{ count }}
    </button>
  </div>
</template>
<script setup>
import { ref } from "vue";
const count = ref(0);
function increment() {
  count.value++;
}
</script>
<script></script>
<style scoped>
@import url(../css/header.css);
</style>
