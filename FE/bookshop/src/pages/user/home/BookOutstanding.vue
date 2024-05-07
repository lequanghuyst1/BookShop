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
              <i style="color: #c92127" class="fa-solid fa-house me-1"></i>
              <a href="/" target="_self" itemprop="item"
                ><span itemprop="name">Trang chủ</span></a
              >
              <meta itemprop="position" content="1" />
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
                ><span itemprop="name">Sách giảm sốc</span></span
              >
              <meta itemprop="position" content="3" />
            </li>
            <!-- <li
                class="active"
                itemprop="itemListElement"
                itemscope=""
                itemtype="http://schema.org/ListItem"
              >
                <span
                  itemprop="item"
                  content="https://www.vinabook.com/collections/sach-van-hoc-nuoc-ngoai"
                  ><span itemprop="name">{{ productInfo.Category }}</span></span
                >
                <meta itemprop="position" content="3" />
              </li> -->
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
                <div class="filter-by">Lọc theo</div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Danh mục</span>
                  </div>
                  <div class="layer-content">
                    <ul
                      class="check-box-list"
                      :style="{
                        maxHeight: isShowMoreCategoryFilter
                          ? 'max-content'
                          : '300px',
                        overflow: isShowMoreCategoryFilter
                          ? 'visible'
                          : 'hidden',
                      }"
                    >
                      <li
                        class="check-box-item"
                        v-for="(item, index) in categories"
                        :key="index"
                        @change="hanldeChangeCategory(item, index)"
                      >
                        <input
                          v-model="categoriesChecked[index]"
                          type="checkbox"
                          :value="item.CategoryName"
                          :id="'category' + index"
                        />
                        <label :for="'category' + index">{{
                          item.CategoryName
                        }}</label>
                      </li>
                    </ul>
                    <p
                      @click="
                        this.isShowMoreCategoryFilter =
                          !this.isShowMoreCategoryFilter
                      "
                      class="narrow-by-list"
                    >
                      {{
                        isShowMoreCategoryFilter ? "Hiện ít lại" : "Xem thêm"
                      }}
                    </p>
                  </div>
                </div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Nhà cung cấp</span>
                  </div>
                  <div class="layer-content">
                    <ul
                      class="check-box-list"
                      :style="{
                        maxHeight: isShowMorePublisherFilter
                          ? 'max-content'
                          : '300px',
                        overflow: isShowMorePublisherFilter
                          ? 'visible'
                          : 'hidden',
                      }"
                    >
                      <li
                        class="check-box-item"
                        v-for="(item, index) in publishers"
                        :key="index"
                        @change="handleChangePublisher(item, index)"
                      >
                        <input
                          v-model="publishersChecked[index]"
                          type="checkbox"
                          :value="item"
                          :id="'publisher' + index"
                        />
                        <label :for="'publisher' + index">{{ item }}</label>
                      </li>
                    </ul>
                    <p
                      @click="
                        this.isShowMorePublisherFilter =
                          !this.isShowMorePublisherFilter
                      "
                      class="narrow-by-list"
                    >
                      {{
                        isShowMorePublisherFilter ? "Hiện ít lại" : "Xem thêm"
                      }}
                    </p>
                  </div>
                </div>
                <div class="group-filter" aria-expanded="false">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Tác giả</span>
                  </div>
                  <div class="layer-content">
                    <ul
                      ref="authorFilter"
                      class="check-box-list"
                      :style="{
                        maxHeight: isShowMoreAuthorFilter
                          ? 'max-content'
                          : '300px',
                        overflow: isShowMoreAuthorFilter ? 'visible' : 'hidden',
                      }"
                    >
                      <li
                        v-for="(item, index) in authors"
                        :key="item.BookId"
                        class="check-box-item"
                      >
                        <input
                          :id="'author' + index"
                          v-model="authorsChecked[index]"
                          type="checkbox"
                          @change="hanldeChangeAuthor(item, index)"
                          :value="item"
                        />
                        <label :for="'author' + index">{{ item }}</label>
                      </li>
                    </ul>
                    <p
                      @click="
                        this.isShowMoreAuthorFilter =
                          !this.isShowMoreAuthorFilter
                      "
                      class="narrow-by-list"
                    >
                      {{ isShowMoreAuthorFilter ? "Hiện ít lại" : "Xem thêm" }}
                    </p>
                  </div>
                </div>

                <div class="group-filter">
                  <div class="layer-subtitle d-flex justify-content-between">
                    <span>Giá sản phẩm</span>
                  </div>
                  <div class="layer-content">
                    <ul class="check-box-list">
                      <li
                        class="check-box-item"
                        v-for="(range, index) in priceRanges"
                        :key="index"
                      >
                        <input
                          type="checkbox"
                          v-model="range.checked"
                          @change="handleChangePriceRange(range)"
                          :value="range"
                          :id="'range-' + index"
                        />
                        <label :for="'range-' + index">{{ range.label }}</label>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-9">
          <div class="category-tab-product">
            <div class="tab-content">
              <div
                v-if="
                  this.filterData.rangeColumn.length > 0 ||
                  this.filterData.filterInput.length > 0
                "
                class="filter-condition"
              >
                <ul>
                  <p
                    style="
                      font-size: 14px;
                      margin-right: 12px !important;
                      display: inline-block;
                    "
                  >
                    Lọc theo:
                  </p>
                  <li
                    v-for="(item, index) in this.filterData.rangeColumn"
                    :key="index"
                  >
                    <span>Giá: {{ item.label }}</span>
                    <div class="icon-close">
                      <i class="fa-solid fa-xmark"></i>
                    </div>
                  </li>
                  <li
                    v-for="(item, index) in this.filterData.filterInput"
                    :key="index"
                  >
                    <span
                      >{{
                        item.ColumnName === "Author"
                          ? "Tác giả"
                          : item.ColumnName === "CategoryName"
                          ? "Danh mục"
                          : "Nhà xuất bản"
                      }}: {{ item.Value }}</span
                    >
                    <div class="icon-close">
                      <i class="fa-solid fa-xmark"></i>
                    </div>
                  </li>
                  <li
                    @click="handleRemoveAllFilter"
                    class="delete-all-category"
                  >
                    <a><div>Xóa bộ lọc</div></a>
                  </li>
                </ul>
              </div>
              <div class="filter-wrap">
                <!-- <p class="filter-title">Sắp xếp theo:</p>
                  <select v-model="filterData.sortColumn" name="" id="">
                    <option
                      v-for="(option, index) in sortOptions"
                      :key="index"
                      :value="option"
                    >
                      {{ option.label }}
                    </option>
                  </select> -->
                <p class="filter-title">Số lượng hiển thị:</p>
                <select name="" v-model="filterData.pageSize" id="">
                  <option value="12">12 sản phẩm</option>
                  <option value="24">24 sản phẩm</option>
                  <option value="48">48 sản phẩm</option>
                </select>
              </div>
              <div class="row">
                <div v-if="checkDataIsValidConditions" class="col-12 mt-1">
                  <p class="note-msg" style="">
                    Không có sản phẩm phù hợp với từ khóa tìm kiếm của bạn.
                  </p>
                </div>
                <div v-if="isDataEmpty" class="col-12 mt-1">
                  <p class="note-msg" style="">
                    Hiện chưa có sản phẩm nào trong danh mục này.
                  </p>
                </div>
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
              <div v-if="this.products.length > 0" id="pages" class="paging">
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
                  v-show="
                    this.filterData.pageNumber !== this.totalPage &&
                    this.products.length > 0
                  "
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
import { mapGetters, mapActions } from "vuex";
export default {
  name: "CategoryUserPage",
  mounted() {
    this.getFieldToFilterData();
    this.handleFilterData();
  },
  data() {
    return {
      isShowMorePublisherFilter: false,
      isShowMoreAuthorFilter: false,
      isShowMoreCategoryFilter: false,
      //Lưu trữ thông tin chi tiết catgory
      category: {},
      categories: [],
      //Lưu trữ danh sách product
      products: [],
      //Lưu trữ tổng số bản ghi
      totalPage: 0,
      totalRecord: 0,
      //Lưu trữ phân trang tối đa được hiện
      maxPages: 4,
      //Lưu trữ danh sách các tác giả được chọn
      authorsChecked: [],
      //Lưu trữ danh sách các nhà xuất bản được chọn
      publishersChecked: [],
      categoriesChecked: [],
      //Lưu trữ danh sách các tác giả
      authors: [],
      //Lưu trữ danh sách các nhà xuất bản
      publishers: [],
      //param filter dữ liệu
      filterData: {
        pageSize: 24,
        pageNumber: 1,
        sortColumn: {
          ColumnName: "CreatedDate",
          Type: this.$Enum.SORT_TYPE.DESC,
        },
        rangeColumn: [],
        filterInput: [],
      },

      //Danh sách khoảng giá lọc
      priceRanges: [
        {
          ColumnName: "Price",
          MinValue: 0,
          MaxValue: 150000,
          checked: false,
          label: "0đ - 150.000đ",
        },
        {
          ColumnName: "Price",
          MinValue: 150000,
          MaxValue: 300000,
          checked: false,
          label: "150.000đ - 300.000đ",
        },
        {
          ColumnName: "Price",
          MinValue: 300000,
          MaxValue: 500000,
          checked: false,
          label: "300.000đ - 500.000đ",
        },
        {
          ColumnName: "Price",
          MinValue: 500000,
          MaxValue: 700000,
          checked: false,
          label: "500.000đ - 700.000đ",
        },
        {
          ColumnName: "Price",
          MinValue: 700000,
          checked: false,
          label: "700.000đ trở lên",
        },
      ],
      isDataEmpty: false,
    };
  },
  watch: {
    //theo dõi biến sortColumn
    "filterData.sortColumn": function () {
      this.handleFilterData();
    },
    //theo dõi biến pageSize
    "filterData.pageSize": function () {
      this.handleFilterData();
    },
    //theo dõi biến pageNumber
    "filterData.pageNumber": function () {
      this.handleFilterData();
    },
    //theo dõi biến rangeColumn
    "filterData.rangeColumn": function () {
      this.handleFilterData();
    },
    //theo dõi biến filterInput
    "filterData.filterInput": function () {
      this.handleFilterData();
      console.log(this.filterData.filterInput);
    },
  },
  computed: {
    ...mapGetters(["searchString"]),
    /**
     * Xét paging dạng ...
     * Author : LQHUY(16/04/2024)
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
    //biến kiểm tra có giá trị nào thỏa mãn kiều kiện lọc không
    checkDataIsValidConditions: function () {
      return (
        (this.filterData.filterInput.length > 0 &&
          this.products.length === 0) ||
        (this.filterData.rangeColumn.length > 0 && this.products.length === 0)
      );
    },
  },
  methods: {
    ...mapActions(["setSearchStringDefault", "setSearchString"]),
    checkDataIsNotEmpty() {
      return (
        this.filterData.filterInput.length === 0 &&
        this.products.length === 0 &&
        this.filterData.rangeColumn.length === 0
      );
    },
    /**
     * Hàm thực hiện lấy danh sách các trường chưa dữ liệu để lọc
     * @author LQHUY(16/04/2024)
     */
    async getFieldToFilterData() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const filter = this.filterData;

        const res = await bookService.getFilter(filter);
        if (res.status === 200) {
          //lấy ra các danh mục khác trong trong danh sách product
          this.categories = res.data.Data.filter(
            (obj, index, self) =>
              index ===
              self.findIndex((t) => t.CategoryName === obj.CategoryName)
          );
          this.authors = res.data.Data.filter(
            (obj, index, self) =>
              index === self.findIndex((t) => t.Author === obj.Author)
          ).map((item) => item.Author);
          this.publishers = res.data.Data.filter(
            (obj, index, self) =>
              index ===
              self.findIndex((t) => t.PublisherName === obj.PublisherName)
          ).map((item) => item.PublisherName);
          this.$emitter.emit("toggleShowLoading", false, 300);
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },
    /**
     * Hàm thực hiện filter theo điều kiện và lấy ra các bản ghi
     * @author LQHUY(16/04/2024)
     */
    async handleFilterData() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const searchString =
          this.$route.params.searchString === "null"
            ? null
            : this.$route.params.searchString;
        const params = {
          PageSize: this.filterData.pageSize,
          PageNumber: this.filterData.pageNumber,
          SearchString: searchString,
          SortColumn: this.filterData.sortColumn,
          RangeColumn: this.filterData.rangeColumn,
          FilterInput: this.filterData.filterInput,
        };
        const searchParams = new URLSearchParams(params);
        let url = `${window.location.origin}${
          window.location.pathname
        }?${searchParams.toString()}`;
        const res = await bookService.getFilter(params);
        if (res.status === 200) {
          this.products = res.data.Data;
          this.totalPage = res.data.TotalPage;
          this.totalRecord = res.data.TotalRecord;
          this.$emitter.emit("toggleShowLoading", false, 300);
          this.isDataEmpty = this.checkDataIsNotEmpty();
        }
        history.replaceState({}, "", url);
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Hàm thực hiện thêm hoặc xóa bỏ điều liện lọc của publisher
     * @param {string} item
     * @param {number} index
     * @author LQHUY(16/04/2024)
     */
    handleChangePublisher(item, index) {
      if (this.publishersChecked[index]) {
        // Nếu publisher được chọn, thêm điều kiện lọc vào filterData.filterInput
        this.filterData.filterInput.push({
          ColumnName: "PublisherName",
          Value: item,
        });
        this.filterData.filterInput = [...this.filterData.filterInput];
      } else {
        // Kiểm tra xem giá trị hủy nằm ở vị trí nào trong mảng
        const filterInputIndex = this.filterData.filterInput.findIndex(
          (ele) => {
            return ele.Value === item && ele.ColumnName === "PublisherName";
          }
        );
        //nếu tổn tại thì loại bỏ điều kiện lọc khớp với tên publisher khỏi filterData.filterInput
        if (filterInputIndex !== -1) {
          this.filterData.filterInput.splice(filterInputIndex, 1);
        }
        this.filterData.filterInput = [...this.filterData.filterInput];
      }
    },

    /**
     * Hàm thực hiện thêm hoặc xóa bỏ điều liện lọc của author
     * @param {string} item
     * @param {number} index
     * @author LQHUY(16/04/2024)
     */
    hanldeChangeAuthor(item, index) {
      //kiểm tra xem có checkbox có được chọn hay không
      if (this.authorsChecked[index]) {
        // Nếu tác giả được chọn, thêm điều kiện lọc vào filterData.filterInput
        this.filterData.filterInput.push({
          ColumnName: "Author",
          Value: item,
        });
        this.filterData.filterInput = [...this.filterData.filterInput];
      } else {
        // kiểm tra xem giá trị hủy nằm ở vị trí nào trong mảng
        const filterInputIndex = this.filterData.filterInput.findIndex(
          (ele) => {
            return ele.Value === item && ele.ColumnName === "Author";
          }
        );
        //nếu tồn tại loại bỏ điều kiện lọc khớp với tên tác giả khỏi filterData.filterInput
        if (filterInputIndex !== -1) {
          this.filterData.filterInput.splice(filterInputIndex, 1);
        }
        this.filterData.filterInput = [...this.filterData.filterInput];
      }
    },

    /**
     * Hàm thực hiện thêm hoặc xóa các khoảng giá trị của giá tiền vào rangeColumn
     * @param {object} range
     * @author LQHUY(16/04/2024)
     */
    handleChangePriceRange(range) {
      if (range.checked) {
        this.filterData.rangeColumn.push(range);
        this.filterData.rangeColumn = [...this.filterData.rangeColumn];
      } else {
        const index = this.filterData.rangeColumn.findIndex(
          (item) =>
            item.ColumnName === range.ColumnName &&
            item.MinValue === range.MinValue &&
            item.MaxValue === range.MaxValue
        );
        if (index !== -1) {
          this.filterData.rangeColumn.splice(index, 1);
          this.filterData.rangeColumn = [...this.filterData.rangeColumn];
        }
      }
    },

    /**
     * Hàm thực hiện thêm hoặc xóa bỏ điều liện lọc của publisher
     * @param {string} item
     * @param {number} index
     * @author LQHUY(16/04/2024)
     */

    hanldeChangeCategory(item, index) {
      if (this.categoriesChecked[index]) {
        // Nếu publisher được chọn, thêm điều kiện lọc vào filterData.filterInput
        this.filterData.filterInput.push({
          ColumnName: "CategoryName",
          Value: item.CategoryName,
        });
        this.filterData.filterInput = [...this.filterData.filterInput];
      } else {
        // Kiểm tra xem giá trị hủy nằm ở vị trí nào trong mảng
        const filterInputIndex = this.filterData.filterInput.findIndex(
          (ele) => {
            return (
              ele.Value === item.CategoryName &&
              ele.ColumnName === "CategoryName"
            );
          }
        );
        //nếu tổn tại thì loại bỏ điều kiện lọc khớp với tên publisher khỏi filterData.filterInput
        if (filterInputIndex !== -1) {
          this.filterData.filterInput.splice(filterInputIndex, 1);
        }
        this.filterData.filterInput = [...this.filterData.filterInput];
      }
    },

    /**
     * Hàm thực hiện xóa tất cả bộ lọc khi click btn xóa bộ lọc
     * @author LQHUY(17/04/2024)
     */
    handleRemoveAllFilter() {
      // if (this.filterData.rangeColumn.length > 0) {
      //   this.filterData.rangeColumn = [];

      // }
      // if (this.filterData.filterInput.length > 0) {
      //   this.filterData.filterInput = [];
      //   this.authorsChecked = [];
      //   this.publishersChecked = []
      // }
      location.reload();
    },
  },
};
</script>
<style scoped>
.narrow-by-list {
  color: #c92127;
  cursor: pointer;
  display: block;
  text-align: center;
  font-weight: bold;
  margin-bottom: 8px !important;
}
.note-msg,
.notice-msg {
  border-color: #fcd344;
  background-color: #fafaec;
  color: #3d6611;
  padding: 15px !important;
  margin-top: 10px;
  font-size: 14px;
  border-radius: 6px;
}

