<template>
  <div class="topbar">
    <div class="container">
      <div class="topbar-wrap d-lg-flex justify-content-between">
        <div class="header__contact d-flex align-items-center">
          <div class="header-contact d-lg-flex flex-grow-1">
            <div class="item d-flex align-items-center">
              <div class="icon me-2">
                <i class="fa-solid fa-phone-volume"></i>
              </div>
              <div class="info">
                <a href="tel:19006401">19006401</a>
              </div>
            </div>

            <div class="item d-flex align-items-center">
              <div class="icon me-2">
                <i class="fa-solid fa-envelope"></i>
              </div>
              <div class="info">
                <a href="mailto:hotro@vinabook.com">hotro@vinabook.com</a>
              </div>
            </div>

            <div class="item d-flex align-items-center">
              <div class="icon me-2">
                <i class="fa-solid fa-location-dot"></i>
              </div>
              <div class="info">
                <a href="/"
                  >52/2 Thoại Ngọc Hầu, Phường Hòa Thạnh, Quận Tân Phú, Hồ Chí
                  Minh</a
                >
              </div>
            </div>
          </div>
        </div>
        <div class="header__account">
          <div v-if="isToken === false">
            <a
              @click="onShowFormLogin"
              class="me-3 text-decoration-none text-uppercase fw-bold"
              >Đăng nhập</a
            >
            <a
              @click="onShowFormRegister"
              class="text-decoration-none text-uppercase fw-bold"
              >Đăng ký</a
            >
          </div>
            <div class="header__user d-flex align-items-center gap-2" v-else>
            <div class="user-image">
              <img
                src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRSbpqvNQMR7YBZ9eN_5qUeAzj6Vp-1ljDiuw&usqp=CAU"
                alt="Ảnh"
                class="header__navbar-item-avatar"
              />
            </div>
            <span class="user-name">{{ userInfo.Fullname }}</span>
            <div class="m-icon-arrow-down" style="scale: calc(10 / 14)"></div>
            <ul class="header__navbar-user-list">
              <li class="header__navbar-user-item">
                <a class="header__navbar-user-item-link" href="customer/account/user-info"
                  >Tài khoản của tôi</a
                >
              </li>
              <li class="header__navbar-user-item">
                <a class="header__navbar-user-item-link" href=""
                  >Địa chỉ của tôi</a
                >
              </li>
              <li class="header__navbar-user-item">
                <a class="header__navbar-user-item-link" href="">Đơn mua</a>
              </li>
              <li
                class="header__navbar-user-item header__navbar-user-item--logout"
                @click="onLogout"
              >
                <p class="header__navbar-user-item-link">Đăng xuất</p>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div class="layout-account"></div>
  <div class="header">
    <div class="container">
      <div class="d-flex justify-content-between align-items-center">
        <div class="header__logo me-5">
          <a style="cursor: pointer" @click="goToHomePage">
            <img
              style="width: 220px; height: 60px"
              src="../../../assets/logo.png"
              alt=""
            />
          </a>
        </div>
        <div class="header__search">
          <div class="header__search-box d-flex">
            <input
              class="header__search-input"
              type="text"
              placeholder="Tìm kiếm sản phẩm"
              name=""
              id=""
            />
            <div class="header__search-button">
              <i class="fa-solid fa-magnifying-glass"></i>
            </div>
            <div
              v-if="isShowLoadingInput"
              class="icon-container header__search-icon--loading"
            >
              <i class="loader"></i>
            </div>
            <div
              @click="clearValueInput"
              v-if="isShowIconClose"
              class="header__search-icon--close"
            >
              <i class="fa-solid fa-circle-xmark"></i>
            </div>
          </div>
          <div v-if="isShowSearchResult" class="header__search-result">
            <div class="header__search-result-item d-flex">
              <div class="header__search-result-item-image">
                <img
                  style="height: 100%"
                  src="../../../assets/img/anh-mau.jpg"
                  alt=""
                />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>Sự ỷ lại nguy hiểm</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">46.000đ</span>
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >79.000đ</span
                  >
                </div>
              </div>
            </div>
            <div class="header__search-result-item d-flex">
              <div class="header__search-result-item-image">
                <img
                  style="height: 100%"
                  src="../../../assets/img/anh-mau.jpg"
                  alt=""
                />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>Sự ỷ lại nguy hiểm</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">46.000đ</span>
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >79.000đ</span
                  >
                </div>
              </div>
            </div>
            <div class="header__search-result-item d-flex">
              <div class="header__search-result-item-image">
                <img
                  style="height: 100%"
                  src="../../../assets/img/anh-mau.jpg"
                  alt=""
                />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>Sự ỷ lại nguy hiểm</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">46.000đ</span>
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >79.000đ</span
                  >
                </div>
              </div>
            </div>
            <div class="header__search-result-item d-flex">
              <div class="header__search-result-item-image">
                <img
                  style="height: 100%"
                  src="../../../assets/img/anh-mau.jpg"
                  alt=""
                />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>Sự ỷ lại nguy hiểm</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">46.000đ</span>
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >79.000đ</span
                  >
                </div>
              </div>
            </div>
            <div class="header__search-result-item d-flex">
              <div class="header__search-result-item-image">
                <img
                  style="height: 100%"
                  src="../../../assets/img/anh-mau.jpg"
                  alt=""
                />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>Sự ỷ lại nguy hiểm</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">46.000đ</span>
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >79.000đ</span
                  >
                </div>
              </div>
            </div>
            <div class="header__search-result-footer">
              Xem thêm 200 sản phẩm
            </div>
          </div>
        </div>
        <div class="header__support ms-5 d-flex align-items-center">
          <img
            class="header__support-image"
            style="width: 32px; height: 32px"
            src="../../../assets/img/phone-call.png"
            alt=""
          />
          <div class="header__support-text ms-3">
            <p>Tư vấn bán hàng</p>
            <p>19006401</p>
          </div>
        </div>
      </div>
    </div>
  </div>
  <TheLogin
    v-if="isShowFormLoginOrRegister"
    @onCloseForm="onHideFormLoginOrRegister"
    :formAccount="formAccount"
  ></TheLogin>
