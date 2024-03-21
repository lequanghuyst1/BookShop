<template>
  <MDialog>
    <template #body>
      <slot name="form"></slot>
    </template>
    <template #footer>
      <slot name="footer"></slot>
    </template>
  </MDialog>
</template>
<script>
export default {
  name: "MForm",
  emits: ["isCheckData"],
  methods: {
    /**
     * Hàm xử lí thêm mới một đối tượng
     * Author: LQHUY(07/12/2002)
     */
    btnSaveOn() {
      this.$emit("isCheckData", false);
    },

    /**
     * Hàm xử lí nhân bản một đối tượng
     * Author: LQHUY(07/12/2002)
     */
    btnSaveOnAndAdd() {
      this.$emit("isCheckData", true);
    },
  },
};
</script>
<style scoped>
@import url(../dialog/dialog.css);
</style>