.delete-all-category {
  display: flex;
  cursor: pointer;
  font-size: 14px;
  align-items: center;
  column-gap: 6px;
  vertical-align: top;
  text-align: left;
  height: 40px;
  padding: 0 12px;
  border-radius: 6px;
  color: #f7941e;
  margin-right: 12px;
  border: 1px solid #f7941e;
  background: none !important;
  display: inline-block;
}
.filter-wrap {
  padding: 15px 0 20px 0;
  border-bottom: 1px solid #ecebeb;
  margin-bottom: 10px;
  display: flex;
  align-items: center;
  column-gap: 20px;
}
.filter-title {
  font-size: 14px;
  display: inline-block;
}
.filter-by {
  color: #6a6a6a;
  position: relative;
  font-size: 20px;
  font-weight: 600;
  border-bottom: 2px solid #f6f6f6;
  color: #c92127;
  font-size: 20px;
  cursor: pointer;
  padding: 16px 0;
  text-transform: uppercase;
}
.filter-wrap select {
  padding: 8px 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
  font-size: 14px;
  outline: none;
}
.filter-condition {
  padding: 15px 0 10px 0;
  margin-bottom: 10px;
  display: inline-block;
}
.filter-condition ul {
  padding: 0 !important;
  margin: 0 !important;
  list-style: none;
  flex-wrap: wrap;
  display: flex;
  row-gap: 10px;
  align-items: center;
}
.filter-condition ul li {
  display: flex;
  font-size: 14px;
  align-items: center;
  column-gap: 6px;
  vertical-align: top;
  text-align: left;
  height: 40px;
  padding: 0 12px;
  border-radius: 6px;
  color: #f7941e;
  background-color: rgba(247, 148, 30, 0.1);
  margin-right: 12px;
}
.filter-condition ul li .icon-close {
  font-size: 17px;
}
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
  margin: 0 !important;
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
  padding: 7px;
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
  background-color: #f7941e;
  border: 2px solid #f7941e;
  color: white;
}
.check-box-list .check-box-item input[type="checkbox"]:checked::after {
  display: block;
  position: absolute;
  content: "\2714";
  font-size: 14px;
  top: calc(-6px / 2);
  left: calc(3px / 2);
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
  letter-spacing: 0.4px;
  position: relative;
  display: inline-block;
  font-size: 18px;
  color: #333;
  font-weight: bold;
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
