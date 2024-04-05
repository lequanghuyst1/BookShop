<template>
  <router-view></router-view>
  <MToast
    v-if="showToastMessage"
    :type="typeToast"
    :message="messageToast"
    :status="statusToast"
  ></MToast>

  <MDialog
    v-if="isShowDialogWarning"
    title="Cảnh báo"
    @onCloseDialog="onHideDialogWaring"
    :message="messageDialog"
    :type="this.$Resource[this.$languageCode].Dialog.Type.Warning"
  >
    <template #footerRight>
      <MButton
        @click="this.isShowDialogWarning = false"
        text="Xác nhận"
      ></MButton>
    </template>
  </MDialog>

  <MLoading v-show="showLoading"></MLoading>
</template>
<script>
export default {
  name: "App",
  created() {
    this.$emitter.on("onShowToastMessage", this.onShowToastMessage);
    this.$emitter.on("onShowDialogWarning", this.onShowDialogWarning);
    this.$emitter.on("toggleShowLoading", this.toggleShowLoading);
    this.$emitter.on("handleApiError", this.handleApiError);
  },
  beforeUnmount() {
    this.$emitter.off("onShowToastMessage");
    this.$emitter.off("onShowDialogWarning");
    this.$emitter.off("toggleShowLoading");
    this.$emitter.off("handleApiError");
  },
  methods: {
    /**
     * Hàm hiện toast message và tự động gỡ bỏ
     * Author: LQHUY (6/12/2023)
     */
    onShowToastMessage(type, message, status) {
      this.showToastMessage = true;
      this.typeToast = type;
      this.messageToast = message;
      this.statusToast = status;
      setTimeout(() => {
        this.showToastMessage = false;
      }, 3000);
    },

    onShowDialogWarning(message) {
      this.isShowDialogWarning = true;
      this.messageDialog = message;
    },

    onHideDialogWaring() {
      this.isShowDialogWarning = false;
    },

    /**
     * Hàm xử lí ẩn hiện loading
     * @param {object} req
     * Author: LQHUY(08/12/2023)
     */
    toggleShowLoading(isShow) {
      this.showLoading = isShow;
    },

    /**
     * Hàm xử lí các lỗi trả về từ API
     * @param {object} req
     * Author: LQHUY(15/12/2023)
     */
    handleApiError(req) {
      try {
        switch (req.response.status) {
          //Lỗi từ người dùng nhập thông tin không hợp lệ
          case 400:
            this.onShowDialogWarning(
              Object.values(req.response.data.errors).join(",")
            );
            break;
          //Lỗi khi tải khoản đăng nhập không đúng
          case 401:
            this.$emitter.emit("toggleShowLoading", false);
            this.$emitter.emit("toggleShowLoadingTable", false);
            this.onShowDialogWarning(
              Object.values(req.response.data.errors).join(",")
            );
            break;
          //Lỗi khi không có quyền truy cập
          case 403:
            this.$emitter.emit("toggleShowLoading", false);
            this.$emitter.emit("toggleShowLoadingTable", false);
            this.onShowDialogWarning(
              Object.values(req.response.data.errors).join(",")
            );
            break;
          //Lỗi khi đường dẫn gọi API lỗi
          case 404:
            this.$emitter.emit("toggleShowLoading", false);
            this.$emitter.emit("toggleShowLoadingTable", false);
            this.onShowDialogWarning(
              Object.values(req.response.data.errors).join(",")
            );
            break;
          //Lỗi từ phía backend
          case 500:
            this.$emitter.emit("toggleShowLoading", false);
            this.$emitter.emit("toggleShowLoadingTable", false);
            this.onShowDialogWarning(
              Object.values(req.response.data.errors).join(",")
            );
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("toggleShowLoadingTable", false);
        this.$emitter.emit(
          "onShowToastMessage",
          this.$Resource[this.$languageCode].ToastMessage.Type.Error,
          this.$Resource[this.$languageCode].MessageHelp,
          this.$Resource[this.$languageCode].ToastMessage.Status.Error
        );
      }
    },
  },
  data() {
    return {
      /**data toast message */
      showToastMessage: false,
      typeToast: "",
      messageToast: "",
      statusToast: "",

      /**data dialog notice */
      isShowDialogWarning: false,
      messageDialog: "",
      showLoading: false,
    };
  },
};
</script>

<style scoped>
@import url(./css/main.css);
</style>