</template>
<script>
import TheLogin from "./TheLogin.vue";
import { checkInfoTokensInStorage } from "@/js/token/TokenService";
import { removeAllInfoTokenToStorage } from "@/js/token/TokenService";
import localStorageService from "@/js/storage/LocalStorageService";
import userService from "@/utils/UserService";
export default {
  name: "TheHeaderUser",
  components: { TheLogin },
  created() {},
  beforeUnmount() {},
  computed: {
    isToken: () => {
      return checkInfoTokensInStorage();
    },
    userInfo: () => {
      return localStorageService.getItemFromLocalStorage("userInfo")
    }
  },
  methods: {
    onShowFormLogin() {
      this.formAccount = this.$Enum.FormAccount.Login;
      this.isShowFormLoginOrRegister = true;
    },
    onShowFormRegister() {
      this.formAccount = this.$Enum.FormAccount.Register;
      this.isShowFormLoginOrRegister = true;
    },
    onHideFormLoginOrRegister() {
      this.isShowFormLoginOrRegister = false;
    },
    goToHomePage() {
      this.$router.push({ path: "/" });
    },
    /**
     * Thực hiện đăng xuất khi click đăng xuất
     * Author: LQHUY(06/04/2024)
     */
    async onLogout(){
      try {
        var res = await userService.Logout(this.userInfo.Email);
        if(res.status === 201){
          this.$emitter.emit(
              "onShowToastMessage",
              this.$Resource[this.$languageCode].ToastMessage.Type.Success,
              "Đăng xuất thành công",
              this.$Resource[this.$languageCode].ToastMessage.Status.Success
            );
            removeAllInfoTokenToStorage();
            location.reload();
        }
      } catch (error) {
        console.log(error)
      }
    }
  },
  data() {
    return {
      isShowLoadingInput: false,
      isShowIconClose: false,
      isShowSearchResult: false,
      formAccount: this.$Enum.FormAccount.Login,
      isShowFormLoginOrRegister: false,
    };
  },
};
</script>
<style scoped>
.topbar {
  background: #417505;
  align-items: center;
  display: flex;
  position: relative;
  color: #fff;
  min-height: 40px;
}
.item {
  margin-right: 30px;
}
.info a {
  color: #fff;
  text-decoration: none;
  font-size: 14px;
}

.header__account a {
  font-size: 14px;
  color: #fff;
  cursor: pointer;
}
.user-name {
  font-size: 14px;
  cursor: pointer;
  position: relative;
}
.user-name:hover {
  opacity: 0.8;
}
.header__account .header__user .header__navbar-user-list {
  position: absolute;
  top: 100%;
  background-color: #fff;
  box-shadow: 0 2px 5px #bbb9b9;
  padding: 0 !important;
  display: none;
}
.header__account:hover .header__navbar-user-list{
  display: block;
}
/* .header__navbar-user-list::before {
  content: "";
  display: block;
  position: absolute;
  min-width: 140px;
  z-index: 1;
  height: 20px;
  right: 0;
  top: -12px;
} */
.header__navbar-user-list::after {
  content: "";
  display: block;
  position: absolute;
  border-width: 16px 20px;
  border-style: solid;
  border-color: transparent transparent #fff transparent;
  top: -26px;
  right: 46px;
}
.header__account .header__user .header__navbar-user-item {
  list-style-type: none;
  min-width: 140px;
}
.header__account .header__user .header__navbar-user-item-link {
  text-decoration: none;
  font-size: 14px;
  padding: 8px 12px;
  display: inline-block;
  color: #333;
  font-weight: 400;
  display: block;
}
.header__account
  .header__user
  .header__navbar-user-item:hover
  .header__navbar-user-item-link {
  background-color: rgb(247, 245, 245);
}

