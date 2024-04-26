<template>
  <div class="main-content">
    <div class="container pt-5">
      <div class="row">
        <div class="col-3">
          <TheSidebar></TheSidebar>
        </div>
        <div class="col-9">
          <TheSlideHome></TheSlideHome>
        </div>
      </div>
      <div class="home-product home-product-new">
        <div class="tab-heading">
          <h4>Sách mới nổi bật</h4>
        </div>
        <div class="tab-content">
          <div
            @click="onPrevSlide"
            v-if="startIndex !== 0"
            class="wrap-button button-prev-slide"
          >
            <i class="fa-solid fa-chevron-left"></i>
          </div>
          <div class="">
            <swiper
              :modules="modules"
              :slides-per-view="5"
              :slides-per-column="2"
              slides-per-column-fill="row"
              :pagination="{ clickable: true }"
            >
              <swiper-slide
                v-for="item in booksOutStanding?.slice(startIndex, endIndex)"
                :key="item.BookId"
                style="margin-right: 10px"
              >
                <div class="inner-item" :title="item.BookName">
                  <div
                    :style="{
                      backgroundColor:
                        item.QuantityInStock > 0
                          ? '#fff'
                          : 'rgba(173, 161, 161, 0.1)',
                    }"
                    class="product-item"
                  >
                    <div class="product-image d-block text-center">
                      <a
                        :href="'http://localhost:8080/' + item.BookSlug"
                        class="d-block"
                      >
                        <img
                          :src="
                            item.ImagePath
                              ? item.ImagePath
                              : 'https://t3.ftcdn.net/jpg/04/34/72/82/240_F_34728286_OWQQvAFoXZLdGHlObozsolNeuSxhpr84.jpg'
                          "
                          alt=""
                        />
                      </a>
                      <div v-if="item.QuantityInStock > 0" class="group-button">
                        <button class="btn-action button-add-like">
                          <i class="fa-solid fa-heart"></i>
                        </button>
                        <button
                          @click="handleOnAdd(item)"
                          class="btn-action button-add-cart"
                        >
                          <i class="fa-solid fa-cart-plus"></i>
                        </button>
                        <button class="btn-action button-detail">
                          <i class="fa-solid fa-eye"></i>
                        </button>
                      </div>
                      <div
                        v-if="item.QuantityInStock === 0"
                        class="product-sold-out"
                      >
                        Hết hàng
                      </div>
                    </div>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          {{ item.BookName }}
                        </a>
                      </h3>
                      <div class="product-price">
                        <div class="group-price d-flex align-items-center">
                          <div class="product-pirce--discount me-2">
                            {{ this.$helper.formatMoney(item.Price) }}đ
                          </div>
                          <div v-if="item.Discount" class="discount-percent">
                            - {{ item.Discount }}%
                          </div>
                        </div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          {{ this.$helper.formatMoney(item.OriginalPrice) }}đ
                        </div>
                      </div>
                      <div class="product-quantity-sold">
                        <p>Đã bán:</p>
                        <p>{{ item.QuantitySold }}</p>
                      </div>
                    </div>
                  </div>
                </div>
              </swiper-slide>
            </swiper>
          </div>
          <div @click="onNextSlide" class="wrap-button button-next-slide">
            <i class="fa-solid fa-chevron-right"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import TheSidebar from "@/components/user/layout/TheSidebar.vue";
import bookService from "@/utils/BookService";
import cartItemService from "@/utils/CartItemService";
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import localStorageService from "@/js/storage/LocalStorageService";
import TheSlideHome from "../../../components/user/layout/TheSlideHome.vue";
// import Swiper core and required modules
// import { Pagination, Scrollbar, Autoplay } from "swiper/modules";

// Import Swiper Vue.js components
import { Swiper, SwiperSlide } from "swiper/vue";

// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import "swiper/css/scrollbar";

// Import Swiper styles
import "swiper/css";
export default {
  name: "HomeUserPage",
  components: { TheSidebar, TheSlideHome, Swiper, SwiperSlide },
  created() {
    this.loadDataBooksOutStanding();
  },
  mounted(){
    document.title = "Trang chủ";

  },
  data() {
    return {
      booksOutStanding: [],
      startIndex: 0,
      endIndex: 10,
    };
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    async loadDataBooksOutStanding() {
      try {
        const params = {
          pageSize: 20,
          pageNumber: 1,
        };
        const res = await bookService.getFilterPaging({ params });
        if (res.status === 200) {
          this.booksOutStanding = res.data.Data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện thêm vào giỏ hàng khi click btn Thêm vào giỏ hàng
     * @author LQHUY(09/04/2024)
     */
    async handleOnAdd(item) {
      item.CartId = this.userInfo.CartId;
      item.Quantity = 1;
      const formData = new FormData();
      formData.append("dataJson", JSON.stringify(item));
      //gọi api thêm mới
      const res = await cartItemService.post(formData);
      if (res.status === 201) {
        const result = await cartItemService.getByCartId(this.userInfo.CartId);
        if (result.status === 200) {
          const dataCart = result.data;
          //thêm mới item vào cart local
          cartLocalStorageService.setCartToLocalStorage(dataCart);
        }

        this.$emitter.emit(
          "onShowToastMessage",
          this.$Resource[this.$languageCode].ToastMessage.Type.Success,
          "Sản phẩm đã được thêm vào giỏ hàng.",
          this.$Resource[this.$languageCode].ToastMessage.Status.Success
        );

        //Update lại tổng số lượng sản phẩm trong cart
        this.$emitter.emit("getQuantityOfCart");
      }
    },
    onNextSlide() {
      if (this.endIndex + 2 <= this.booksOutStanding.length) {
        this.startIndex += 2;
        this.endIndex += 2;
      }
    },
    onPrevSlide() {
      this.startIndex -= 2;
      this.endIndex -= 2;
    },
  },
};
</script>
<style scoped>
.banner-item-picture {
  width: 100%;
}
.main-content {
  background-color: #f0f0f0;
}
.home-product {
  background-color: #fff;
  margin-top: 32px;
  border-radius: 8px;
  box-shadow: 0 1px 2px 0 rgba(60, 64, 67, 0.1),
    0 2px 6px 2px rgba(60, 64, 67, 0.15);
}

.tab-heading {
  background: transparent;
  border: 0;
  text-transform: capitalize;
  letter-spacing: 0.4px;
  padding: 16px 16px 16px 16px;
}
.tab-heading h4 {
  font-size: 18px;
  font-weight: 600;
}
.tab-content {
  padding: 0 10px 16px 10px;
  position: relative;
  z-index: 1;
}
.wrap-button {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  color: #333;
  z-index: 1;
  align-items: center;
  background: #fff;
  box-shadow: 0 0 4px 0 rgba(0, 0, 0, 0.2);
  display: flex;
  font-size: 18px;
  height: 60px;
  justify-content: center;
  opacity: 0.7;
  outline: none;
  top: 50%;
  transition: 0.3s;
  width: 30px;
  cursor: pointer;
  z-index: 2;
}
.button-prev-slide {
  border-radius: 0 100px 100px 0;
  left: 0;
  padding-right: 10px;
}
.button-next-slide {
  border-radius: 100px 0 0 100px;
  padding-left: 10px;
  right: 0;
}
.swiper-slide {
  margin-right: 5px;
  margin-left: 4px;
  margin-top: 4px;
  max-width: calc(20% - 11px);
}
.home-product-new {
  margin-bottom: 32px;
}
</style>
