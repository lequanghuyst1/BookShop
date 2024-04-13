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
              <div class="tab-history-item-number">{{ orders.length }}</div>
              <div class="tab-history-item-text">Tất Cả</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide swiper-slide-next"
              onclick="location.href='https://www.fahasa.com/sales/order/history/?status=pending_payment';"
              style="width: 150px"
            >
              <div class="tab-history-item-number">0</div>
              <div class="tab-history-item-text">Chờ xác nhận</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              onclick="location.href='https://www.fahasa.com/sales/order/history/?status=pending';"
              style="width: 150px"
            >
              <div class="tab-history-item-number">0</div>
              <div class="tab-history-item-text">Đã xác nhận</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              onclick="location.href='https://www.fahasa.com/sales/order/history/?status=processing';"
              style="width: 150px"
            >
              <div class="tab-history-item-number">0</div>
              <div class="tab-history-item-text">Đang vận chuyển</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              onclick="location.href='https://www.fahasa.com/sales/order/history/?status=complete';"
              style="width: 150px"
            >
              <div class="tab-history-item-number">0</div>
              <div class="tab-history-item-text">Đã vận chuyển</div>
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
        <!-- <div
          class="order-history-swiper-button-next"
          tabindex="0"
          role="button"
          aria-label="Next slide"
          aria-disabled="false"
        >
          <i class="fa fa-chevron-right"></i>
        </div> -->
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
          v-for="order in orders"
          :key="order.OrderId"
        >
          <div class="table-order-cell">
            <div
              class="table-order-cell-content table-order-cell-content-mobile"
            >
              <div class="order-history-id">{{ order.OrderCode }}</div>
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
              <div>{{ this.$helper.formatDate(order.OrderDate) }}</div>
            </div>
          </div>
          <div class="table-order-cell" style="max-width: 250px">
            <div class="table-order-cell-content">
              <div class="order-history-person-mobile">Người nhận:</div>
              <div>{{ order.Fullname }}</div>
            </div>
          </div>
          <div class="table-order-cell">
            <div class="table-order-cell-content">
              <div class="order-history-total-mobile">Tổng Tiền:</div>
              <div>
                <span class="price">{{ order.TotalAmount }}</span
                >&nbsp;đ
              </div>
            </div>
          </div>
          <div class="table-order-cell hidden-max-width-992">
            {{ order.Status }}
          </div>
          <div
            class="table-order-cell table-order-link-more hidden-max-width-992"
            @click="goToOrderDetail(order.OrderId)"
          >
            Xem chi tiết
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import localStorageService from "@/js/storage/LocalStorageService";
import orderService from "@/utils/OrderService";
export default {
  name: "OrderPage",
  created() {
    this.getOrdersData();
  },
  data() {
    return {
      //Lưu danh sách các đơn hàng đã đặt
      orders: [],
      //Lưu giá trị tổng số đơn hàng bị hủy
      quantityOrderCancelled: 0,
      ordersCancelled: [],
      orderStatus: null,
    };
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemEncodeFromLocalStorage("userInfo");
    },
  },
  methods: {
    /**
     * Hàm thực hiện lấy danh sách các bản ghi
     * @author LQHUY(12/04/2024)
     */
    async getOrdersData() {
      try {
        const res = await orderService.GetByUserId(this.userInfo.UserId);
        if (res.status === 200) {
          switch (this.orderStatus) {
            case this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION:
              this.orders = res.data.filter(
                (item) => item.Status === "Chờ xác nhận"
              );
              break;
            default:
              this.orders = res.data;
              break;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    goToOrderDetail(orderId) {
      this.$router.push("order/order-detail/" + orderId);
    },
  },
};
</script>
<style scoped>
@import url(./order.css);
</style>
