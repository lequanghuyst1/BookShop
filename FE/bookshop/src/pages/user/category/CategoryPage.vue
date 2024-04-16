<template>
  <div class="breadcrumb-shop">
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
  <div class="container">
    <div class="collection-body py-4">
      <div class="row">
        <div class="col-3">
          <div class="wrap-filter">
            <div class="layer">
              <div class="sidebar-block">
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Nhà cung cấp</span>
                    <div class="icon-controll">
                      <i class="icon-up fa-solid fa-chevron-up"></i>
                      <i class="icon-down fa-solid fa-chevron-down"></i>
                    </div>
                  </div>
                  <div class="layer-content">
                    <ul class="check-box-list">
                      <li
                        @click="(e) => e.stopPropagation()"
                        class="check-box-item"
                      >
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                    </ul>
                  </div>
                </div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Tác giả</span>
                    <div class="icon-controll">
                      <i class="icon-up fa-solid fa-chevron-up"></i>
                      <i class="icon-down fa-solid fa-chevron-down"></i>
                    </div>
                  </div>
                  <div class="layer-content">
                    <ul class="check-box-list">
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Nhã Nam</label>
                      </li>
                    </ul>
                  </div>
                </div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Loại sách</span>
                    <div class="icon-controll">
                      <i class="icon-up fa-solid fa-chevron-up"></i>
                      <i class="icon-down fa-solid fa-chevron-down"></i>
                    </div>
                  </div>
                  <div class="layer-content">
                    <ul class="check-box-list">
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Sách</label>
                      </li>
                    </ul>
                  </div>
                </div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Giá sản phẩm</span>
                    <div class="icon-controll">
                      <i class="icon-up fa-solid fa-chevron-up"></i>
                      <i class="icon-down fa-solid fa-chevron-down"></i>
                    </div>
                  </div>
                  <div class="layer-content">
                    <ul class="check-box-list">
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">Dưới 500,000đ</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">500,000đ - 1,000,000đ</label>
                      </li>
                      <li class="check-box-item">
                        <input type="checkbox" value="Nhã Đam" />
                        <label for="">1,000,000đ - 1,500,000</label>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-9">
          <div class="filter-condition">
            <select v-model="filterData.sortType" name="" id="">
              <option value="1">Mới nhất</option>
              <option value="2">Cũ nhất</option>
              <option value="3">Tên: Z-A</option>
              <option value="4">Tên: A-Z</option>
              <option value="5">Giá: Giảm dần</option>
              <option value="6">Giá: Tăng dần</option>
              <option value="7">Bán chạy nhất</option>
              <option value="8">Tồn kho: Giảm dần</option>
            </select>
            <select name="" v-model="filterData.pageSize" id="">
              <option value="12">12 sản phẩm</option>
              <option value="24">24 sản phẩm</option>
              <option value="48">24 sản phẩm</option>
            </select>
          </div>

          <div class="category-tab-product">
            <div class="tab-heading">
              <h4>Sách mới nổi bật</h4>
            </div>
            <div class="tab-content">
              <div class="row">
                <div v-for="item in products" :key="item.BookId" class="col-3">
                  <div class="product-item">
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
                      <div class="group-button">
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
              </div>
              <div id="pages" class="paging">
                <div
                  v-show="this.filterData.pageNumber !== 1"
                  class="paging-button btn-prev"
                  @click="this.filterData.pageNumber--"
                >
                  <i class="fa-solid fa-chevron-left"></i>
                </div>
                <ol>
                  <li
                    v-for="number in pages"
                    :key="number"
                    :class="{
                      'current-page': this.filterData.pageNumber === number,
                    }"
                  >
                    <span
                      style="display: block"
                      @click="this.filterData.pageNumber = number"
                      v-if="number !== '...'"
                    >
                      {{ number }}
                    </span>
                    <span style="cursor: default; display: block" v-else
                      >...</span
                    >
                  </li>
                </ol>
                <div
                  v-show="this.filterData.pageNumber !== this.totalPage"
                  class="paging-button btn-next"
                  @click="this.filterData.pageNumber++"
                >
                  <i class="fa-solid fa-chevron-right"></i>
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
import bookService from "@/utils/BookService";
import categoryService from "@/utils/CategoryService";

