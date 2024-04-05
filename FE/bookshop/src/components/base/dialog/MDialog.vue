<template>
  <div class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container">
      <div class="m-dialog__header">
        <slot name="header"></slot>
        <h3 class="m-dialog__header-title">{{ title }}</h3>

        <div class="m-dialog__header-action">
          <div
            v-tippy="{
              content: 'Thoát',
              placement: 'bottom',
            }"
            class="m-dialog__header-close"
            @click="onCloseDialog"
          >
            <i class="fa-solid fa-xmark"></i>
          </div>
        </div>
      </div>
      <div class="m-dialog__content">
        <div class="m-dialog__content-icon" :class="className[type]">
          <i :class="icons[type]"></i>
        </div>
        <p class="m-dialog__content-message">
          {{ message }}
        </p>
      </div>
      <div class="m-dialog__footer">
        <div class="m-dialog__footer-left">
          <slot name="footerLeft"></slot>
        </div>
        <div class="m-dialog__footer-right">
          <slot name="footerRight"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MDialog",
  props: {
    title: {
      type: String,
      required: true,
    },

    message: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      required: true,
    },
  },
  emits: ["onCloseDialog"],
  data() {
    return {
      icons: {
        question: "fa-solid fa-circle-question",
        warning: "fa-solid fa-triangle-exclamation",
        info: "fa-solid fa-circle-info",
      },
      className: {
        question: "m-dialog__content-icon--question",
        warning: "m-dialog__content-icon--warning",
        info: "m-dialog__content-icon--info",
      },
    };
  },
  methods: {
    /**
     * Hàm ẩn form hoặc dialog
     * Author: LQHUY (06/12/2023)
     */
    onCloseDialog() {
      this.$emit("onCloseDialog");
    },
  },
};
</script>
<style scoped>
@import url(./dialog.css);
</style>
