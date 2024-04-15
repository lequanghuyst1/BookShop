<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-3">
        <TheSidebar></TheSidebar>
      </div>
      <div class="col-9">
        <TheSlideHome></TheSlideHome>
      </div>
    </div>
    <div class="home-tab-product">
      <div class="tab-heading">
        <h4>Sách mới nổi bật</h4>
      </div>
      <div class="tab-content">
        <div class="row">
          <div
            v-for="item in booksOutStanding"
            :key="item.BookId"
            class="col-lg-5ths"
            :title="item.BookName"
          >
            <div class="product-item">
              <div class="product-image d-block text-center">
                <div class="product-sale">-10%</div>
                <a :href="'product/' + item.BookSlug" class="d-block">
                  <img
                    :src="
                      item.ImagePath
                        ? item.ImagePath
                        : 'https://t3.ftcdn.net/jpg/04/34/72/82/240_F_34728286_OWQQvAFoXZLdGHlObozsolNeuSxhpr84.jpg'
                    "
                    alt=""
                  />
                </a>
                <div class="group-button">
                  <button class="btn-action button-add-like">
                      <i class="fa-solid fa-heart"></i>
                  </button>
                  <button @click="handleOnAdd(item)" class="btn-action button-add-cart">
                      <i class="fa-solid fa-cart-plus"></i>
                  </button>
                  <button class="btn-action button-detail">
                      <i class="fa-solid fa-eye"></i>
                  </button>
                </div>
              </div>
              <div class="product-detail">
                <h3 class="product-name">
                  <a href="">
                    {{ item.BookName }}
                  </a>
                </h3>
                <div class="product-price">
                  <div class="product-pirce--discount me-2">
                    {{ this.$helper.formatMoney(item.Price) }}đ
                  </div>
                  <div
                    class="product-pirce--original text-decoration-line-through"
                  >
                    79,000đ
                  </div>
                </div>
                <div class="product-quantity-sold">
                  <p>Đã bán:</p>
                  <p>{{ item.QuantitySold }}</p>
                </div>
              </div>
            </div>
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
export default {
  name: "HomeUserPage",
  components: { TheSidebar, TheSlideHome },
  created() {
    this.loadDataBooksOutStanding();
  },
  data() {
    return {
      booksOutStanding: [],
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
        const res = await bookService.getAll();
        if (res.status === 200) {
          this.booksOutStanding = res.data;
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
      item.UnitPrice = item.Price;
      item.Quantity = 1;
      item.ProvisionalMoney = item.Price;
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
  },
};
</script>
<style scoped>
.banner-item-picture {
  width: 100%;
}

.tab-heading {
  margin-bottom: 20px;
  padding: 5px 30px 5px 0px;
  background: transparent;
  border: 0;
  line-height: 24px;
  text-transform: capitalize;
  letter-spacing: 0.4px;
  position: relative;
  display: inline-block;
  font-size: 18px;
  color: #ec8000;
}
.tab-content {
  
}
</style>