export default {
  name: "CategoryUserPage",
  mounted() {
    this.isShowValueOfGroupFilter();
    this.getCategoryData();
    this.getFilterData();
  },
  data() {
    return {
      category: {},
      products: [],
      totalPage: 0,
      filterData: {
        pageSize: 2,
        pageNumber: 1,
        sortType: 1,
      },
      maxPages: 4,
    };
  },
  watch: {
    "filterData.sortType": function () {
      this.getFilterData();
    },
    "filterData.pageSize": function () {
      this.getFilterData();
    },
    "filterData.pageNumber": function () {
      this.getFilterData();
    },
  },
  computed: {
    /**
     * Xét paging dạng ...
     * Author : LQHUY
     */
    pages() {
      const middle = Math.floor(this.maxPages / 2);
      let start, end;
      if (this.totalPage <= this.maxPages) {
        start = 1;
        end = this.totalPage;
      } else if (this.filterData.pageNumber <= middle) {
        start = 1;
        end = this.maxPages;
      } else if (this.filterData.pageNumber >= this.totalPage - middle) {
        start = this.totalPage - this.maxPages + 1;
        end = this.totalPage;
      } else {
        start = this.filterData.pageNumber - middle;
        end = this.filterData.pageNumber + middle;
      }

      const pages = [];
      for (let i = start; i <= end; i++) {
        pages.push(i);
      }
      if (start > 1 && this.totalPage > this.maxPages) {
        pages.unshift("...");
        pages.splice(1, 1);
        pages.unshift(1);
      }
      if (end < this.totalPage) {
        pages.push("...");
        pages.splice(this.totalPage - 1, 1);
        pages.push(this.totalPage);
      }

      return pages;
    },
  },
  methods: {
    isShowValueOfGroupFilter() {
      const filter = document.querySelectorAll(".group-filter");
      filter.forEach((item) => {
        item.onclick = () => {
          let isOpen = item.getAttribute("aria-expanded");
          if (isOpen === "false") {
            item.setAttribute("aria-expanded", "true");
          } else {
            item.setAttribute("aria-expanded", "false");
          }
        };
      });
    },
    async getCategoryData() {
      try {
        const res = await categoryService.getBySlug(this.$route.params.slug);
        if (res.status === 200) {
          this.category = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getProductsData() {
      try {
        const res = await bookService.getByCategorySlug(
          this.$route.params.slug
        );
        if (res.status === 200) {
          this.products = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getFilterData() {
      try {
        const params = {
          pageSize: this.filterData.pageSize,
          pageNumber: this.filterData.pageNumber,
          sortType: this.filterData.sortType,
        };
        const res = await bookService.getFilter({ params });
        if (res.status === 200) {
          this.products = res.data.Data;
          this.totalPage = res.data.TotalPage;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
#pages {
  padding: 15px;
  color: black;
  display: flex;
  justify-content: center;
  align-items: center;
}
#pages ol {
  display: flex;
  align-items: center;
  margin: 0 !important;
  padding: 0 !important;
}
#pages ol li {
  display: inline-block;
  border-radius: 5px;
  line-height: 40px;
  color: black;
  cursor: pointer;
  user-select: none;
  height: 40px;
  width: 40px;
  text-align: center;
  margin: 0 2px;
}
.paging-button {
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 5px;
  line-height: 40px;
  color: #f79622;
  cursor: pointer;
  font-size: 18px;
  user-select: none;
  height: 40px;
  width: 40px;
  text-align: center;
  margin: 0 8px;
}
.paging-button:hover {
  background: #fcdab0;
}
#pages li.current-page {
  background: #c92127;
  color: #fff;
}

.group-filter {
  background-color: #fff;
  border-radius: 4px;
  border-bottom: 1px solid #eee;
}
.layer .layer-subtitle {
  text-transform: uppercase;
  cursor: pointer;
  position: relative;
  padding: 13px 0;
  line-height: 24px;
  font-size: 16px;
  color: rgba(0, 0, 0, 0.87);
  font-weight: bold;
  cursor: pointer;
}
.group-filter .layer-content {
  display: none;
}
.sidebar-block {
  background: #fff;
  padding: 15px;
  margin-bottom: 15px;
}
.group-filter[aria-expanded="true"] .layer-content {
  height: auto;
  border-bottom: 1px solid #eee;
  display: block;
}
.icon-controll {
  font-size: 12px;
}

.group-filter[aria-expanded="true"] .icon-down {
  display: none;
}

.group-filter[aria-expanded="false"] .icon-up {
  display: none;
}
.check-box-list {
  margin: 0 0 10px;
  padding: 0;
  list-style-type: none;
}
.check-box-list .check-box-item {
  line-height: 20px;
  font-size: 12px;
  margin-bottom: 12px;
  display: flex;
  align-items: center;
}
.check-box-list .check-box-item input[type="checkbox"] {
  -webkit-appearance: none;
  border: 2px solid #cacece;
  padding: 9px;
  border-radius: 4px;
  display: inline-block;
  position: relative;
  top: 3px;
  height: 0;
}
.check-box-list .check-box-item input[type="checkbox"]:checked {
  /* background-color: #ffffff;
  border: 1px solid #50b83c;
  color: white; */
  background-color: #50b83c;
  border: 2px solid #50b83c;
  color: white;
}
.check-box-list .check-box-item input[type="checkbox"]:checked::after {
  display: block;
  position: absolute;
  content: "\2714";
  font-size: 18px;
  top: calc(-3px / 2);
  left: calc(4px / 2);
}

.check-box-item label {
  display: inline-block;
  cursor: pointer;
  line-height: 18px;
  margin-bottom: 0;
  color: #4b4848;
  vertical-align: top;
  margin-left: 8px;
  transform: translateY(2px);
  font-size: 14px;
}
.check-box-item:hover label {
  color: #ff8c05;
  cursor: pointer;
}
.banner-item-picture {
  width: 100%;
}

.wrap-product {
  padding: 10px 18px;
  background-color: #fff;
  margin-bottom: 10px;
  border-radius: 4px;
}
.widget-title {
  font-size: 16px;
  font-weight: 600;
  padding-bottom: 10px;
  border-bottom: 2px solid #eeeeee;
  position: relative;
}
.widget-title::before {
  display: block;
  content: "";
  width: 100px;
  height: 2px;
  position: absolute;
  left: 0;
  bottom: -1px;
  background-color: #417505;
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
  background-color: #fff;
  padding: 15px;
}
.group-button {
  width: 100%;
  position: absolute;
  bottom: 10px;
  justify-content: center;
  column-gap: 6px;
  display: none;
}
.btn-action {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  border: none;
  background-color: #f5f5f5;
  font-size: 14px;
  outline: none;
}
.btn-action a {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #777;
  border-radius: 50%;
  text-decoration: none;
  border: 1px solid #fff;
}
.btn-action:hover a {
  background-color: #333;
  color: #fff;
  transition: all 0.3s;
}
/* .product-image {
  padding-top: 160%;
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center center;
  position: relative;
} */
</style>
