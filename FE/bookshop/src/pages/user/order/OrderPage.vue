<template>
  <div class="order-main-body">
    <div class="my-tabslider-order">
      <div class="page-title">
        <h1>Đơn hàng của tôi</h1>
      </div>
      <div class="tab-container-order-history-container">
      <div
        class="tab-container-order-history swiper-container swiper-container-horizontal"
      >
        <div class="swiper-wrapper">
          <div
            class="tab-history-item swiper-slide tab-history-item-active swiper-slide-active"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=All';"
            style="width: 150px"
          >
            <div class="tab-history-item-border-left"></div>
            <div class="tab-history-item-number">1</div>
            <div class="tab-history-item-text">Tất Cả</div>
            <div class="tab-history-item-border"></div>
          </div>
          <div
            class="tab-history-item swiper-slide swiper-slide-next"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=pending_payment';"
            style="width: 150px"
          >
            <div class="tab-history-item-number">0</div>
            <div class="tab-history-item-text">Chờ thanh toán</div>
            <div class="tab-history-item-border"></div>
          </div>
          <div
            class="tab-history-item swiper-slide"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=pending';"
            style="width: 150px"
          >
            <div class="tab-history-item-number">0</div>
            <div class="tab-history-item-text">Chờ xác nhận</div>
            <div class="tab-history-item-border"></div>
          </div>
          <div
            class="tab-history-item swiper-slide"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=processing';"
            style="width: 150px"
          >
            <div class="tab-history-item-number">0</div>
            <div class="tab-history-item-text">Đang xử lý</div>
            <div class="tab-history-item-border"></div>
          </div>
          <div
            class="tab-history-item swiper-slide"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=complete';"
            style="width: 150px"
          >
            <div class="tab-history-item-number">0</div>
            <div class="tab-history-item-text">Hoàn tất</div>
            <div class="tab-history-item-border"></div>
          </div>
          <div
            class="tab-history-item swiper-slide"
            onclick="location.href='https://www.fahasa.com/sales/order/history/?status=canceled';"
            style="width: 150px"
          >
            <div class="tab-history-item-number">1</div>
            <div class="tab-history-item-text">Bị hủy</div>
            <div class="tab-history-item-border"></div>
          </div>
        </div>
        <span
          class="swiper-notification"
          aria-live="assertive"
          aria-atomic="true"
        ></span>
      </div>
      <div
        class="order-history-swiper-button-prev swiper-button-disabled"
        tabindex="0"
        role="button"
        aria-label="Previous slide"
        aria-disabled="true"
        style="display: none"
      >
        <i class="fa fa-chevron-left"></i>
      </div>
      <div
        class="order-history-swiper-button-next"
        tabindex="0"
        role="button"
        aria-label="Next slide"
        aria-disabled="false"
      >
        <i class="fa fa-chevron-right"></i>
      </div>
    </div>
    </div>
    
    <div class="my-account">
      <div class="table-order-container">
        <div class="table-order-row table-order-header">
          <div class="table-order-cell">Mã đơn hàng</div>
          <div class="table-order-cell">Ngày mua</div>
          <div class="table-order-cell">Người nhận</div>
          <div class="table-order-cell">Tổng Tiền</div>
          <div class="table-order-cell">Trạng thái</div>
          <div class="table-order-cell"></div>
        </div>
        <div
          class="table-order-row"
          data-hrefs="https://www.fahasa.com/sales/order/view/order_id/7740567/"
        >
          <div class="table-order-cell">
            <div
              class="table-order-cell-content table-order-cell-content-mobile"
            >
              <div class="order-history-id">103431888</div>
              <div
                class="order-history-status-mobile"
                style="
                  background: #f3b4af;
                  color: #a90000;
                  border-color: #f3b4af;
                "
              >
                Bị hủy
              </div>
            </div>
          </div>
          <div class="table-order-cell">
            <div class="table-order-cell-content">
              <div class="order-history-date-mobile">Ngày mua:</div>
              <div>10/04/2024</div>
            </div>
          </div>
          <div class="table-order-cell" style="max-width: 250px">
            <div class="table-order-cell-content">
              <div class="order-history-person-mobile">Người nhận:</div>
              <div>Lê Quang Huy</div>
            </div>
          </div>
          <div class="table-order-cell">
            <div class="table-order-cell-content">
              <div class="order-history-total-mobile">Tổng Tiền:</div>
              <div><span class="price">105.870</span>&nbsp;đ</div>
            </div>
          </div>
          <div class="table-order-cell hidden-max-width-992">Bị hủy</div>
          <div
            class="table-order-cell table-order-link-more hidden-max-width-992"
            onclick="location.href='https://www.fahasa.com/sales/order/view/order_id/7740567/';"
          >
            Xem chi tiết
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import localStorageService from '@/js/storage/LocalStorageService';
export default {
  name: "OrderPage",
  data() {
    return {
        orders: [],
    }
  },
  computed: {
    userInfo: function(){
        return localStorageService.getItemEncodeFromLocalStorage("userInfo");
    },
  },
  methods: {
    getOrdersData(){

    },
  },
};
</script>
<style scoped>
@import url(./order.css);
</style>
