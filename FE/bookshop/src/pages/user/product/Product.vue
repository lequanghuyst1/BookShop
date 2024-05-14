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
              <i style="color: #c92127" class="fa-solid fa-house me-1"></i>
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
              <a
                :href="'/collections/' + productInfo.CategorySlug"
                target="_self"
                itemprop="item"
              >
                <span itemprop="name">{{ productInfo.CategoryName }}</span>
              </a>
              <meta itemprop="position" content="2" />
            </li>

            <li
              itemprop="itemListElement"
              itemscope=""
              itemtype="http://schema.org/ListItem"
            >
              <span
                itemprop="item"
                content="https://www.vinabook.com/collections/sach-van-hoc-nuoc-ngoai"
                ><span itemprop="name">{{ productInfo.BookName }}</span></span
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
                      {{
                        this.$helper.formatMoney(productInfo.OriginalPrice)
                      }}đ</span
                    >
                    <span v-show="productInfo.Discount" class="product-discount"
                      >-{{ productInfo.Discount }}%</span
                    >
                  </div>
                  <div class="policy-return d-flex">
                    <p>Thời gian giao hàng</p>
                    <div class="">
                      <p>
                        Giao hàng đến
                        <b
                          >{{ deliveryAddress?.Ward }},
                          {{ deliveryAddress.District }},
                          {{ deliveryAddress.Province }}</b
                        >
                      </p>
                      <p class="mt-2">
                        Dự kiến giao
                        <b>{{ dateDelivery }}</b>
                      </p>
                    </div>
                  </div>

                  <div class="policy-return d-flex">
                    <p>Chính sách đổi trả</p>
                    <div class="d-flex">
                      <p>Đổi trả sản phẩm trong 30 ngày</p>
                      <p class="see-more ms-2">Xem thêm</p>
                    </div>
                  </div>
                  <form v-on:submit.prevent action="" id="add-item-form">
                    <div class="select-action d-flex align-items-center pb-3">
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
                    <p v-if="messageInfo" class="not-enough-quantity">
                      {{ messageInfo }}
                    </p>
                    <div
                      v-show="productInfo.QuantityInStock > 0"
                      class="wrap-addcart"
                    >
                      <button @click="handleOnAddCart" class="add-to-cart">
                        <i class="fa-solid fa-cart-plus"></i>
                        <span>Thêm vào giỏ hàng</span>
                      </button>
                      <button @click="handleOnPurchase" class="buy-now">
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
            <div class="pro-service-title">Chỉ có ở WanderBook</div>

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

            <!-- <div class="pro-service-item">
              <div class="pro-service-icon">
                <img
                  src="//theme.hstatic.net/200000845405/1001198377/14/pro_service_icon_3.png?v=457"
                  alt="icon"
                />
              </div>
              <div class="pro-service-text">
                Miễn phí vận chuyển cho Đơn hàng từ 250.000đ
              </div>
            </div> -->

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
    <div class="container" style="padding: 0 !important">
      <div class="product-introduction-body">
        <div class="home-product home-product-new">
          <div class="tab-heading">
            <h4>Fahasa giới thiệu</h4>
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
                        <div
                          v-if="item.QuantityInStock > 0"
                          class="group-button"
                        >
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
                {{ this.$helper.formatDate(productInfo.PublicationDate) }}
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
          <p>
            <b>{{ productInfo.BookName }}</b>
          </p>
          <p v-html="productDesc"></p>
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

  <div class="preview-product">
    <div class="container">
      <div class="preview__title">
        <h2>Đánh giá và nhận xét sản phẩm</h2>
      </div>
      <div class="preview-content">
        <div class="preview__rating">
          <div class="row">
            <div class="col-4">
              <div class="preview__rating-point">
                <p>{{ this.avergeRating ? this.avergeRating : 0 }}/5</p>
                <div class="item-star d-flex gap-2">
                  <div
                    v-for="star in 5"
                    :key="star"
                    class="star-contaier d-flex"
                  >
                    <div
                      class="icon"
                      :class="{ 'is-active': star <= this.avergeRating }"
                    >
                      <i class="fa-solid fa-star"></i>
                    </div>
                  </div>
                </div>
                <div class="count-preview">
                  <p>{{ this.totalReview }} đánh giá</p>
                </div>
              </div>
            </div>
            <div class="col-5">
              <div class="box-review">
                <div
                  v-for="(item, index) in lstRating"
                  :key="index"
                  class="rating-level d-flex align-items-center"
                >
                  <div class="star-count">
                    <span>{{ item.rating }}</span>
                    <div class="rating-level-icon is-active">
                      <i class="fa-solid fa-star"></i>
                    </div>
                  </div>
                  <div class="progress w-100">
                    <div
                      class="progress-bar bg-primary"
                      role="progressbar"
                      :style="{
                        width: (item.quantity / this.totalReview) * 100 + '%',
                      }"
                      :aria-valuenow="(item.quantity / this.totalReview) * 100"
                      aria-valuemin="0"
                      aria-valuemax="100"
                    ></div>
                  </div>
                  <span class="count-user-preview flex-shrink-0">
                    {{ item.quantity }} đánh giá
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="button__review-contaier">
          <p>Bạn đánh giá sao về sản phẩm này</p>
          <div
            @click="this.isShowFormPreviewProduct = true"
            class="button-review"
          >
            <div class="button-review-icon">
              <i class="fa-solid fa-pen"></i>
            </div>
            <button class="btn-review">Viết đánh giá</button>
          </div>
        </div>
        <div class="box-review">
          <div
            v-for="(item, index) in reviewProducts"
            :key="item.ReviewProductId"
            class="review-comment-item"
          >
            <div class="review-comment-item-title">
              <div class="review-comment-item-avatar"></div>
              <div class="block-info">
                <p class="block-info__name">{{ item.Fullname }}</p>
                <div class="date-time">
                  <div class="icon-clock">
                    <i class="fa-regular fa-clock"></i>
                  </div>
                  <p>{{ this.$helper.formatOrderDate(item.ReviewDate) }}</p>
                </div>
                <!-- <p v-show="item.IsEdit" class="content-edit">Đã chỉnh sửa</p> -->
              </div>
              <div
                v-if="item.UserId === this.userInfo?.UserId"
                style="margin-right: 0px"
                class="edit-comment"
                @click="onShowActionReview(index)"
              >
                <i class="fa-solid fa-ellipsis-vertical"></i>
                <ul v-if="isShowActionReview[index]" class="action-list">
                  <li
                    @click="onEditItem(item.ReviewProductId)"
                    class="action-item"
                  >
                    Sửa
                  </li>
                  <li
                    @click="hanldeOnDeleteReviewProduct(item.ReviewProductId)"
                    class="action-item"
                  >
                    Xóa
                  </li>
                </ul>
              </div>
            </div>
            <div class="comment-item-review">
              <div class="star-contaier d-flex">
                <div
                  v-for="number in 5"
                  :key="number"
                  :class="{ 'is-active': number <= item.Rating }"
                  class="icon"
                >
                  <i class="fa-solid fa-star"></i>
                </div>
              </div>
              <div class="comment-content">
                {{ item.Content }}
              </div>
            </div>
          </div>
        </div>
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
  <PreviewProduct
    @onCloseForm="
      () => {
        this.isShowFormPreviewProduct = false;
      }
    "
    @loadData="getReviewProductData"
    :userId="this.userInfo.UserId"
    :bookId="this.productInfo.BookId"
    :reviewProductIdEdit="reviewProductIdEdit"
    v-if="isShowFormPreviewProduct"
  ></PreviewProduct>
