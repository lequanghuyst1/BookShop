<template>
  <button class="m-button--no-text">
    <div style="font-size: 24px; color: #486192" :class="icon"></div>
  </button>
</template>
<script>
export default {
  name: "MButtonNoText",
  props: {
    icon: {
      type: String,
      required: true,
    },
  },
};
</script>
<style scoped>
@import url(./button.css);
</style>
