<template>
  <div class="cart">
    <div class="container">
      <div class="page-title title-buttons">
        <div class="page-title-container">
          <h1 style="display: inline-block; width: auto">Giỏ hàng</h1>
          <span class="cart-title-num-items">(4 sản phẩm)</span>
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
              />
            </div>
            <div>
              <span
                >Chọn tất cả (<span class="num-items-checkbox">4</span> sản
                phẩm)</span
              >
            </div>
            <div>Số lượng</div>
            <div>Thành tiền</div>
            <div></div>
          </div>
          <div class="product-cart-left">
            <div class="item-product-cart">
              <div class="checked-product-cart">
                <input
                  type="checkbox"
                  id="checkbox-product-387389"
                  name="checkbox_product_387389"
                  class="checkbox-add-cart"
                />
              </div>
              <div class="img-product-cart">
                <a
                  class="product-image"
                  href="https://www.fahasa.com/chu-khung-long-tham-an-giai-cuu-giang-sinh-bai-hoc-ve-thoi-tham-lam-song-ngu.html"
                  ><img
                    src="https://cdn0.fahasa.com/media/catalog/product//g/i/giang-sinh.jpg"
                    width="120"
                    height="120"
                    alt="Chú Khủng Long Tham Ăn: Giải Cứu Giáng Sinh – Bài Học Về Thói Tham Lam (Song Ngữ)"
                /></a>
              </div>
              <div class="group-product-info">
                <div class="info-product-cart">
                  <div>
                    <h2 class="product-name-full-text">
                      <a
                        href="https://www.fahasa.com/chu-khung-long-tham-an-giai-cuu-giang-sinh-bai-hoc-ve-thoi-tham-lam-song-ngu.html"
                        >Chú Khủng Long Tham Ăn: Giải Cứu Giáng Sinh – Bài Học
                        Về Thói Tham Lam (Song Ngữ)</a
                      >
                    </h2>
                  </div>
                  <div class="price-original">
                    <div class="cart-price">
                      <div class="cart-fhsItem-price">
                        <div><span class="price">31.500 đ</span></div>
                        <div class="fhsItem-price-old">
                          <span class="price">35.000 đ</span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="number-product-cart">
                  <div class="product-view-quantity-box">
                    <div class="product-view-quantity-box-block">
                      <a class="btn-subtract-qty"
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
                        name="cart[387389][qty]"
                        id="qty-387389"
                        maxlength="12"
                        align="center"
                        value="1"
                        title="So luong"
                      /><a class="btn-add-qty"
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
                      <a
                        onclick="cart.deleteItem('387389', event);"
                        title="Remove item"
                        id="387389"
                        class="btn-remove-mobile-cart"
                        ><i class="fa fa-trash-o" style="font-size: 22px"></i
                      ></a>
                    </div>
                  </div>
                  <div class="cart-price-total">
                    <span class="cart-price"
                      ><span class="price">31.500 đ</span></span
                    >
                  </div>
                </div>
              </div>
              <div class="div-of-btn-remove-cart">
                <a title="Remove Item" class="btn-remove-desktop-cart">
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
                  <span class="price">31.500 đ</span>
                </div>
              </div>
              <div class="border-product"></div>
              <div class="total-cart-page title-final-total">
                <div class="title-cart-page-left">Tổng Số Tiền (gồm VAT)</div>
                <div class="number-cart-page-right">
                  <span class="price">31.500 đ</span>
                </div>
              </div>
            </div>
            <div class="checkout-type-button-cart" style="text-align: center">
              <div class="method-button-cart">
                <button
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
export default {
  name: "CartPage",
};
</script>
<style scoped>
@import url(./cart.css);
button > span {
    color: white;
    background: #C92127;
    border: none;
    padding: 10px 20px;
    border-radius: 8px;
    transition: all 0.3s;
}
</style>