</template>
<script>
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import localStorageService from "@/js/storage/LocalStorageService";
import bookService from "@/utils/BookService";
import cartItemService from "@/utils/CartItemService";
import PreviewProduct from "./PreviewProduct.vue";
import reviewProductService from "@/utils/ReviewProductService";
import { Swiper, SwiperSlide } from "swiper/vue";

// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import "swiper/css/scrollbar";

// Import Swiper styles
import "swiper/css";
import deliveryAddressService from "@/utils/DeliveryAddressService";
// import cartLocalStorageService from "@/js/storage/CartLocalStorage";
export default {
  name: "ProductPage",
  components: { PreviewProduct, Swiper, SwiperSlide },
  props: {
    id: {
      type: String,
    },
  },
  created() {
    this.loadDataProduct();
  },
  mounted() {
    this.loadDataBooksOutStanding();
    this.getDeliveryAddressDefault();
  },
  data() {
    return {
      productInfo: {},
      cartItem: {},
      // quantity: 1,
      isShowMoreViewDesc: false,
      isShowFormPreviewProduct: false,
      reviewProducts: [],
      isShowActionReview: [],
      reviewProductIdEdit: null,
      totalReview: 0,
      lstRating: [
        { rating: 5, quantity: 0 },
        { rating: 4, quantity: 0 },
        { rating: 3, quantity: 0 },
        { rating: 2, quantity: 0 },
        { rating: 1, quantity: 0 },
      ],
      avergeRating: null,
      messageInfo: null,

      booksOutStanding: [],
      startIndex: 0,
      endIndex: 10,
      productDesc: "",
      category: {},
      deliveryAddress: {},
      dateDelivery: null,
    };
  },
  watch: {
    //Theo dõi biến số lượng cập nhật số tiền
    "cartItem.Quantity": function (newValue) {
      if (newValue > this.productInfo.QuantityInStock) {
        this.messageInfo = `Số lượng yêu cầu ${newValue} không có
        sẵn.`;
        this.cartItem.Quantity = this.productInfo.QuantityInStock;
      }
    },
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    formatContent(desc) {
      let content = desc
        ? desc.replace(/\n\n/g, "<br><br>").replace(/\n/g, "<br>")
        : "";
      return content;
    },
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
          this.cartItem.Quantity = this.productInfo.QuantityInStock > 0 ? 1 : 0;
          this.getReviewProductData();
          this.productDesc = this.formatContent(this.productInfo.Description);
          document.title = this.productInfo.BookName;
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
      if (this.messageInfo) {
        this.messageInfo = null;
      }
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
    async handleOnAddCart() {
      if (
        this.cartItem.Quantity > this.cartItem.QuantityInStock ||
        this.cartItem.Quantity === 0
      ) {
        return;
      }
      this.cartItem.CartId = this.userInfo.CartId;
      const formData = new FormData();
      formData.append("dataJson", JSON.stringify(this.cartItem));
      //gọi api thêm mới
      const res = await cartItemService.post(formData);
      if (res.status === 201) {
        const result = await cartItemService.getByCartId(this.userInfo?.CartId);
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
    /**
     * Thực hiện mua ngay khi click Mua ngay
     * @author LQHUY
     */
    async handleOnPurchase() {
      try {
        if (
          this.cartItem.Quantity > this.cartItem.QuantityInStock ||
          this.cartItem.Quantity === 0
        ) {
          return;
        }
        this.cartItem.CartId = this.userInfo.CartId;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.cartItem));
        //gọi api thêm mới
        const res = await cartItemService.post(formData);
        if (res.status === 201) {
          const result = await cartItemService.getByCartId(
            this.userInfo?.CartId
          );
          if (result.status === 200) {
            const dataCart = result.data;
            const itemAddNew = dataCart.filter(
              (item) => item.BookId === this.cartItem.BookId
            );
            if (itemAddNew.length > 0) {
              localStorageService.setItemToLocalStorage("itemSelected", [
                itemAddNew[0].CartItemId,
              ]);
            }
            //set lại cart local
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
          this.$router.push("cart");
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy danh sách các đánh giá và nhận xét
     * @author LQHUY(18/04/2024)
     */
    async getReviewProductData() {
      try {
        const params = {
          bookId: this.productInfo.BookId,
        };
        const res = await reviewProductService.getByBookId({ params });
        if (res.status === 200) {
          this.reviewProducts = res.data;
          this.totalReview = this.reviewProducts.length;
          this.lstRating.forEach((item) => {
            const quantity = this.reviewProducts.filter(
              (ele) => ele.Rating === item.rating
            ).length;
            item.quantity = quantity;
          });
        }
        const totalRating = this.reviewProducts.reduce(
          (currentValue, item) => currentValue + item.Rating,
          0
        );
        this.avergeRating = totalRating / this.totalReview;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện show hoặc hide danh sách hành động của
     * @author LQHUY(18/04/2024)
     */
    onShowActionReview(index) {
      if (this.isShowActionReview[index]) {
        this.isShowActionReview[index] = false;
      } else {
        this.isShowActionReview[index] = true;
      }
    },

    /**
     * Thực hiện show form khi click sửa
     * @author LQHUY(18/04/2024);
     */
    onEditItem(id) {
      this.reviewProductIdEdit = id;
      this.isShowFormPreviewProduct = true;
    },

    /**
     * Thực hiện xóa đánh giá khi click xóa
     * @author LQHUY(18/04/2024);
     */
    async hanldeOnDeleteReviewProduct(id) {
      try {
        const res = await reviewProductService.delete(id);
        if (res.status === 200) {
          this.getReviewProductData();
        }
      } catch (error) {
        console.log(error);
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
      this.endInd;
    },

    async getDeliveryAddressDefault() {
      try {
        const res = await deliveryAddressService.getAllByUserId(
          this.userInfo.UserId
        );
        if (res.status === 200) {
          this.deliveryAddress = res.data.filter(
            (item) => item.DeliveryAddressDefault === true
          )[0];
          // Lấy ra ngày hiện tại
          var today = new Date();

          // Cộng thêm 3 ngày
          var nextDate = new Date(today);
          if (this.deliveryAddress.Province === "Thành phố Hà Nội") {
            nextDate.setDate(today.getDate() + 3);
          } else {
            nextDate.setDate(today.getDate() + 5);
          }

          // Danh sách các ngày trong tuần
          var daysOfWeek = [
            "Chủ Nhật",
            "Thứ Hai",
            "Thứ Ba",
            "Thứ Tư",
            "Thứ Năm",
            "Thứ Sáu",
            "Thứ Bảy",
          ];

          // Lấy ra thứ của ngày kế tiếp
          var nextDayOfWeek = daysOfWeek[nextDate.getDay()];

          // Lấy ra ngày và tháng của ngày kế tiếp
          var nextDay = nextDate.getDate();
          var nextMonth = nextDate.getMonth() + 1; // Tháng trong JavaScript bắt đầu từ 0

          // Hiển thị kết quả

          this.dateDelivery =
            nextDayOfWeek +
            " - " +
            (nextDay < 10 ? "0" + nextDay : nextDay) +
            "/" +
            (nextMonth < 10 ? "0" + nextMonth : nextMonth);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
.edit-comment {
  position: relative;
  width: 20px;
  height: 20px;
  cursor: pointer;
}
.edit-comment .action-list {
  padding: 0 !important;
  margin: 0 !important;
  position: absolute;
  background-color: #fff;
  right: 0;
  border-radius: 4px;
  box-shadow: 0 2px 5px #bbb9b9;
}
.edit-comment .action-item {
  cursor: pointer;
  list-style: none;
  min-width: 80px;
  padding: 6px 8px;
  font-size: 14px;
  text-align: center;
}
.edit-comment .action-item:hover {
  background-color: #fee;
  color: #c92127;
}
.review-comment-item {
  border-bottom: 1px solid #ccc;
  padding: 15px 0;
}
.review-comment-item-title {
  display: flex;
  align-items: center;
  column-gap: 10px;
}
.review-comment-item-avatar {
  border-radius: 50%;
  font-weight: 600;
  height: 32px;
  width: 32px;
  width: 32px;
  background-color: #576908;
  color: #fff;
}
.block-info {
  display: flex;
  align-items: center;
  flex: 1;
}
.block-info__name {
  color: #333;
  font-size: 14px;
  font-weight: 600;
  margin-right: 16px !important;
}
.content-edit,
.date-time {
  display: flex;
  align-items: center;
  font-size: 13px;
  column-gap: 6px;
  color: #333;
  margin-right: 10px;
}
.comment-item-review {
  margin-left: 40px;
  margin-top: 4px;
}
.comment-content {
  font-size: 14px;
  margin-top: 4px;
}

.preview-product .container {
  padding: 16px !important;
  background-color: #fff;
  border-radius: 4px;
  padding-bottom: 32px !important;
}
.preview__rating {
  padding: 8px;
}
.preview-product .preview__title h2 {
  font-size: 16px !important;
  font-weight: bold;
  margin-bottom: 18px !important;
}
.preview-product .preview__rating-point {
  display: flex;
  height: 100%;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  row-gap: 4px;
  border-right: 1px solid #ccc;
}
.preview__rating-point p {
  font-size: 20px;
  font-weight: bold;
}
.count-preview p {
  font-size: 14px;
  font-weight: 500;
}
.item-star .star-contaier {
  column-gap: 12px;
}
.icon {
  color: rgba(145, 158, 171, 0.522);
  font-size: 13px;
}

.box-review .rating-level {
  column-gap: 16px;
}
.rating-level .star-count {
  display: flex;
  column-gap: 2px;
  font-size: 14px !important;
  align-items: center;
}
.is-active {
  color: #ffbf00;
}
.rating-level-icon {
  font-size: 12px;
}
.progress {
  height: 10px !important;
}
.progress .bg-primary {
  background-color: #c92127 !important;
}

.count-user-preview {
  font-size: 13px;
  color: #333;
}

.button__review-contaier {
  text-align: center;
  border-top: 1px solid #ccc;
  border-bottom: 1px solid #ccc;
  margin-top: 16px;
  padding: 32px 0;
  cursor: pointer;
}
.button__review-contaier p {
  font-size: 17px;
}
.button-review {
  display: inline-block;
  margin-top: 16px;
  padding: 8px 32px;
  border: 2px solid #c92127;
  border-radius: 8px;
  color: #c92127;
  width: 250px;
}
.button-review-icon {
  display: inline-block;
  margin-right: 4px;
}
.btn-review {
  border: none;
  outline: none;
  background-color: #fff;
  margin-left: 4px;
  color: #c92127;
}
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
.not-enough-quantity {
  font-size: 14px !important;
  color: #c92127;
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
  margin-top: 16px;
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
.main-content {
  background-color: #f0f0f0;
}
.home-product {
  background-color: #fff;
  margin-top: 16px;
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
</style>
