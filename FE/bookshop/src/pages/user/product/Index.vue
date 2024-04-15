<template>
  <div :id="this.$route.params.id" class="breadcrumb-shop">
    <div class="container">
      <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 pd5">
          <ol
            class="breadcrumb breadcrumb-arrows"
            itemscope=""
            itemtype="http://schema.org/BreadcrumbList"
          >
            <li
              itemprop="itemListElement"
              itemscope=""
              itemtype="http://schema.org/ListItem"
              class="d-flex align-items-center"
            >
              <i style="color: #417505" class="fa-solid fa-house me-1"></i>
              <a href="/" target="_self" itemprop="item"
                ><span itemprop="name">Trang chủ</span></a
              >
              <meta itemprop="position" content="1" />
            </li>

            <li
              itemprop="itemListElement"
              itemscope=""
              itemtype="http://schema.org/ListItem"
            >
              <a href="/collections" target="_self" itemprop="item">
                <span itemprop="name">Danh mục</span>
              </a>
              <meta itemprop="position" content="2" />
            </li>

            <li
              class="active"
              itemprop="itemListElement"
              itemscope=""
              itemtype="http://schema.org/ListItem"
            >
              <span
                itemprop="item"
                content="https://www.vinabook.com/collections/sach-van-hoc-nuoc-ngoai"
                ><span itemprop="name">Sách Văn Học Nước Ngoài</span></span
              >
              <meta itemprop="position" content="3" />
            </li>
          </ol>
        </div>
      </div>
    </div>
  </div>
  <div class="product">
    <div class="container py-5 rounded-3">
      <div class="row">
        <div class="col-9">
          <div class="product-detail">
            <div class="row">
              <div class="col-6">
                <div class="product-gallary">
                  <div class="text-center">
                    <div class="product-image">
                      <img
                        :src="
                          productInfo.ImagePath ? productInfo.ImagePath : ''
                        "
                        alt=""
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="product-info">
                  <h3 class="product-name">{{ productInfo.BookName }}</h3>
                  <div class="d-flex justify-content-between">
                    <span class="product-publisher">
                      Nhà xuất bản:
                      <b>{{ productInfo.PublisherName }}</b>
                    </span>
                    <span class="product-author">
                      Tác giả:
                      <b>{{ productInfo.Author }}</b>
                    </span>
                  </div>
                  <div class="mt-1">
                    <span class="product-cover">
                      Hình thức bìa: <b>Bìa cứng</b>
                    </span>
                  </div>

                  <div class="product-price" style="padding: 16px 0 16px 0">
                    <span class="product-price-sale">
                      {{ this.$helper.formatMoney(productInfo.Price) }}đ</span
                    >
                    <span class="product-price-cover">
                      {{ productInfo.Price }}</span
                    >
                    <span class="product-discount">-10%</span>
                  </div>
                  <div class="policy-return d-flex">
                    <p>Chính sách đổi trả</p>
                    <div class="d-flex">
                      <p>Đổi trả sản phẩm trong 30 ngày</p>
                      <p class="see-more ms-2">Xem thêm</p>
                    </div>
                  </div>
                  <form v-on:submit.prevent action="" id="add-item-form">
                    <div class="select-action d-flex align-items-center">
                      <span class="select-title" style="min-width: 150px">
                        Số lượng
                      </span>
                      <div class="select-quantity d-flex">
                        <button
                          @click="hanldeOnReduceQuantity"
                          class="btn-quantity"
                        >
                          <i class="fa-solid fa-minus"></i>
                        </button>
                        <input
                          type="text"
                          class="quantity-add"
                          v-model="cartItem.Quantity"
                        />
                        <button
                          @click="handleOnIncreaseQuantity"
                          class="btn-quantity"
                        >
                          <i class="fa-solid fa-plus"></i>
                        </button>
                      </div>
                    </div>
                    <div class="wrap-addcart">
                      <button @click="handleOnAdd" class="add-to-cart">
                        <i class="fa-solid fa-cart-plus"></i>
                        <span>Thêm vào giỏ hàng</span>
                      </button>
                      <button class="buy-now">
                        <i class="fa-solid fa-money-bill-wave"></i>
                        <span>Mua ngay</span>
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="pro-service">
            <div class="pro-service-title">Chỉ có ở Vinabook</div>

            <div class="pro-service-item">
              <div class="pro-service-icon">
                <img
                  src="//theme.hstatic.net/200000845405/1001198377/14/pro_service_icon_1.png?v=457"
                  alt="icon"
                />
              </div>
              <div class="pro-service-text">Sản phẩm 100% chính hãng</div>
            </div>

            <div class="pro-service-item">
              <div class="pro-service-icon">
                <img
                  src="//theme.hstatic.net/200000845405/1001198377/14/pro_service_icon_2.png?v=457"
                  alt="icon"
                />
              </div>
              <div class="pro-service-text">
                Tư vấn mua sách trong giờ hành chính
              </div>
            </div>

            <div class="pro-service-item">
              <div class="pro-service-icon">
                <img
                  src="//theme.hstatic.net/200000845405/1001198377/14/pro_service_icon_3.png?v=457"
                  alt="icon"
                />
              </div>
              <div class="pro-service-text">
                Miễn phí vận chuyển cho Đơn hàng từ 250.000đ
              </div>
            </div>

            <div class="pro-service-item">
              <div class="pro-service-icon">
                <img
                  src="//theme.hstatic.net/200000845405/1001198377/14/pro_service_icon_5.png?v=457"
                  alt="icon"
                />
              </div>
              <div class="pro-service-text">Hotline: 1900 6401</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div class="product-introduction">
    <div class="container">
      <div class="row">
        <div class="product-introduction-header">Vinabook giới thiệu</div>
      </div>
      <div class="product-introduction-body">
        <div class="introduction-btn btn-prev">
          <i class="fa-solid fa-chevron-left"></i>
        </div>
        <div class="row">
          <div class="col-lg-5ths col-md-5ths">
            <div class="product-item">
              <div class="product-image d-block text-center">
                <div class="product-sale">-10%</div>
                <a :href="'product/' + productInfo.BookId" class="d-block">
                  <img
                    :src="productInfo.ImagePath ? productInfo.ImagePath : ''"
                    alt=""
                  />
                </a>
                <div class="group-button">
                  <button class="btn-action button-add-like">
                    <a href="/heart">
                      <i class="fa-solid fa-heart"></i>
                    </a>
                  </button>
                  <button class="btn-action button-add-cart">
                    <a href="/cart">
                      <i class="fa-solid fa-cart-plus"></i>
                    </a>
                  </button>
                  <button class="btn-action button-detail">
                    <a href="/detail">
                      <i class="fa-solid fa-eye"></i>
                    </a>
                  </button>
                </div>
              </div>
              <div class="product-detail">
                <h3 class="product-name">
                  <a href="">
                    {{ productInfo.BookName }}
                  </a>
                </h3>
                <div class="product-price d-flex flex-wrap">
                  <div class="product-pirce--discount me-2">
                    {{ productInfo.Price }}
                  </div>
                  <div
                    class="product-pirce--original text-decoration-line-through"
                  >
                    79,000đ
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="introduction-btn btn-next">
          <i class="fa-solid fa-chevron-right"></i>
        </div>
      </div>
    </div>
  </div>
  <div class="product__view-info">
    <div class="container">
      <div class="product__view-info-title">Thông tin sản phẩm</div>
      <div class="product__view-info-content">
        <table class="data-table table-additional">
          <colgroup>
            <col width="25%" />
            <col />
          </colgroup>
          <tbody>
            <tr>
              <th class="table-label">Mã hàng</th>
              <td class="data_sku">{{ productInfo.BookCode }}</td>
            </tr>
            <tr>
              <th class="table-label">Nhà xuất bản</th>
              <td class="data_supplier">
                <a
                  class="xem-chi-tiet"
                  href="sky-books?fhs_campaign=ATTRIBUTE_PRODUCT"
                  >{{ productInfo.PublisherName }}</a
                >
              </td>
            </tr>
            <tr>
              <th class="table-label">Tác giả</th>
              <td class="data_author">{{ productInfo.Author }}</td>
            </tr>

            <tr>
              <th class="table-label">Năm XB</th>
              <td class="data_publish_year">
                {{ productInfo.PublicationDate }}
              </td>
            </tr>
            <tr>
              <th class="table-label">Trọng lượng (gr)</th>
              <td class="data_weight">{{ productInfo.Heavy }}</td>
            </tr>
            <tr>
              <th class="table-label">Kích Thước Bao Bì</th>
              <td class="data_size">{{ productInfo.Size }}cm</td>
            </tr>
            <tr>
              <th class="table-label">Số trang</th>
              <td class="data_qty_of_page">{{ productInfo.NumberOfPage }}</td>
            </tr>
            <tr>
              <th class="table-label">Hình thức</th>
              <td class="data_book_layout">Bìa Mềm</td>
            </tr>

            <tr>
              <th class="table-label">Sản phẩm bán chạy nhất</th>
              <td>
                <a
                  style="font-size: 14px; color: #2489f4"
                  href="https://www.fahasa.com/sach-trong-nuoc/van-hoc-trong-nuoc/sach-to-mau-danh-cho-ngu-i-l-n.html?order=num_orders_month"
                  >Top 100 sản phẩm Sách Tô Màu Dành Cho Người Lớn bán chạy của
                  tháng</a
                >
              </td>
            </tr>

            <tr>
              <td colspan="2" style="padding: 0px">
                <div>
                  Giá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện
                  hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ
                  giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí
                  đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...
                </div>
                <div style="color: #c92127">
                  Chính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ
                  thống Nhà sách Fahasa trên toàn quốc
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div
        :style="{ height: isShowMoreViewDesc ? 'auto' : '200px' }"
        class="product__tabs-description mt-3"
      >
        <div id="desc_content" class="std">
          <p style="text-align: justify">
            <strong>Tô Bình Yên Vẽ Hạnh Phúc (Tái Bản 2022)</strong>
          </p>

          <p style="text-align: justify">
            Sau thành công của cuốn sách đầu tay “Phải lòng với cô đơn” chàng
            họa sĩ nổi tiếng và tài năng Kulzsc đã trở lại với một cuốn sách vô
            cùng đặc biệt mang tên: "Tô bình yên - vẽ hạnh phúc” – sắc nét phong
            cách cá nhân với một chút “thơ thẩn, rất hiền”.
          </p>

          <p style="text-align: justify">
            Không giống với những cuốn sách chỉ để đọc, “Tô bình yên – vẽ hạnh
            phúc” là một cuốn sách mà độc giả vừa tìm được “Hạnh phúc to to từ
            những điều nho nhỏ” vừa được thực hành ngay lập tức. Một sự kết hợp
            mới lạ đầy thú vị giữa thể loại sách tản văn và sách tô màu. Lật mở
            cuốn sách này, bạn sẽ thấy vô vàn những điều nhỏ bé dễ thương lan
            tòa nguồn năng lượng tích cực. Và kèm theo một list những điều tuyệt
            vời khiến bạn không thể bỏ lỡ:
          </p>

          <p style="text-align: justify">
            - Tận tình chỉ dẫn: 8 hướng dẫn tô màu đầy hứng thú từ Kulzsc
          </p>

          <p style="text-align: justify">
            - Tranh vẽ đơn giản, gần gũi. Nét vẽ đáng yêu, dễ thương
          </p>

          <p style="text-align: justify">
            - Chủ đề xoay quanh những điều bình yên trong cuộc sống: Đọc sách,
            đi dạo, dọn dẹp nhà cửa, trồng cây, ăn cơm mẹ nấu, nghe một bản nhạc
            hay, và nghĩ về nụ cười của một ai đó…
          </p>

          <p style="text-align: justify">
            - Mỗi bức tranh lại là những lời thủ thỉ, tâm tình của tác giả. Là
            những điều nhắn gửi nho nhỏ mong bạn bớt đi những xao động:
          </p>

          <p style="text-align: justify">“Em chọn hạnh phúc</p>

          <p style="text-align: justify">Em chọn bình yên</p>

          <p style="text-align: justify">Em chọn bên cạnh</p>

          <p style="text-align: justify">Những điều an yên”</p>

          <p style="text-align: justify">
            Hay đơn giản là những giãi bày ngắn gọn, thay nỗi lòng của một ai
            đó: "Tớ biết cậu một mình vẫn ổn, nhưng có những chuyện, có ai đó
            cùng làm, vẫn hơn."
          </p>

          <p style="text-align: justify">
            Thông qua những hình vẽ đang đợi được lấp đầy bằng muôn vàn sắc màu
            ấy, Kulzsc sẽ giúp bạn - những người lớn cô đơn, những ai đang cần
            bổ sung vitamin hạnh phúc “truy tìm” những niềm vui bị bỏ quên hay
            tuyệt chiêu để đối phó với stress.
          </p>

          <p style="text-align: justify">
            Bởi tô màu là một hình thức chữa lành đơn giản mà hiệu quả, nên mỗi
            khi thấy bực dọc, chán nản, hay khiến mình bận rộn hơn hãy thử tìm
            tới “Tô bình yên - vẽ hạnh phúc” cùng hộp màu đã cất lâu trong tủ
            và... Sao không thể là một đám mây màu tím, một mái tóc vàng tươi
            hay một nụ hoa màu xanh biển nhỉ? Không cần phải đắn đo đâu, bạn cứ
            thoải mái xóa đi căng thẳng, xóa đi những gam màu u ám, tự tay mình
            điểm tô những màu sắc tươi sáng lấp lánh, đầy ắp hy vọng theo ý
            thích, tận hưởng những phút thư giãn thật bình yên không muộn phiền
            thôi nào.
          </p>
          <div class="clear"></div>
        </div>
      </div>
      <div @click="openViewMoreDesc" class="desc-viewmore">
        <button>
          {{ isShowMoreViewDesc == false ? "Xem thêm" : "Rút gọn" }}
        </button>
      </div>
    </div>
  </div>

  <div class="product__view-policy">
    <div class="container">
      <div class="product-attribute">
        <div class="item">
          <div
            class="item-content"
            onclick="policy_popup.showPopUp('chinh-sach-khach-si', 'Chính Sách Khách Sỉ');"
          >
            <img
              src="https://cdn0.fahasa.com/media/wysiwyg/Thang-1-2020/icon/ico_shop_1.png"
            />
            <span><a>Chính Sách Khách Sỉ</a></span>
          </div>
        </div>
        <div class="item">
          <div
            class="item-content"
            onclick="policy_popup.showPopUp('phuong-thuc-van-chuyen', 'Thời Gian Giao Hàng');"
          >
            <img
              src="https://cdn0.fahasa.com/media/wysiwyg/Thang-1-2020/icon/ico_truck.png"
            />
            <span><a>Thời Gian Giao Hàng</a></span>
          </div>
        </div>
        <div class="item">
          <div
            class="item-content"
            onclick="policy_popup.showPopUp('chinh-sach-doi-tra', 'Chính Sách Đổi Trả');"
          >
            <img
              src="https://cdn0.fahasa.com/media/wysiwyg/Thang-1-2020/icon/ico_transfer.png"
            />
            <span><a>Chính Sách Đổi Trả</a></span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import localStorageService from "@/js/storage/LocalStorageService";