.header__navbar-user-item--logout {
  border-top: 1px solid rgba(0, 0, 0, 0.1);
}
.header__navbar-user-item--logout p{
  font-size: 14px;
  padding: 8px 12px !important;
  cursor: pointer;
  display: inline-block;
  color: #333;
  font-weight: 400;
}
.header__navbar-user-item--logout p {
  color: rgb(26, 147, 141) !important;
}
.info a:hover,
.header__account a:hover {
  color: #ff8c05;
}
.header {
  padding: 26px 0;
  height: auto;
  transition: all 0.3s ease-in-out;
  box-shadow: 0 2px 5px #ebebeb;
}
.header__logo img {
  width: 80%;
}
.header__support-text p {
  color: #333;
  font-size: 14px;
}
.header__support-text p {
  color: #333;
  font-size: 14px;
  text-align: left;
}
.header__logo {
  flex-shrink: 0;
}
.header__search {
  width: 600px;
  position: relative;
}
.header__search-input {
  padding: 12px 40px 12px 16px;
  width: 100%;
  height: 100%;
  outline: none;
  font-size: 14px;
  border: 1px solid #e0e0e0;
  border-radius: 24px;
}

.header__search-input:focus {
  border-color: #417505;
  transition: all 0.3s;
}

.header__search-box {
  position: relative;
  height: 100%;
}

.header__search-button {
  width: 60px;
  display: flex;
  position: absolute;
  top: 0px;
  right: 0;
  bottom: 0px;
  align-items: center;
  justify-content: center;
  font-size: 20px;
  background-color: rgba(22, 24, 35, 0.12);
  color: rgba(22, 24, 35, 0.34);
  border-bottom-right-radius: 24px;
  border-top-right-radius: 24px;
}

.header__search-button:hover {
  transition: all 0.3s;
  cursor: pointer;
  background: rgba(22, 24, 35, 0.06);
  color: rgba(22, 24, 35, 0.75);
}
.header__search-icon--close {
  color: rgba(22, 24, 35, 0.34);
}

.header__search-icon--close,
.header__search-icon--loading {
  position: absolute;
  right: 76px;
  top: 50%;
  transform: translateY(-50%);
}
.header__search-icon--loading {
  top: calc(50% + 2px);
  right: 75px;
}

.header__search-result {
  position: absolute;
  background-color: #fff;
  top: calc(100% + 5px);
  width: 100%;
  overflow: hidden;
  max-height: 365px;
  border-radius: 4px;
  box-shadow: 0 2px 5px #bbb9b9;
}
.header__search-result-footer {
  position: absolute;
  font-size: 14px;
  background-color: #fff;
  padding: 10px 0;
  bottom: 0;
  left: 0;
  right: 0;
}
.header__search-result-item {
  padding: 10px;
  border-bottom: 1px dashed #ccc;
}
.header__search-result-item-image {
  width: 70px;
  height: 60px;
}
.item-name h5 {
  font-size: 16px;
  color: #333;
  margin-bottom: 4px !important;
}
.item-price {
  font-size: 13px;
}
.item-price--discount {
  font-weight: 600;
}
.item-price--original {
  color: #797979;
}
.loader {
  position: relative;
  height: 18px;
  width: 18px;
  display: inline-block;
  animation: around 5.4s infinite;
}

@keyframes around {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

.loader::after,
.loader::before {
  content: "";
  background: white;
  position: absolute;
  display: inline-block;
  width: 100%;
  height: 100%;
  border-width: 2px;
  border-color: #50b83c #50b83c transparent transparent;
  border-style: solid;
  border-radius: 20px;
  box-sizing: border-box;
  top: 0;
  left: 0;
  animation: around 0.7s ease-in-out infinite;
}

.loader::after {
  animation: around 0.7s ease-in-out 0.1s infinite;
  background: transparent;
}
.header__support {
  flex-shrink: 0;
}
</style>
