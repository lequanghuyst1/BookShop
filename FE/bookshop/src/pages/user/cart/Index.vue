<template>
  <div class="cart">
    <div class="container">
      <div class="page-title title-buttons">
        <div class="page-title-container">
          <h1 style="display: inline-block; width: auto">Giỏ hàng</h1>
          <span class="cart-title-num-items"
            >({{ quantityOfCart }} sản phẩm)</span
          >
        </div>
      </div>
      <form v-on:submit.prevent action="" id="form-cart">
        <div class="row">
          <div class="col-8">
            <div class="header-cart-item" style="display: flex">
              <div class="checkbox-all-product">
                <input
                  class="checkbox-add-cart"
                  type="checkbox"
                  id="checkbox-all-products"
                  @click="this.lstIdItemSelected = []"
                  v-model="selectAllItem"
                />
              </div>
              <div>
                <span
                  >Chọn tất cả (<span
                    v-show="this.quantityItemSected > 0"
                    class="num-items-checkbox"
                    >{{ this.quantityItemSected }}</span
                  >
                  sản phẩm)</span
                >
              </div>
              <div>Số lượng</div>
              <div>Thành tiền</div>
              <div></div>
            </div>
            <div class="product-cart-left">
              <div
                v-for="item in cartData"
                :key="item.CartItemId"
                class="item-product-cart"
              >
                <div class="checked-product-cart">
                  <input
                    type="checkbox"
                    :value="item.CartItemId"
                    v-model="lstIdItemSelected"
                    @click="handleOnSelectItem(item)"
                    class="checkbox-add-cart"
                  />
                </div>
                <div class="img-product-cart">
                  <a
                    class="product-image"
                    href="https://www.fahasa.com/chu-khung-long-tham-an-giai-cuu-giang-sinh-bai-hoc-ve-thoi-tham-lam-song-ngu.html"
                    ><img
                      :src="
                        item.ImagePath
                          ? item.ImagePath
                          : 'https://t3.ftcdn.net/jpg/04/34/72/82/240_F_34728286_OWQQvAFoXZLdGHlObozsolNeuSxhpr84.jpg'
                      "
                      width="120"
                      height="120"
                      :alt="item.BookName"
                  /></a>
                </div>
                <div class="group-product-info">
                  <div class="info-product-cart">
                    <div>
                      <h2 class="product-name-full-text">
                        <a :href="'product' + item.BookId">{{
                          item.BookName
                        }}</a>
                      </h2>
                    </div>
                    <div class="price-original">
                      <div class="cart-price">
                        <div class="cart-fhsItem-price">
                          <div>
                            <span class="price"
                              >{{
                                this.$helper.formatMoney(item.Price)
                              }}
                              đ</span
                            >
                          </div>
                          <div class="fhsItem-price-old">
                            <span class="price"
                              >{{
                                this.$helper.formatMoney(item.Price)
                              }}
                              đ</span
                            >
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="number-product-cart">
                    <div class="product-view-quantity-box">
                      <div class="product-view-quantity-box-block">
                        <a
                          @click="hanldeOnReduceQuantity(item)"
                          class="btn-subtract-qty"
                          style="padding: 8px"
                          ><img
                            style="
                              width: 12px;
                              height: auto;
                              vertical-align: middle;
                            "
                            src="https://cdn0.fahasa.com/skin//frontend/ma_vanese/fahasa/images/ico_minus2x.png" /></a
                        ><input
                          type="text"
                          class="qty-carts"
                          id="qty-387389"
                          maxlength="12"
                          v-model="item.Quantity"
                          title="So luong"
                        /><a
                          @click="handleOnIncreaseQuantity(item)"
                          class="btn-add-qty"
                          style="padding: 8px"
                          ><img
                            style="
                              width: 12px;
                              height: auto;
                              vertical-align: middle;
                            "
                            src="https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/ico_plus2x.png"
                        /></a>
                      </div>
                      <div
                        class="product-view-icon-remove-mobile"
                        style="display: none"
                      >
                        <p title="Remove item" class="btn-remove-mobile-cart">
                          <i class="fa fa-trash-o" style="font-size: 22px"></i>
                        </p>
                      </div>
                    </div>
                    <div class="cart-price-total">
                      <span class="cart-price"
                        ><span class="price"
                          >{{
                            this.$helper.formatMoney(item.Quantity * item.Price)
                          }}
                          đ</span
                        ></span
                      >
                    </div>
                  </div>
                </div>
                <div class="div-of-btn-remove-cart">
                  <a
                    @click="handleOnDelete(item.CartItemId)"
                    title="Remove Item"
                    class="btn-remove-desktop-cart"
                  >
                    <i class="fa-regular fa-trash-can"></i>
                  </a>
                </div>
              </div>
              <div class="border-product"></div>
            </div>
          </div>
          <div class="col-4">
            <div class="block-total-cart" style="">
              <div class="block-totals-cart-page">
                <div class="total-cart-page">
                  <div class="title-cart-page-left">Thành tiền</div>
                  <div class="number-cart-page-right">
                    <span class="price">{{ totalAmountCart }} đ</span>
                  </div>
                </div>
                <div class="border-product"></div>
                <div class="total-cart-page title-final-total">
                  <div class="title-cart-page-left">Tổng Số Tiền (gồm VAT)</div>
                  <div class="number-cart-page-right">
                    <span class="price">{{ totalAmountCart }} đ</span>
                  </div>
                </div>
              </div>
              <div class="checkout-type-button-cart" style="text-align: center">
                <div class="method-button-cart">
                  <button
                    @click="this.$router.push('checkout')"
                    type="button"
                    title="Thanh toán"
                    class="button btn-proceed-checkout btn-checkout"
                  >
                    <span><span>Thanh toán</span></span>
                  </button>
                  <div class="retail-note">
                    <a
                      href="https://www.fahasa.com/chinh-sach-khach-si/"
                      target="_blank"
                      >(Giảm giá trên web chỉ áp dụng cho bán lẻ)</a
                    >
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import cartItemService from "@/utils/CartItemService";
import localStorageService from "@/js/storage/LocalStorageService";
export default {
  name: "CartPage",
  created() {
    //Gán dữ liệu cho cart lấy từ localStorage
    this.cartData = cartLocalStorageService.getCartFromLocalStorage();
    this.getQuantityOfCart();
  },
  data() {
    return {
      cartData: [],
      totalAmountCart: 0,
      lstIdItemSelected: [],
      quantityOfCart: 0,
      quantityItemSected: 0,
    };
  },
  watch: {
    lstIdItemSelected: function () {
      this.getQuantityItemSelected();
      this.calculatorTotalAmountCart();
    },
  },

  computed: {
    //biến chọn tất cả
    selectAllItem: {
      // Khi truy cập giá trị computed property
      get: function () {
        return this.cartData
          ? this.cartData
              .map((item) => item.CartItemId)
              .every((ele) => this.lstIdItemSelected.includes(ele)) &&
              this.lstIdItemSelected.length >= this.cartData.length &&
              this.lstIdItemSelected.length > 0
          : false;
      },
      // Khi thay đổi giá trị computed property
      set: function (value) {
        if (value) {
          this.cartData.forEach((item) => {
            if (
              !this.lstIdItemSelected
                .map((ele) => ele.CartItemId)
                .includes(item.CartItemId)
            ) {
              this.lstIdItemSelected.push(item.CartItemId);
            }
          });
          this.lstIdItemSelected = [...this.lstIdItemSelected];
      localStorageService.setItemToLocalStorage("itemSelected",this.lstIdItemSelected);
        } else {
          if (this.lstIdItemSelected.length > 0) {
            return;
          } else {
            this.lstIdItemSelected = [];

            localStorageService.setItemToLocalStorage("itemSelected",this.lstIdItemSelected);

          }
        }
      },
    },
    userInfo: function () {
      return localStorageService.getItemEncodeFromLocalStorage("userInfo");
    },
  },
  methods: {
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

    /**
     * Thực hiện lẩy ra tổng số lượng sản phẩm được chọn
     * @author LQHUY(09/04/2024)
     */
    getQuantityItemSelected() {
      const itemSelected = this.cartData.filter((item) => {
        return this.lstIdItemSelected.includes(item.CartItemId);
      });
      this.quantityItemSected = itemSelected.reduce(
        (accumulator, item) => accumulator + item.Quantity,
        0
      );
    },

    /**
     * Thực hiện tính toán tổng số tiền trong giỏ hàng
     * @author LQHUY(09/04/2024)
     */
    calculatorTotalAmountCart() {
      const itemSelected = this.cartData.filter((item) => {
        return this.lstIdItemSelected.includes(item.CartItemId);
      });
      const totalAmount = itemSelected.reduce(
        (accumulator, item) => accumulator + item.ProvisionalMoney,
        0
      );
      this.totalAmountCart = this.$helper.formatMoney(totalAmount);
    },

    /**
     * Thực hiện tăng số lượng khi click icon +
     * @author LQHUY(09/04/2024)
     */
    async handleOnIncreaseQuantity(item) {
      item.Quantity++;
      await this.handleOnEdit(item);
    },

    /**
     * Thực hiện giảm số lượng khi click icon +
     * @author LQHUY(09/04/2024)
     */
    async hanldeOnReduceQuantity(item) {
      item.Quantity--;
      if (item.Quantity < 1) {
        item.Quantity = 1;
      }
      await this.handleOnEdit(item);
    },

    /**
     * Hàm update cart item
     * @param {object} item
     * @author LQHUY(11/04/2024)
     */
    async handleOnEdit(item) {
      this.$emitter.emit("toggleShowLoading", true);
      try {
        //gán 1 số giá trị cho item
        item.CartId = this.userInfo.CartId;
        item.UnitPrice = item.Price;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(item));
        //gọi api update
        const res = await cartItemService.put(item.CartId, formData);
        if (res.status === 200) {
          //update số lượng vào local
          cartLocalStorageService.updateQuantityItemToCart(item);
          //Gán dữ liệu cho cart lấy từ localStorage
          this.cartData = cartLocalStorageService.getCartFromLocalStorage();
          //gọi hàm update lại tổng số lượng sản phẩm trong cart
          this.$emitter.emit("getQuantityOfCart");
          this.getQuantityOfCart();
          setTimeout(() => {
            this.$emitter.emit("toggleShowLoading", false);
          }, 300);
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Thực hiện xóa bỏ product đã chọn ra khỏi cart theo id
     * @param {string} id
     * @author LQHUY(09/04/2024)
     */
    async handleOnDelete(id) {
      this.$emitter.emit("toggleShowLoading", true);

      const res = await cartItemService.delete(id);
      if (res.status === 200) {
        //thực hiện xỏa bỏ
        cartLocalStorageService.removeItemGetOutCart(id);

        //xóa rồi gán lại dữ liệu cho cartData
        this.cartData = cartLocalStorageService.getCartFromLocalStorage();

        //Update lại tổng số lượng sản phẩm trong cart
        this.$emitter.emit("getQuantityOfCart");
        this.getQuantityOfCart();

        setTimeout(() => {
          this.$emitter.emit("toggleShowLoading", false);
        }, 300);
      }
    },

    /**
     * Hàm thực hiện thêm hoặc gỡ id được chọn vào lstIdItemSelected khi click vào chekbox
     * @param {object} item
     * @author LQHUY(09/04/2024)
     */
    handleOnSelectItem(item) {
      // this.lstIdItemSelected[index] = true;
      const index = this.lstIdItemSelected.indexOf(item.CartItemId);
      if (index === -1) {
        // Nếu phần tử không tồn tại trong mảng, thêm nó vào mảng
        this.lstIdItemSelected.push(item.CartItemId);
      } else {
        // Nếu phần tử tồn tại trong mảng, loại bỏ nó khỏi mảng
        this.lstIdItemSelected.splice(index, 1);
      }
      this.lstIdItemSelected = [...this.lstIdItemSelected];
      localStorageService.setItemToLocalStorage("itemSelected",this.lstIdItemSelected);
    },
  },
};
</script>
<style scoped>
@import url(./cart.css);
button > span {
  color: white;
  background: #c92127;
  border: none;
  padding: 10px 20px;
  border-radius: 8px;
  transition: all 0.3s;
}
</style>
