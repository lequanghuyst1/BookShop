<template>
  <div class="header">
    <div class="header__logo h-100">
      <div class="header__logo-image h-100 justify-content-center">
        <img src="../../../assets/logo.png" alt="" style=" width: 70%;" />
      </div>
    </div>

    <div class="header__info">
      <div class="header__info-name">
        <h4 class="header__info-company-name">NHÀ SÁCH WANDER</h4>
      </div>
      <div class="header__navbar-list">
        <div
          v-tippy="{ content: 'Thông báo', placement: 'bottom' }"
          class="header__navbar-item"
        >
          <i class="header__navbar-icon fa-regular fa-bell"></i>
        </div>
        <div
          v-tippy="{ content: 'Giúp đỡ', placement: 'bottom' }"
          class="header__navbar-item"
        >
          <i class="header__navbar-icon fa-regular fa-circle-question"></i>
        </div>
        <div
          @click="this.isShowActionUser = !this.isShowActionUser"
          class="header__navbar-item header__navbar-item-user"
        >
          <img
            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRSbpqvNQMR7YBZ9eN_5qUeAzj6Vp-1ljDiuw&usqp=CAU"
            alt="Ảnh"
            class="header__navbar-item-avatar"
          />
          <span class="header__navbar-item-user-name">{{
            user ? user.Fullname : ""
          }}</span>
          <div class="m-icon-arrow-down" style="scale: calc(10 / 14)"></div>
          <ul v-show="isShowActionUser" class="user-action">
            <li class="user-action-item">Thông tin cá nhân</li>
            <li class="user-action-item" @click="onLogout">Đăng xuất</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { removeAllInfoTokenToStorage } from "@/js/token/TokenService";
import userService from "@/utils/UserService";
import localStorageService from "@/js/storage/LocalStorageService";
export default {
  name: "TheHeader",
  created() {
  },
  mounted() {
    document.title = "Admin";
  },
  computed: {
    user: () => {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    /**
     * Thực hiện đăng xuất khi click đăng xuất
     * Author: LQHUY(06/04/2024)
     */
    async onLogout() {
      try {
        var res = await userService.Logout(this.user?.Email);
        if (res.status === 201) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đăng xuất thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          removeAllInfoTokenToStorage();
          this.$router.replace("login").finally(() => {
            document.title = "Đăng nhập"; // Đặt lại tiêu đề sau khi chuyển hướng
          });
          removeAllInfoTokenToStorage();
          localStorageService.removeItemLocalStorage("cart");
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      isShowActionUser: false,
    };
  },
};
</script>
<style scoped>
@import url(../css/header.css);
</style>
