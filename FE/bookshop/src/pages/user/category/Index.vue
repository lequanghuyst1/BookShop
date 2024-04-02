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
          <div class="wrap-product">
            <div class="widget-title mb-2">
              <span>Sách bán chạy</span>
            </div>
            <div class="widget-collection-list">
              <div class="product-item d-flex column-gap-3">
                <div class="product-img">
                  <div class="procduct-discount">-10%</div>
                  <a href="/" class="product-link">
                    <img
                      class="w-100"
                      src="../../../assets/img/anh-mau.jpg"
                      alt=""
                    />
                  </a>
                </div>
                <div class="product-content">
                  <div class="product-title">
                    <a href="/" class="product-link">
                      Sự ỷ lại nguy hiểm Sự ỷ lại nguy hiểm
                    </a>
                  </div>
                  <div class="product-price d-flex flex-wrap">
                    <div class="product-pirce--discount me-2">46,000đ</div>
                    <div
                      class="product-pirce--original text-decoration-line-through"
                    >
                      79,000đ
                    </div>
                  </div>
                </div>
              </div>
              <div class="product-item d-flex column-gap-3">
                <div class="product-img">
                  <div class="procduct-discount">-10%</div>
                  <a href="/" class="product-link">
                    <img
                      class="w-100"
                      src="../../../assets/img/anh-mau.jpg"
                      alt=""
                    />
                  </a>
                </div>
                <div class="product-content">
                  <div class="product-title">
                    <a href="/" class="product-link">
                      Sự ỷ lại nguy hiểm Sự ỷ lại nguy hiểm
                    </a>
                  </div>
                  <div class="product-price d-flex flex-wrap">
                    <div class="product-pirce--discount me-2">46,000đ</div>
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
        </div>
        <div class="col-9">
          <div class="home-slider w-100 mb-5">
            <div class="image">
              <a href="">
                <img
                  class="w-100"
                  src="https://theme.hstatic.net/200000845405/1001198377/14/home_slider_image_2.jpg?v=437"
                  alt=""
                />
              </a>
            </div>
          </div>
  
          <div class="category-tab-product">
            <div class="tab-heading">
              <h4>Sách mới nổi bật</h4>
            </div>
            <div class="tab-content">
              <div class="row">
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <div class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <a href="/" class="d-block">
                        <img
                          src="https://product.hstatic.net/200000845405/product/nh_ng-c_-g_i-m_t-t_ch----b_a-1_cb8ed626604c47429f44d721891cda29_medium.jpg"
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
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/nh_ng-c_-g_i-m_t-t_ch----b_a-1_cb8ed626604c47429f44d721891cda29_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
                        <div
                          class="product-pirce--original text-decoration-line-through"
                        >
                          79,000đ
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-3">
                  <div class="product-item">
                    <a href="/" class="product-image d-block text-center">
                      <div class="product-sale">-10%</div>
                      <img
                        src="https://product.hstatic.net/200000845405/product/8935074133526_1_a7c9ab98cd0144c380544f534323fb62_medium.jpg"
                        alt=""
                      />
                    </a>
                    <div class="product-detail">
                      <h3 class="product-name">
                        <a href="">
                          Những cô gái bí ẩn đi tìm nơi cơ trú ngoài biển xa không
                          trở về
                        </a>
                      </h3>
                      <div class="product-price d-flex flex-wrap">
                        <div class="product-pirce--discount me-2">46,000đ</div>
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
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "CategoryUserPage",
  mounted() {
    this.isShowValueOfGroupFilter();
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
  },
};
</script>
<style scoped>

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
  /* content: "";
  width: 9px;
  height: 5px;
  top: calc(7px / 2);
  left: calc(5px / 2);
  border: 3px solid #50b83c;
  border-top: none;
  border-right: none;
  -webkit-transform: rotate(-45deg);
  -moz-transform: rotate(-45deg);
  -o-transform: rotate(-45deg);
  -ms-transform: rotate(-45deg);
  transform: rotate(-45deg); */
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
.category-tab-product .product-item {
  border: 0;
}
.product-item {
  padding: 20px 0;
  border-bottom: 1px solid #eeeeee;
}
.product-img {
  width: 70px;
  flex-shrink: 0;
  position: relative;
}
.procduct-discount {
  background: #f03737;
  color: #fff;
  padding: 3px 4px;
  font-size: 12px;
  width: 36px;
  text-align: center;
  border-radius: 2px;
  position: absolute;
  top: -12px;
  left: 0;
  z-index: 1;
}
.product-link {
  text-decoration: none;
}
.product-title {
  width: 100%;
}
.product-title a {
  color: #4b4848;
  font-weight: 600;
  font-size: 14px;
  display: block;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.product-title a:hover {
  color: #ec8000;
}
.product-content {
  width: calc(100% - 100px);
}
.product-price {
  font-size: 14px;
  margin-top: 4px;
}
.product-pirce--discount {
  color: #f6001a;
  font-size: 15px;
}
.product-pirce--original {
  color: #999;
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
.product-item .product-image {
  position: relative;
  display: block;
}
.product-sale {
  background: #f03737;
  position: absolute;
  font-size: 12px;
  line-height: 1;
  padding: 5px 10px;
  font-weight: bold;
  z-index: 1;
  color: #fff;
  border: 1px solid #ededed;
  top: 12px;
  right: 12px;
  color: #fff;
}
.product-image a img {
  max-width: 100%;
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
.category-tab-product .product-item {
  font-family: "Montserrat", sans-serif !important ;
  margin-bottom: 10px;
  position: relative;
  transition: transform linear 0.1s;
}
.category-tab-product .product-item:hover {
  transform: translateY(-4px);
}
.product-item:hover .group-button {
  display: flex;
  transition: all 0.5s;
}
.product-item:hover .product-name a {
  color: #ec8000;
}
.product-detail {
  padding: 16px 0;
}
.product-name a {
  display: block;
  text-decoration: none;
  font-weight: bold;
  font-size: 16px;
  color: #4b4848;
  font-weight: 600;
  height: 37px;
  line-height: 19px;
  overflow: hidden;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;
  text-overflow: ellipsis;
  /* white-space: nowrap;*/
}
</style>