import bookService from "@/utils/BookService";
import cartItemService from "@/utils/CartItemService";
// import cartLocalStorageService from "@/js/storage/CartLocalStorage";
export default {
  name: "ProductPage",
  props: {
    id: {
      type: String,
    },
  },
  created() {
    this.loadDataProduct();
  },
  watch: {
    //Theo dõi biến số lượng cập nhật số tiền
    "cartItem.Quantity": function (newValue) {
      this.cartItem.ProvisionalMoney = this.cartItem.Price * newValue;
    },
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    /**
     * Thực hiện lấy dữ liệu chi tiết của sản phẩm
     * @author LQHUY(09/04/2024)
     */
    async loadDataProduct() {
      try {
        //gọi API lấy chi tiết sản phẩm
        const res = await bookService.getBySlug(this.$route.params.slug);
        if (res.status === 200) {
          this.productInfo = res.data;
          this.cartItem = res.data;
          this.cartItem.Quantity = 1;
          this.cartItem.ProvisionalMoney = this.cartItem.Price;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện tăng số lượng khi click icon +
     * @author LQHUY(09/04/2024)
     */
    handleOnIncreaseQuantity() {
      this.cartItem.Quantity++;
    },

    /**
     * Thực hiện giảm số lượng khi click icon +
     * @author LQHUY(09/04/2024)
     */
    hanldeOnReduceQuantity() {
      this.cartItem.Quantity--;
      if (this.cartItem.Quantity < 1) {
        this.cartItem.Quantity = 1;
      }
    },

    /**
     * Thực hiện mở rộng phần review khi nhấn btn xem thêm
     * @author LQHUY(09/04/2024)
     */
    openViewMoreDesc() {
      this.isShowMoreViewDesc = !this.isShowMoreViewDesc;
    },

    /**
     * Thực hiện thêm vào giỏ hàng khi clcik btn Thêm vào giỏ hàng
     * @author LQHUY(09/04/2024)
     */
    async handleOnAdd() {
      this.cartItem.CartId = this.userInfo.CartId;
      this.cartItem.UnitPrice = this.cartItem.Price;
      const formData = new FormData();
      formData.append("dataJson", JSON.stringify(this.cartItem));
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
  data() {
    return {
      productInfo: {},
      cartItem: {},
      quantity: 1,
      isShowMoreViewDesc: false,
    };
  },
};
</script>
<style scoped>
.product .container {
  background-color: #fff;
  border-radius: 4px;
}
.product-detail .product-image img {
  max-width: 100%;
  min-width: 70%;
}
.product-detail .product-info .product-name {
  font-size: 28px;
  font-weight: 500;
  color: #333;
  overflow-wrap: break-word;
  padding-bottom: 16px;
}
.product-publisher,
.product-author,
.product-cover {
  font-size: 13px;
}
.product-price {
  display: flex;
  align-items: center;
}
.product-price .product-price-sale {
  font-size: 32px;
  line-height: 32px;
  color: #c92127;
  font-family: "Roboto", sans-serif !important;
  font-weight: 700;
}
.product-price .product-price-cover {
  margin-left: 16px;
  color: #777a7b;
  font-size: 16px;
  text-decoration: line-through;
}
.product-price .product-discount {
  margin-left: 8px;
  padding: 2px 8px;
  border-radius: 4px;
  background-color: #c92127;
  color: #fff;
  font-size: 14px;
  font-weight: 600;
}
.policy-return {
  font-size: 13px;
  margin-bottom: 24px;
}
.policy-return > p {
  min-width: 150px;
  max-width: 200px;
}
.see-more {
  color: #2489f4;
  cursor: pointer;
}
.select-quantity {
  border: 1px solid #ccc;
  border-radius: 4px;
  overflow: hidden;
}
.quantity-add {
  width: 30px;
  border: none;
  outline: none;
  text-align: center;
}
.select-quantity button {
  width: 46px;
  padding: 4px 0;
  background-color: #fff;
  border: none;
  outline: none;
}
.wrap-addcart {
  margin-top: 24px;
  display: flex;
  justify-content: center;
  column-gap: 20px;
}
.wrap-addcart button {
  height: 44px;
  width: 200px;
  font-size: 14px;
  font-weight: 600;
  border-radius: 12px;
}
.wrap-addcart button i {
  margin-right: 12px;
}
.wrap-addcart .add-to-cart {
  margin-left: 0;
  color: #c92127;
  background: #fff;
  border: 2px solid #c92127;
}
.wrap-addcart .buy-now {
  background-color: #c92127;
  color: #fff;
  border: none;
}

.pro-service {
  border: 1px solid #ddd;
  border-radius: 5px;
  margin-bottom: 15px;
}
.pro-service .pro-service-title {
  font-weight: bold;
  color: #333;
  font-size: 16px;
  margin-bottom: 0;
  padding: 15px;
  background: #ecebe9;
}
.pro-service .pro-service-item {
  display: flex;
  align-items: center;
  color: #333;
  padding: 19px 20px;
  margin-right: 20px;
}
.pro-service .pro-service-icon {
  width: 32px;
  margin-right: 20px;
}
.pro-service .pro-service-icon img {
  max-width: 100%;
  -webkit-backface-visibility: hidden;
  -ms-transform: translateZ(0);
  -webkit-transform: translateZ(0);
  transform: translateZ(0);
}
.pro-service .pro-service-text {
  width: calc(100% - 46px);
  font-size: 16px;
  line-height: 23px;
}

.product-introduction {
  margin: 24px 0;
}
.product-introduction-body {
  position: relative;
}
.product-introduction-body .introduction-btn {
  position: absolute;
  top: 40%;
  transform: translateY(-50%);
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1;
  color: #ccc;
  border: 1px solid #ccc;
}
.product-introduction-body .introduction-btn.btn-prev {
  left: -5px;
}
.product-introduction-body .introduction-btn.btn-next {
  right: -5px;
}
.product-introduction .container {
  border-radius: 8px;
  overflow: hidden;
}
.product-introduction-header {
  padding-top: 15px;
  padding-left: 15px;
  padding-bottom: 20px;
  font-size: 17px;
  font-weight: bold;
  text-transform: uppercase;
  text-align: center;
  display: flex;
  align-items: center;
  margin-top: 1px;
}
.product-introduction .row {
  background-color: #fff !important;
}

/*Start product-view-info */
.product__view-info {
  margin: 24px 0;
}
.product__view-info .container {
  background-color: #fff;
  border-radius: 8px;
  padding-top: 16px;
  padding-bottom: 16px;
}
.product__view-info-content {
  padding: 10px 0;
  border-bottom: 1px solid #c1c1c1;
}
.product__view-info-title {
  padding-top: 8px;
  font-size: 17px;
  font-weight: bold;
  text-transform: uppercase;
  text-align: center;
  display: flex;
  align-items: center;
}
.data-table {
  border: none;
  box-shadow: none;
  width: 100%;
  font-size: 14px;
}
.data-table tbody th {
  border: none !important;
  padding: 4px 0 !important;
  color: #777;
  display: table-cell;
  font-weight: 500;
  line-height: 1.6;
  text-align: left;
  white-space: nowrap;
  vertical-align: baseline;
  color: #777;
}
.std {
  font-size: 14px;
}
.std p {
  margin-bottom: 10px !important;
}
.product__tabs-description {
  overflow: hidden;
  transition: all 0.5s;
}
@keyframes openViewDesc {
}
.desc-viewmore {
  display: flex;
  justify-content: center;
  padding: 8px;
}
.desc-viewmore button {
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
  transition: all 0.5s;
}
/*End product-view-info */

/* view-policy */
.product__view-policy .container {
  background-color: #fff;
  border-radius: 8px;
}
.product__view-policy .product-attribute {
  display: flex;
  justify-content: space-around;
  align-items: center;
  padding: 10px 27px;
  margin-top: 16px;
  margin-bottom: 16px;
}
.product__view-policy .product-attribute .item {
  display: inline-block;

  padding: 10px 10px;
}
.product__view-policy .product-attribute .item span {
  padding-left: 16px;
  font-size: 16px;
  font-weight: 600;
  color: #646464;
  cursor: pointer;
}
</style>
