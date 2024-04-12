<template>
  <div class="sidebar-account">
    <ul class="sidebar-account-list">
      <li
        v-for="(item, index) in menu"
        :key="index"
        class="sidebar-account-item"
      >
        <router-link
          :to="item.path"
          class="sidebar-account-item-link"
        >
          <div class="sidebar-account-item-icon">
            <i :class="item.icon"></i>
          </div>
          <p class="sidebar-account-item-title">
            {{ item.title }}
          </p>
        </router-link>
      </li>
      <li @click="onLogout" class="sidebar-account-item">
        <a class="sidebar-account-item-link">
          <div
            class="sidebar-account-item-icon"
            style="transform: rotate(180deg)"
          >
            <i class="fa-solid fa-arrow-right-from-bracket"></i>
          </div>
          <p class="sidebar-account-item-title">Thoát tài khoản</p>
        </a>
      </li>
    </ul>
  </div>
</template>
<script>
import sidebarAccount from "../../../js/resource/sidebar-account";
import userService from "@/utils/UserService";
import { removeAllInfoTokenToStorage } from "@/js/token/TokenService";
import localStorageService from "@/js/storage/LocalStorageService";
export default {
  name: "SidebarAccount",
  mounted() {},

  computed: {
    user: () => {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    // goToPage(item, index) {
    //   location.href = item.href;
    //   this.activeSidebar = sidebarAccount.map(() => false);
    //   setTimeout(() => {
    //     this.activeSidebar[index] = true;
    //   }, 2000);
    // },
    /**
     * Thực hiện đăng xuất khi click đăng xuất
     * Author: LQHUY(06/04/2024)
     */
    async onLogout() {
      try {
        var res = await userService.Logout(this.user?.Email);
        if (res.status === 201) {
          removeAllInfoTokenToStorage();
          this.$router.push("/").finally(() => {
            location.reload();
            this.$emitter.emit(
              "onShowToastMessage",
              this.$Resource[this.$languageCode].ToastMessage.Type.Success,
              "Đăng xuất thành công",
              this.$Resource[this.$languageCode].ToastMessage.Status.Success
            );
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      menu: sidebarAccount,
    };
  },
};
</script>
<style scoped>
.sidebar-account {
  background-color: #fff;
  border-radius: 8px;
}
.sidebar-account .sidebar-account-list {
  padding: 10px !important;
}
.sidebar-account .sidebar-account-list .sidebar-account-item {
  list-style: none;
  margin-bottom: 8px;
  cursor: pointer;
}
.sidebar-account-list .sidebar-account-item a {
  display: flex;
  align-items: center;
  color: #686868;
  padding: 5px 16px;

  font-size: 15px;
  column-gap: 12px;
}
.sidebar-account-list .sidebar-account-item-icon {
  font-size: 18px;
}

.sidebar-account-list .sidebar-account-item a.router-link-active {
  border: 1px solid #c92127;
  border-radius: 8px;
  background-color: #fee;
  color: #c92127;
}
</style>
