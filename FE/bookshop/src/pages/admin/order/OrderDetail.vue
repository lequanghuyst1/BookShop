<template>
  <div class="wrap-order-detail">
    <div :id="this.$route.params.id" class="breadcrumb-shop">
      <div class="container">
        <div @click="backToOrderManagement" class="breadcrumb-title">
          <i class="fa-solid fa-angles-left"></i>
          Quay lại
        </div>
      </div>
    </div>
    <div class="order-detail__header">
      <div class="order-detail__info">
        <div class="d-flex">
          <div class="order-code">
            <div class="order__info-title">Mã</div>
            <p>{{ order.OrderCode }}</p>
          </div>
          <div class="order-date">
            <div class="order__info-title">Ngày tạo</div>
            <p>{{ this.$helper.formatOrderDate(order.OrderDate) }}</p>
          </div>
          <div class="order-status">
            <div class="order__info-title">Tình trạng đơn hàng</div>
            <p
              :style="{
                color:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#DEB85B'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#a90000'
                    : '#a0dbb3',
                borderColor:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#DEB85B'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#a90000'
                    : '#a0dbb3',
                backgroundColor:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#fdfde8'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#f3b4af'
                    : '#f1fcf5',
              }"
            >
              {{
                this.$helper.hanldeValueTypeEnum(
                  "ORDER_STATUS",
                  order.OrderStatus
                )
              }}
            </p>
          </div>
          <div class="delivery-status">
            <div class="order__info-title">Trạng thái giao hàng</div>
            <p
              :style="{
                color:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.DELIVERIED
                    ? '#a0dbb3'
                    : '#DEB85B',
                borderColor:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.DELIVERIED
                    ? '#a0dbb3'
                    : '#DEB85B',
                backgroundColor:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.DELIVERIED
                    ? '#f1fcf5'
                    : '#fdfde8',
              }"
            >
              {{
                this.$helper.hanldeValueTypeEnum(
                  "DELIVERY_STATUS",
                  order.DeliveryStatus
                )
              }}
            </p>
          </div>

          <div class="payment-status">
            <div class="order__info-title">Trạng thái thanh toán</div>
            <p
              :style="{
                color:
                  this.order.PaymentStatus === this.$Enum.PAYMENT_STATUS.UNPAID
                    ? '#DEB85B'
                    : '#a0dbb3',
                borderColor:
                  this.order.PaymentStatus === this.$Enum.PAYMENT_STATUS.UNPAID
                    ? '#DEB85B'
                    : '#a0dbb3',
                backgroundColor:
                  this.order.PaymentStatus === this.$Enum.PAYMENT_STATUS.UNPAID
                    ? '#fdfde8'
                    : '#f1fcf5',
              }"
            >
              {{
                this.$helper.hanldeValueTypeEnum(
                  "PAYMENT_STATUS",
                  order.PaymentStatus
                )
              }}
            </p>
          </div>
        </div>
      </div>
      <div class="button-print-invoice">
        <MButton
          text="In"
          style="background-color: #0051c8"
          class="m-button m-button-icon"
          icon="fa-solid fa-print"
        ></MButton>
      </div>
    </div>
    <div class="order-detail__body">
      <div class="row">
        <div class="col-9">
          <div class="check-order-delivered">
            <p
              :style="{
                color:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.NOT_DELIVERY
                    ? '#DEB85B'
                    : '#a0dbb3',
                borderColor:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.NOT_DELIVERY
                    ? '#DEB85B'
                    : '#a0dbb3',
                backgroundColor:
                  this.order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.NOT_DELIVERY
                    ? '#fdfde8'
                    : '#f1fcf5',
              }"
            >
              {{
                this.order.DeliveryStatus ===
                this.$Enum.DELIVERY_STATUS.NOT_DELIVERY
                  ? "Chưa giao hàng"
                  : "Hoàn thành xử lý giao hàng"
              }}
            </p>
          </div>
          <div class="procees-order">
            <div class="header-product-list d-flex">
              <div style="font-size: 15px; flex-basis: 70%; text-align: right">
                Số lượng
              </div>
              <div style="font-size: 15px; flex-basis: 15%; text-align: center">
                Giá
              </div>
              <div style="font-size: 15px; flex-basis: 15%; text-align: center">
                Thành tiền
              </div>
            </div>
            <div class="product-list">
              <div
                v-for="item in orderDetails"
                :key="item.OrderDetailId"
                class="produt-item d-flex"
              >
                <div class="product-item-image">
                  <img :src="item.ImagePath" alt="" />
                </div>
                <div class="product-item-desc">
                  <h3 class="product-item-name">{{ item.BookName }}</h3>
                </div>
                <div class="product-item-quantiy">{{ item.Quantity }}</div>
                <div class="product-item-price">
                  {{ this.$helper.formatMoney(item.Price) }}đ
                </div>
                <div class="product-item-into-money me-1">
                  {{ this.$helper.formatMoney(item.Quantity * item.Price) }}đ
                </div>
              </div>
            </div>
            <div class="total-amount-order">
              <p>Tổng cộng :</p>
              <p>
                <b>{{ this.$helper.formatMoney(order.TotalAmount) }}đ</b>
              </p>
            </div>
            <div
              v-if="
                order.DeliveryStatus ===
                  this.$Enum.DELIVERY_STATUS.NOT_DELIVERY &&
                order.OrderStatus !== this.$Enum.ORDER_STATUS.CANCELLED
              "
              class="btn-ship"
            >
              <button
                @click="handleOnConfirmDelivery"
                class="m-button"
                style="background-color: #0051c8"
              >
                Giao hàng
              </button>
            </div>
          </div>
          <div class="info-payment mt-3">
            <div class="info-payment__header">
              <p
                :style="{
                  color:
                    this.order.PaymentStatus ===
                    this.$Enum.PAYMENT_STATUS.UNPAID
                      ? '#DEB85B'
                      : '#a0dbb3',
                  borderColor:
                    this.order.PaymentStatus ===
                    this.$Enum.PAYMENT_STATUS.UNPAID
                      ? '#DEB85B'
                      : '#a0dbb3',
                  backgroundColor:
                    this.order.PaymentStatus ===
                    this.$Enum.PAYMENT_STATUS.UNPAID
                      ? '#fdfde8'
                      : '#f1fcf5',
                }"
              >
                {{
                  this.$helper.hanldeValueTypeEnum(
                    "PAYMENT_STATUS",
                    order.PaymentStatus
                  )
                }}
              </p>
            </div>
            <div class="info-payment__content mb-4">
              <div class="row">
                <div class="col-6">
                  <div class="add-note" style="text-align: right">
                    <MInput label="Ghi chú đơn hàng"></MInput>
                    <button class="m-button" style="background-color: #0051c8">
                      Cập nhật
                    </button>
                  </div>
                </div>
                <div class="col-6">
                  <div class="info-wrap">
                    <div class="info-item">
                      <p class="info-item__title">Số lượng sản phẩm</p>
                      <p class="info-item__value">{{ order.TotalQuantity }}</p>
                    </div>
                    <div class="info-item">
                      <p class="info-item__title">Tổng tiền hàng</p>
                      <p class="info-item__value">
                        {{
                          this.$helper.formatMoney(
                            order.TotalAmount - order.ShippingFee
                          )
                        }}đ
                      </p>
                    </div>
                    <div class="info-item">
                      <p class="info-item__title">Giảm giá</p>
                      <p class="info-item__value">0đ</p>
                    </div>
                    <div class="info-item">
                      <p class="info-item__title">Vận chuyển</p>
                      <p class="info-item__value">
                        {{ this.$helper.formatMoney(order.ShippingFee) }}đ
                      </p>
                    </div>
                    <div class="info-item">
                      <div class="info-item__title">
                        <b>Tổng giá trị đơn hàng</b>
                      </div>
                      <p class="info-item__value">
                        <b
                          >{{ this.$helper.formatMoney(order.TotalAmount) }}đ</b
                        >
                      </p>
                    </div>
                    <p class="payment-method">
                      {{
                        this.$helper.hanldeValueTypeEnum(
                          "PAYMENT_METHOD",
                          order.PaymentMethod
                        )
                      }}
                    </p>
                    <div class="info-item">
                      <p class="info-item__title">Đã thanh toán</p>
                      <p class="info-item__value">
                        {{ this.$helper.formatMoney(order.TotalAmount) }}đ
                      </p>
                    </div>

                    <div class="payment" style="text-align: right">
                      <button
                        v-if="
                          order.PaymentStatus !==
                            this.$Enum.PAYMENT_STATUS.PAID &&
                          order.OrderStatus !==
                            this.$Enum.ORDER_STATUS.CANCELLED
                        "
                        class="m-button"
                        style="background-color: #0051c8"
                        @click="handleOnOrderPayment"
                      >
                        Thanh toán
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="confirm-order">
            <p class="confirm-order-title">Xác thực đơn hàng</p>

            <div
              v-if="
                order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION ||
                order.OrderStatus === this.$Enum.ORDER_STATUS.WAIT_FOR_PAY
              "
              class="confirm-group"
            >
              <p class="confirm-order-text">Vui lòng xác thực đơn hàng.</p>
              <div class="confirm-order-button mb-2">
                <button
                  @click="onConfirmOrder"
                  class="m-button"
                  style="background-color: #0051c8; width: 100%"
                >
                  Xác thực đơn hàng
                </button>
              </div>
            </div>
            <div v-else class="confirm-group d-flex align-item-center gap-2">
              <div style="color: #0051c8; padding: 8px 0" class="confirm-icon">
                <i class="fa-solid fa-circle-check"></i>
              </div>
              <p class="confirm-order-text">Đơn hàng đã được xác thực</p>
            </div>
          </div>
          <div class="profile-customer">
            <p class="profile-customer-header">Thông tin người mua</p>
            <div class="info-customer">
              <p class="customer-name mb-2" style="color: #437ff7">
                {{ userInfo.Fullname }}
              </p>
              <div class="user-achivement quantity-ordered">
                <p>Đã đặt</p>
                <p>
                  <b>{{ totalOrderUserOrdered }} đơn hàng</b>
                </p>
              </div>
              <div class="user-achivement quantity-ordered">
                <p>Đơn hàng hoàn tất</p>
                <p>
                  <b
                    >{{ totalOrderComplete ? totalOrderComplete : 0 }} đơn
                    hàng</b
                  >
                </p>
              </div>
              <div class="user-achivement quantity-ordered">
                <p>Đơn hàng đã hủy</p>
                <p>
                  <b>{{ totalOrderCancel ? totalOrderCancel : 0 }} đơn hàng</b>
                </p>
              </div>
              <div class="user-achivement accumulated-revenue">
                <p>Doanh thu tích lũy</p>
                <p>
                  <b>{{ this.$helper.formatMoney(accumulatedRevenue) }}đ</b>
                </p>
              </div>
            </div>
            <div class="info-border info-user-delivery">
              <p class="profile-customer-header">Người nhận hàng</p>
              <p>{{ order.Fullname }}</p>
              <p>{{ order.PhoneNumber }}</p>
            </div>
            <div class="info-border info-delivery-address">
              <p class="profile-customer-header">Địa chỉ giao hàng</p>
              <p>{{ order.Address }}</p>
            </div>
          </div>
          <div class="change-status-order mt-3 d-flex justify-content-between">
            <button
              v-show="
                order.OrderStatus === this.$Enum.ORDER_STATUS.CONFIRMED &&
                order.OrderStatus !== this.$Enum.ORDER_STATUS.CANCELLED
              "
              @click="handleOrder"
              class="m-button"
              style="background-color: #0051c8"
            >
              Xử lý đơn hàng
            </button>
            <button
              v-show="
                order.OrderStatus === this.$Enum.ORDER_STATUS.PROCESSING &&
                order.OrderStatus !== this.$Enum.ORDER_STATUS.CANCELLED
              "
              @click="handleOnOrderComplete"
              class="m-button"
              style="background-color: #0051c8"
            >
              Hoàn tất đơn
            </button>
            <button
              v-show="
                order.OrderStatus !== this.$Enum.ORDER_STATUS.COMPLETE &&
                order.OrderStatus !== this.$Enum.ORDER_STATUS.CANCELLED
              "
              @click="onCancelOrder"
              class="m-button"
              style="background-color: #ff0000"
            >
              Hủy đơn hàng
            </button>
            <button
              v-show="order.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED"
              @click="handleOnReOrder"
              class="m-button"
              style="background-color: #0051c8"
            >
              Đặt lại hàng
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <MDialog
    v-if="isShowDialog"
    title="Xác thực đơn hàng"
    @onCloseDialog="
      () => {
        this.isShowDialog = false;
      }
    "
    :message="messageDialog"
    :type="this.$Resource[this.$languageCode].Dialog.Type.Question"
  >
    <template #footerLeft>
      <MButton
        @click="this.isShowDialog = false"
        class="m-button--sub"
        text="Hủy"
      ></MButton>
    </template>
    <template #footerRight>
      <MButton
        style="background-color: #0051c8"
        @click="handleOnUpdateOrder"
        text="Xác nhận"
      ></MButton>
    </template>
  </MDialog>
