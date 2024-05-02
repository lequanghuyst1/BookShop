<template>
  <div v-if="isShowOverlay" class="m-dialog">
    <div class="m-dialog__overlay"></div>
  </div>
  <div class="topbar">
    <div class="container">
      <div
        v-if="this.isHeaderFixed === false"
        class="topbar-wrap d-lg-flex justify-content-between"
      >
        <!-- <div class="header__contact d-flex align-items-center">
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
        </div> -->
        <div class="top-banner-block">
          <p>
            <a href="https://www.fahasa.com/minh-long?fhs_campaign=topbanner"
              ><img
                src="https://cdn0.fahasa.com/media/wysiwyg/Thang-04-2024/MinhLongT4_Header_1263x60.jpg"
                alt=""
            /></a>
          </p>
        </div>
      </div>
    </div>
  </div>

  <div class="header" :class="{ fixed: isHeaderFixed }">
    <div class="container">
      <div class="d-flex justify-content-between align-items-center">
        <div class="header__logo me-5">
          <div
            class="fhs_mouse_point"
            onclick="location.href = '/';"
            title="FAHASA.COM"
          >
            <img
              src="https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/fahasa-logo.png"
              alt=""
              style="width: 220px; vertical-align: middle"
            />
          </div>
        </div>
        <div class="header__search">
          <div class="header__search-box d-flex">
            <input
              ref="inputIcon"
              class="header__search-input"
              type="text"
              placeholder="Tìm kiếm sản phẩm"
              name=""
              v-model="searchString"
              id=""
              @click="
                () => {
                  this.isShowOverlay = true;
                  this.isShowSearchResult = true;
                }
              "
            />
            <div @click="goToSearchEnginePage" class="header__search-button">
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
            <a
              v-for="book in booksFilter"
              :key="book.BookId"
              class="header__search-result-item d-flex"
              :href="'http://localhost:8080/' + book.BookSlug"
              @click="
                () => {
                  this.isShowOverlay = false;
                  this.isShowSearchResult = false;
                }
              "
            >
              <div class="header__search-result-item-image">
                <img style="height: 100%" :src="book.ImagePath" alt="" />
              </div>
              <div
                class="header__search-result-item-info ms-2 w-100 text-start"
              >
                <div class="item-name">
                  <h5>{{ book.BookName }}</h5>
                </div>
                <div class="item-price d-flex">
                  <span class="item-price--discount">
                    {{ this.$helper.formatMoney(book.OriginalPrice) }} đ</span
                  >
                  <span
                    class="item-price--original ms-3 text-decoration-line-through"
                    >{{ this.$helper.formatMoney(book.Price) }} đ</span
                  >
                </div>
              </div>
            </a>

            <div
              v-if="totalRecord > this.paramFilter.pageSize"
              class="header__search-result-footer"
              @click="goToSearchEnginePage"
            >
              Xem thêm {{ totalRecord }} sản phẩm
            </div>
          </div>
        </div>

        <div class="header__block-right gap-3 ms-5 d-flex align-items-center">
          <div class="block-item">
            <a href="" class="block-item-link" id="btn-notification">
              <div class="item-icon">
                <i class="fa-regular fa-bell"></i>
              </div>
              <p class="item-title">Thông báo</p>
            </a>
          </div>
          <div class="block-item">
            <a
              style="cursor: pointer"
              @click="onGoToCartPage"
              class="block-item-link"
              id="btn-cart"
            >
              <div class="item-icon d-flex justify-content-center">
                <div class="m-icon-cart"></div>
              </div>
              <p class="item-title" style="margin-top: 3px !important">
                Giỏ hàng
              </p>
              <span v-show="this.quantityOfCart > 0" class="cart-quantity">{{
                this.quantityOfCart
              }}</span>
            </a>
          </div>
          <div
            @click="this.isShowDropDownAccount = !this.isShowDropDownAccount"
            class="block-item"
            id="btn-account"
          >
            <div style="cursor: pointer" class="block-item-link">
              <div class="item-icon">
                <i class="fa-regular fa-user"></i>
              </div>
              <p class="item-title">
                {{ isToken ? userInfo?.Fullname : "Tài khoản" }}
              </p>
            </div>

            <!-- Chưa đăng nhập -->
            <div
              class="fhs_top_account_dropdown fhs_dropdown right guest"
              style="display: block"
              v-show="isShowDropDownAccount && !isToken"
            >
              <div @click="onShowFormLogin">
                <button
                  type="button"
                  title="Đăng nhập"
                  class="fhs_btn_default active"
                >
                  <span>Đăng nhập</span>
                </button>
              </div>
              <div @click="onShowFormRegister">
                <button type="button" title="Đăng ký" class="fhs_btn_default">
                  <span>Đăng ký</span>
                </button>
              </div>
            </div>

            <!-- Đã đăng nhập -->
            <div
              v-show="isShowDropDownAccount && isToken"
              style="min-width: 300px"
              class="fhs_top_account_dropdown fhs_dropdown right min"
            >
              <div>
                <a
                  class="fhs_center_space d-flex justify-content-between"
                  href="/customer/home"
                  ><div class="fhs_center_left">
                    <div
                      class="fhs_center_left ico_vip_copper"
                      style="margin-right: 8px"
                    ></div>
                    <div>
                      <div
                        class="fhs_center_left fhs_nowrap_one"
                        style="
                          font-size: 1.23em;
                          color: #0d0e0f;
                          font-weight: bold;
                          max-width: 200px;
                          height: 22px;
                          text-transform: capitalize;
                        "
                      ></div>
                      <div class="fhs_center_left" style="height: 22px">
                        Thành viên Fahasa
                      </div>
                    </div>
                  </div>
                  <div
                    class="icon_seemore_gray right"
                    style="width: 24px; height: 24px"
                  ></div
                ></a>
              </div>
              <div style="border-top: 1px solid #f2f4f5">
                <a
                  class="fhs_center_left"
                  href="http://localhost:8080/customer/order"
                  ><span class="icon_bill_gray" style="margin-right: 8px"></span
                  ><span>Đơn hàng của tôi</span></a
                >
              </div>
              <div style="border-top: 1px solid #f2f4f5">
                <a class="fhs_center_left" href="/wishlist/"
                  ><span class="ico_heart_gray" style="margin-right: 8px"></span
                  ><span>Sản phẩm yêu thích</span></a
                >
              </div>
              <div style="border-top: 1px solid #f2f4f5">
                <a class="fhs_center_left" href="/tryout/voucher/"
                  ><span
                    class="ico_voucher_gray"
                    style="margin-right: 8px"
                  ></span
                  ><span>Wallet Voucher</span></a
                >
              </div>
              <div
                class="fhs_center_space"
                style="border-top: 1px solid #f2f4f5"
              >
                <div class="fhs_center_left fhs_nowrap_one fhs_flex_grow">
                  <a
                    class="fhs_center_left fhs_flex_grow"
                    href="/tryout/history/"
                    ><span
                      class="ico_fpoint_gray"
                      style="margin-right: 8px"
                    ></span
                    ><span>Tài khoản F-point</span></a
                  >
                </div>
              </div>
              <div
                class="fhs_center_left fhs_mouse_point"
                style="cursor: pointer; border-top: 1px solid #f2f4f5"
                @click="onLogout"
              >
                <div class="fhs_center_left fhs_flex_grow">
                  <span class="ico_logout_gray" style="margin-right: 8px"></span
                  ><span>Thoát tài khoản</span>
                </div>
              </div>
              <div
                class="fhs_center_left fhs_mouse_point"
                @click="
                  (e) => {
                    e.stopImmediatePropagation();
                    this.isShowDropDownAccount = !this.isShowDropDownAccount;
                  }
                "
                style="
                  border-top: 1px solid #c27b86;
                  display: flex;
                  justify-content: center;
                  cursor: pointer;
                "
              >
                <span style="color: #c27b86 !important; font-size: 16px"
                  >Đóng</span
                >
              </div>
            </div>
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

  <div v-if="isShowWaringLogin" class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div style="max-width: 300px" class="m-dialog__container">
      <div class="m-dialog__header">
        <slot name="header"></slot>
        <h3 class="m-dialog__header-title">{{ "Thành viên" }}</h3>

        <div class="m-dialog__header-action">
          <div
            v-tippy="{
              content: 'Thoát',
              placement: 'bottom',
            }"
            class="m-dialog__header-close"
            @click="this.isShowWaringLogin = false"
          >
            <i class="fa-solid fa-xmark"></i>
          </div>
        </div>
      </div>
      <div class="m-dialog__content">
        <p>
          Vui lòng đăng nhập tài khoản để xem ưu đãi và thanh toán dễ dàng hơn.
        </p>
      </div>
      <div class="m-dialog__footer">
        <div class="m-dialog__footer-left">
          <button @click="onShowFormLogin" class="btn-login">Đăng nhập</button>
        </div>
        <div class="m-dialog__footer-right">
          <button @click="onShowFormRegister" class="btn-register">
            Đăng ký
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import TheLogin from "./TheLogin.vue";
import { checkInfoTokensInStorage } from "@/js/token/TokenService";
import { removeAllInfoTokenToStorage } from "@/js/token/TokenService";
import localStorageService from "@/js/storage/LocalStorageService";
import userService from "@/utils/UserService";
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import bookService from "@/utils/BookService";
import { mapActions } from "vuex";
export default {
  name: "TheHeaderUser",
  components: { TheLogin },
  data() {
    return {
      isShowLoadingInput: false,
      isShowIconClose: false,
      isShowSearchResult: false,
      isShowOverlay: false,
      formAccount: this.$Enum.FormAccount.Login,
      isShowFormLoginOrRegister: false,
      isShowDropDownAccount: false,
      isShowWaringLogin: false,
      quantityOfCart: 0,
      isHeaderFixed: false,
      searchString: null,
      booksFilter: [],
      paramFilter: {
        pageSize: 4,
        pageNumber: 1,
        searchString: null,
      },
      totalRecord: 0,
    };
  },
  created() {
    this.$emitter.on("getQuantityOfCart", this.getQuantityOfCart);
    this.getQuantityOfCart();
  },
  mounted() {
    this.getBooksFilterData();
    document.addEventListener("click", (e) => {
      if (!e.target.closest("#btn-account")) {
        this.isShowDropDownAccount = false;
      }
      if (!e.target.closest(".header__search")) {
        this.isShowSearchResult = false;
        this.isShowOverlay = false;
      }
    });
    document.addEventListener("scroll", () => {
      var scrollPosition =
        window.pageYOffset || document.documentElement.scrollTop;
      if (scrollPosition > 60) {
        // Khi cuộn dính sát top
        this.isHeaderFixed = true;
      } else {
        this.isHeaderFixed = false;
      }
    });
  },
  beforeUnmount() {
    this.$emitter.off("getQuantityOfCart", this.getQuantityOfCart);
  },
  computed: {
    isToken: () => {
      return checkInfoTokensInStorage();
    },
    userInfo: () => {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  watch: {
    /**
     * Gán lại cho giá trị truyền vào
     * @param {string} newValue
     * Author: (03/01/2024)
     */
    searchString: function (newValue) {
      this.isShowLoadingInput = true;
      this.isShowIconClose = false;
      clearTimeout(this.timeoutHandler);
      this.timeoutHandler = setTimeout(() => {
        this.isShowLoadingInput = false;
        if (newValue === null || newValue === "") {
          this.isShowIconClose = false;
        } else {
          this.isShowIconClose = true;
        }
        this.getBooksFilterData();
        this.setSearchString(newValue);
      }, 500);
    },
  },
  methods: {
    ...mapActions(["setSearchString"]),

    /**
     * Xóa bỏ dữ liệu trên ô tìm kiếm
     * Author: Author: (03/01/2024)
     */
    clearValueInput() {
      this.searchString = "";
      this.isShowIconClose = false;
      this.$refs["inputIcon"].focus();
    },
    async getBooksFilterData() {
      try {
        const params = {
          pageSize: 5,
          pageNumber: 1,
          searchString: this.searchString,
        };
        const res = await bookService.getFilterPaging({ params });
        if (res.status === 200) {
          this.booksFilter = res.data.Data;
          this.totalRecord = res.data.TotalRecord;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thực hiện lẩy ra tổng số lượng sản phẩm trong cart
     * @author LQHUY(09/04/2024)
     */
    getQuantityOfCart() {
      const cartList = cartLocalStorageService.getCartFromLocalStorage();
      this.quantityOfCart = cartList.reduce(
        (accumulator, item) => accumulator + item.Quantity,
        0
      );
    },

    onShowFormLogin() {
      event.stopImmediatePropagation();
      this.formAccount = this.$Enum.FormAccount.Login;
      this.isShowFormLoginOrRegister = true;
      this.isShowDropDownAccount = false;
      this.isShowWaringLogin = false;
    },
    onShowFormRegister() {
      event.stopImmediatePropagation();
      this.formAccount = this.$Enum.FormAccount.Register;
      this.isShowFormLoginOrRegister = true;
      this.isShowDropDownAccount = false;
      this.isShowWaringLogin = false;
    },
    onHideFormLoginOrRegister() {
      this.isShowFormLoginOrRegister = false;
    },
    goToHomePage() {
      this.$router.push({ path: "/" });
    },
    /**
     * Thực hiện đăng xuất khi click đăng xuất
     * @author LQHUY(06/04/2024)
     */
    async onLogout() {
      try {
        var res = await userService.Logout(this.userInfo.Email);
        if (res.status === 201) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đăng xuất thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          removeAllInfoTokenToStorage();
          localStorageService.removeItemLocalStorage("cart");
          location.reload();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện đi tới trang giỏ hành khi click icon giỏ hàng
     * @author LQHUY(01/04/2024)
     */
    onGoToCartPage() {
      if (this.isToken) {
        location.href = "http://localhost:8080/cart";
      } else {
        this.isShowWaringLogin = true;
      }
    },
    goToSearchEnginePage() {
      location.href =
        "http://localhost:8080/search-engine/" + this.searchString;
    },
  },
};
</script>
<style scoped>
.topbar {
  align-items: center;
  display: flex;
  position: relative;
  color: #fff;
  min-height: 40px;
  background-color: #ff5653;
  z-index: 10;
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
.header__account:hover .header__navbar-user-list {
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
.header__navbar-user-item--logout p {
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
  height: 68px;
  display: flex;
  align-items: center;
  transition: all 0.3s ease-in-out;
  box-shadow: 0 2px 5px #ebebeb;
  position: relative;
}
.fixed {
  position: fixed;
  top: 0; /* Khoảng cách header cố định cách top */
  width: 100%;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2); /* Hiệu ứng đổ bóng */
  z-index: 5;
}
.header__logo img {
  width: 80%;
}
.block-item {
  margin-left: 8px;
}
.block-item-link {
  color: #7a7e7f;
  font-size: 18px;
}
.block-item-link .item-icon {
  font-weight: 600;
}
.block-item-link .item-title {
  color: #646464;

  font-size: 13px;
}
#btn-account {
  position: relative;
}
.fhs_dropdown {
  position: absolute;
  bottom: -10px;
  right: 0;
  -webkit-transform: translate(0, 100%);
  -ms-transform: translate(0, 100%);
  -moz-transform: translate(0, 100%);
  transform: translate(0, 100%);
  background-color: white;
  z-index: 999;
  white-space: nowrap;
  -webkit-border-radius: 8px;
  border-radius: 8px;
  -moz-border-radius: 8px;
  -webkit-border-radius: 8px;
  -webkit-box-shadow: 0 4px 6px rgb(32 33 36 / 28%);
  -moz-box-shadow: 0 4px 6px rgb(32 33 36 / 28%);
  box-shadow: 0 4px 6px rgb(32 33 36 / 28%);
  color: #7a7e7f;
  max-width: 100vw;
  cursor: default;
  box-shadow: 0 2px 5px #bbb9b9;
}
.fhs_top_account_dropdown > div:first-of-type {
  padding: 16px 12px 16px 12px;
  -moz-border-top-left-radius: 8px;
  -moz-border-top-right-radius: 8px;
  -webkit-border-top-left-radius: 8px;
  -webkit-border-top-right-radius: 8px;
}
.fhs_center_left {
  display: flex;
  display: -webkit-box;
  display: -ms-flexbox;
  display: -webkit-flex;
  align-items: center;
  -webkit-align-items: center;
  -webkit-justify-content: flex-start;
  justify-content: flex-start;
  text-align: left;
  font-size: 14px;
  color: #7a7e7f;
}
.fhs_nowrap_one {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-size: 1.23em;
  color: #0d0e0f;
  font-weight: bold;
  max-width: 200px;
  height: 22px;
  text-transform: capitalize;
}
.fhs_center_left {
  display: flex;
  display: -webkit-box;
  display: -ms-flexbox;
  display: -webkit-flex;
  align-items: center;
  -webkit-align-items: center;
  -webkit-justify-content: flex-start;
  justify-content: flex-start;
  text-align: left;
}
.icon_seemore_gray.right {
  transform: rotate(-90deg);
  transition: all 0.5s ease 0s;
}

.fhs_top_account_dropdown > div {
  padding: 9px 12px;
}

.fhs_center_left span {
  color: #7a7e7f;
  font-size: 14px;
  font-weight: 500;
}

.fhs_top_account_dropdown.min > div:hover,
.fhs_top_languages_dropdown > div:hover {
  background-color: #f2f4f5;
}

.fhs_bar_bottom a,
.fhs_btn_default {
  width: 100%;
  display: flex;
  display: -webkit-box;
  display: -ms-flexbox;
  display: -webkit-flex;
  -ms-flex-align: center;
  -webkit-align-items: center;
  align-items: center;
  -webkit-justify-content: center;
  justify-content: center;
  color: #c92127;
  background-color: #fff;
  border: 2px solid #c92127;
  font-size: 14px;
  font-weight: 700;
  width: 210px;
  max-width: 100%;
  height: 40px;
  max-height: 100%;
  border-radius: 8px;
  -moz-border-radius: 8px;
  -webkit-border-radius: 8px;
  cursor: pointer;
  -webkit-user-drag: none;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -o-user-select: none;
  user-select: none;
}

.fhs_bar_bottom a.active,
.fhs_btn_default.active {
  color: #fff;
  background-color: #c92127;
  border: 2px solid #c92127;
}
.fhs_top_account_dropdown.guest > div:first-of-type {
  padding: 16px 12px 4px 12px;
}
.fhs_top_account_dropdown.guest > div:last-of-type {
  padding: 4px 12px 16px 12px;
}

.fhs_btn_default:hover {
  font-size: 15px;
}

#btn-cart {
  position: relative;
}
#btn-cart .cart-quantity {
  position: absolute;
  top: -8px;
  right: 5px;
  font-size: 12px;
  color: #fff;
  width: 20px;
  height: 20px;
  background-color: #c92127;
  border-radius: 50%;
  border: 1px solid #fff;
}
.header {
  z-index: 10;
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
  max-height: 450px;
  border-radius: 4px;
  box-shadow: 0 2px 5px #bbb9b9;
  z-index: 2;
}
.header__search-result-footer {
  font-size: 14px;
  background-color: #fff;
  padding: 10px 0;
  cursor: pointer;
}
.header__search-result-footer:hover{
  background-color: #ecebeb;
}
.header__search-result-item {
  padding: 10px;
  border-bottom: 1px dashed #ccc;
}
.header__search-result-item:hover {
  background-color: #ecebeb;
  cursor: pointer;
}
.header__search-result-item-image {
  width: 70px;
  height: 60px;
}
.item-name h5 {
  font-size: 15px;
  color: #333;
  margin-bottom: 4px !important;
}
.item-price {
  font-size: 13px;
}
.item-price--discount {
  font-weight: 600;
  color: #333;
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
.m-dialog__header h3 {
  color: #d70018 !important;
}
.m-dialog__content {
  margin: 12px 0 !important;
}
.m-dialog__content p {
  font-size: 15px;
  font-weight: 600;
  text-align: center;
  color: #4a4a4a;
}
.m-dialog__footer {
  height: auto !important;
  padding: 12px 24px !important;
}
.m-dialog__footer-right,
.m-dialog__footer-left {
  flex-basis: 45%;
}
.btn-register {
  -webkit-text-fill-color: transparent;
  align-items: center;
  background-size: 200% auto;
  background: linear-gradient(90deg, #ff512f, #dd2440 51%, #ff512f);
  -webkit-background-clip: text;
  border: 2px solid #dd2440;
  border-radius: 8px;
  box-shadow: 0 0 20px #eee;
  cursor: pointer;
  font-weight: 600;
  width: 100%;
  justify-content: center;
  padding: 10px;
  text-align: center;
  transition: 0.5s;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
}

.btn-login {
  align-items: center;
  background-image: linear-gradient(90deg, #ff512f, #dd2440 51%, #ff512f);
  background-size: 200% auto;
  border-radius: 8px;
  box-shadow: 0 0 20px #eee;
  color: #fff;
  cursor: pointer;
  font-weight: 600;
  justify-content: center;
  padding: 10px;
  text-align: center;
  transition: 0.5s;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
  border: none;
  width: 100%;
}
.btn-login:hover,
.btn-register:hover {
  background-position: 100%;
  color: #fff;
  -webkit-text-decoration: none;
  text-decoration: none;
  transform: scale(1.02);
}
</style>
