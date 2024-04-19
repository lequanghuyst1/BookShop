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
              class="tab-history-item tab-history-item-active swiper-slide swiper-slide-active"
              @click="getOrdersData()"
              style="width: 150px"
            >
              <div class="tab-history-item-border-left"></div>
              <div class="tab-history-item-number">{{ quantityOrder }}</div>
              <div class="tab-history-item-text">Tất Cả</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide swiper-slide-next"
              @click="
                getOrdersData(this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION)
              "
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderWatting }}
              </div>
              <div class="tab-history-item-text">Chờ xác nhận</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              @click="getOrdersData(this.$Enum.ORDER_STATUS.CONFIRMED)"
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderConfirmed }}
              </div>
              <div class="tab-history-item-text">Đã xác nhận</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              @click="getOrdersData(this.$Enum.ORDER_STATUS.SHIPPING)"
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderShipping }}
              </div>
              <div class="tab-history-item-text">Đang vận chuyển</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              @click="getOrdersData(this.$Enum.ORDER_STATUS.DELIVERED)"
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderDelivered }}
              </div>
              <div class="tab-history-item-text">Đã vận chuyển</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              @click="getOrdersData(this.$Enum.ORDER_STATUS.COMPLETE)"
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderComplete }}
              </div>
              <div class="tab-history-item-text">Hoàn tất</div>
              <div class="tab-history-item-border"></div>
            </div>
            <div
              class="tab-history-item swiper-slide"
              @click="getOrdersData(this.$Enum.ORDER_STATUS.CANCELLED)"
              style="width: 150px"
            >
              <div class="tab-history-item-number">
                {{ quantityOrderCancelled }}
              </div>
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
                <span class="price">{{
                  this.$helper.formatMoney(order.TotalAmount)
                }}</span
                >&nbsp;đ
              </div>
            </div>
          </div>
          <div class="table-order-cell hidden-max-width-992">
            {{ orderStatusString(order.OrderStatus) }}
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
  mounted() {
    document.addEventListener("click", function (e) {
      if (e.target.closest(".tab-container-order-history-container")) {
        const tabHistory = document.querySelectorAll(".tab-history-item");
        const tabActive = document.querySelector(
          ".tab-history-item.tab-history-item-active"
        );
        tabHistory.forEach((item) => {
          item.onclick = function () {
            tabActive.classList.remove("tab-history-item-active");
            item.classList.add("tab-history-item-active");
          };
        });
      }
    });
  },
  data() {
    return {
      //Lưu danh sách các đơn hàng đã đặt
      orders: [],
      //Lưu giá trị tổng số đơn hàng bị hủy
      quantityOrderCancelled: 0,
      quantityOrder: 0,
      quantityOrderWatting: 0,
      quantityOrderConfirmed: 0,
      quantityOrderShipping: 0,
      quantityOrderDelivered: 0,
      quantityOrderComplete: 0,
      ordersCancelled: [],
      orderStatus: null,
    };
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
  },
  methods: {
    /**
     * Hàm thực hiện lấy danh sách các bản ghi
     * @author LQHUY(12/04/2024)
     */
    async getOrdersData(orderStatus) {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const res = await orderService.GetByUserId(this.userInfo.UserId);
        if (res.status === 200) {
          switch (orderStatus) {
            case this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION:
              this.orders = res.data.filter(
                (item) =>
                  item.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
              );
              break;
            case this.$Enum.ORDER_STATUS.CONFIRMED:
              this.orders = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CONFIRMED
              );
              break;
            case this.$Enum.ORDER_STATUS.SHIPPING:
              this.orders = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.SHIPPING
              );
              break;
            case this.$Enum.ORDER_STATUS.DELIVERED:
              this.orders = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.DELIVERED
              );
              break;
            case this.$Enum.ORDER_STATUS.COMPLETE:
              this.orders = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.COMPLETE
              );
              break;
            case this.$Enum.ORDER_STATUS.CANCELLED:
              this.orders = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
              );
              break;
            default:
              this.orders = res.data;
              this.quantityOrder = this.orders.length;
              this.quantityOrderWatting = res.data.filter(
                (item) =>
                  item.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
              ).length;
              this.quantityOrderConfirmed = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CONFIRMED
              ).length;
              this.quantityOrderShipping = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.SHIPPING
              ).length;
              this.quantityOrderDelivered = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.DELIVERED
              ).length;
              this.quantityOrderCancelled = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
              ).length;
              this.quantityOrderComplete = res.data.filter(
                (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.COMPLETE
              ).length;
              break;
          }
          this.$emitter.emit("toggleShowLoading", false);
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    goToOrderDetail(orderId) {
      this.$router.push("order/order-detail/" + orderId);
    },

    orderStatusString(orderStatus) {
      switch (orderStatus) {
        case this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION:
          return this.resource.ENUM_ORDER_STATUS.waitForConfirmation;
        case this.$Enum.ORDER_STATUS.CONFIRMED:
          return this.resource.ENUM_ORDER_STATUS.confirmed;
        case this.$Enum.ORDER_STATUS.SHIPPING:
          return this.resource.ENUM_ORDER_STATUS.shipping;
        case this.$Enum.ORDER_STATUS.DELIVERED:
          return this.resource.ENUM_ORDER_STATUS.delivered;
        case this.$Enum.ORDER_STATUS.CANCELLED:
          return this.resource.ENUM_ORDER_STATUS.cancelled;
        default:
          return "";
      }
    },
  },
};
</script>
<style scoped>
@import url(./order.css);
</style>