</template>
<script>
import orderService from "@/utils/OrderService";
import userService from "@/utils/UserService";
export default {
  name: "OrderDetailAdminPage",
  async mounted() {
    await this.getOrderData();
    await this.getOrderDetailsData();
    await this.getUserInfoData();
    await this.caculTotalOrder();
  },
  data() {
    return {
      order: {},
      orderDetails: [],
      userInfo: {},
      totalOrderUserOrdered: 0,
      accumulatedRevenue: 0,
      isShowDialog: false,
      typeStatus: "",
    };
  },

  methods: {
    async getOrderData() {
      try {
        const res = await orderService.getById(this.$route.params.id);
        if (res.status === 200) {
          this.order = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getOrderDetailsData() {
      try {
        const res = await this.$httpRequest.get(
          `OrderDetails/GetByOrderId/${this.$route.params.id}`
        );
        if (res.status === 200) {
          this.orderDetails = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async getUserInfoData() {
      try {
        const res = await userService.getById(this.order.UserId);
        if (res.status === 200) {
          this.userInfo = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async caculTotalOrder() {
      const res = await orderService.GetByUserId(this.order.UserId);
      if (res.status === 200) {
        this.totalOrderUserOrdered = res.data.length;
        const ordersComplete = res.data.filter(
          (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.COMPLETE
        );
        this.totalOrderComplete = ordersComplete.length;

        const ordersCancel = res.data.filter(
          (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
        );
        this.totalOrderCancel = ordersCancel.length;
        const accumulatedRevenue = ordersComplete.reduce(
          (previousValue, item) => previousValue + item.TotalAmount,
          0
        );
        this.accumulatedRevenue = accumulatedRevenue ? accumulatedRevenue : 0;
      }
    },

    handleOnUpdateOrder() {
      if (this.typeStatus === "Confirm Order") {
        this.confirmOrder();
      } else if (this.typeStatus === "Confirm Cancel") {
        this.cancelOrder();
      }
    },

    async handleOnConfirmDelivery() {
      try {
        if (
          this.order.OrderStatus ===
          this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
        ) {
          this.isShowDialog = true;
          this.messageDialog =
            "Bạn cần xác thực đơn hàng trước khi giao. Bạn muốn xác thực đơn hàng <" +
            this.order.OrderCode +
            ">";
          this.typeStatus = "Confirm Order";
          return;
        }
        this.$emitter.emit("toggleShowLoading", true);
        this.order.DeliveryStatus =
          this.$Enum.DELIVERY_STATUS.BEING_TRANSPORTED;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Xác nhận giao hàng thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          //đóng dialog
        }
      } catch (error) {
        console.log(error);
      }
    },

    async confirmOrder() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        this.order.OrderStatus = this.$Enum.ORDER_STATUS.CONFIRMED;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Xác nhận thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          //đóng dialog
          this.isShowDialog = false;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async handleOnOrderPayment() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        this.order.PaymentStatus = this.$Enum.PAYMENT_STATUS.PAID;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Thanh toán thành công.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
        }
      } catch (error) {
        console.log(error);
      }
    },

    async handleOnOrderComplete() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        this.order.OrderStatus = this.$Enum.ORDER_STATUS.COMPLETE;
        this.order.DeliveryStatus = this.$Enum.DELIVERY_STATUS.DELIVERIED;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đơn hàng được hoàn tất.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          this.caculTotalOrder();
        }
      } catch (error) {
        console.log(error);
      }
    },

    async cancelOrder() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const orderData = {
          Order : this.order,
          OrderDetails: this.orderDetails
        }
       
        const res = await orderService.CancelOrder(orderData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Hủy đơn hàng thành công.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          //đóng dialog
          this.isShowDialog = false;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async handleOrder() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        this.order.OrderStatus = this.$Enum.ORDER_STATUS.PROCESSING;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đơn hàng đang được xử lý.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          //đóng dialog
          this.isShowDialog = false;
        }
      } catch (error) {
        console.log(error);
      }
    },
    onConfirmOrder() {
      this.isShowDialog = true;
      this.messageDialog =
        "Bạn muốn xác thực đơn hàng <" + this.order.OrderCode + "> ?";
      this.typeStatus = "Confirm Order";
    },
    onCancelOrder() {
      this.isShowDialog = true;
      this.messageDialog = "Bạn có chắc chắn hủy đơn hàng này?";
      this.typeStatus = "Confirm Cancel";
    },

    async handleOnReOrder() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        this.order.OrderStatus = this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION;
        this.order.DeliveryStatus = this.$Enum.DELIVERY_STATUS.NOT_DELIVERY;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.order));
        const res = await orderService.put(this.order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đơn hàng đang được xử lý.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          this.getOrderData();
          //đóng dialog
          this.isShowDialog = false;
        }
      } catch (error) {
        console.log(error);
      }
    },
    backToOrderManagement() {
      location.href = "http://localhost:8080/admin/order-manegement";
    },
  },
};
</script>
<style scoped>
.m-button:hover {
  opacity: 0.9;
}
.breadcrumb-shop {
  background-color: #eeeff1;
  margin-left: -12px;
}
</style>
