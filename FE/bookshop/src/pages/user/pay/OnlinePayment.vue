<template>
  <div class="payment-online">
    <div class="payment-wrap">
      <div class="payment__header">
        <div class="payment__header-logo">
          <img
            src="https://vnpay.vn/s1/statics.vnpay.vn/2023/9/06ncktiwd6dc1694418196384.png"
            alt=""
          />
        </div>
        <div class="payment__header-transaction-time">
          <p class="title">Giao dịch hết hạn sau</p>
          <div class="time hour">14</div>
          <p style="margin: 0 4px !important">:</p>
          <div class="time minute">10</div>
        </div>
      </div>
      <div class="payment__body">
        <div class="payment-info">
          <div class="payment-info-title">Thông tin đơn hàng</div>
          <div class="info-item">
            <p class="info-item-title">Số tiền thanh toán</p>
            <p class="info-item-value">20.000.000 VNĐ</p>
          </div>
          <div class="info-item">
            <p class="info-item-title">Giá trị đơn hàng</p>
            <p class="info-item-value">20.000.000 VNĐ</p>
          </div>
          <div class="info-item">
            <p class="info-item-title">Phí giao dịch</p>
            <p class="info-item-value">0đ</p>
          </div>
          <div class="info-item">
            <p class="info-item-title">Số tiền thanh toán</p>
            <p class="info-item-value">20.000.000 VNĐ</p>
          </div>
          <div class="info-item">
            <p class="info-item-title">Nhà cung cấp</p>
            <p class="info-item-value">VNPAY</p>
          </div>
        </div>
        <div class="card-info">
          <div class="card-info-header">
            <p>Thanh toán qua VNPAY</p>
            <p>Thẻ nội địa</p>
          </div>
          <div class="payment-form">
            <form v-if="isOTPAuthentication === false" action="" class="">
              <MInput
                :ref="textFields.accountName.ref"
                :label="textFields.accountName.label"
                :rules="textFields.accountName.rules"
                :placeholder="textFields.accountName.placeholder"
                v-model="onlinePayment.AccountName"
              ></MInput>
              <MInput
                :ref="textFields.cardNumber.ref"
                :label="textFields.cardNumber.label"
                :rules="textFields.cardNumber.rules"
                :placeholder="textFields.cardNumber.placeholder"
                v-model="onlinePayment.CardNumber"
              ></MInput>
              <MInput
                :ref="textFields.releaseDate.ref"
                :label="textFields.releaseDate.label"
                :rules="textFields.releaseDate.rules"
                :placeholder="textFields.releaseDate.placeholder"
                v-model="onlinePayment.ReleaseDate"
              ></MInput>
            </form>
            <form v-if="isOTPAuthentication" action="">
              <MInput
                :ref="textFields.accountName.ref"
                :label="textFields.accountName.label"
                :rules="textFields.accountName.rules"
                :placeholder="textFields.accountName.placeholder"
                v-model="onlinePayment.AccountName"
              ></MInput>
            </form>
            <div class="group-button-payment">
              <div class="cancel-payment">
                <MButton
                  style="background-color: #e1e3e4; color: #333"
                  text="Hủy thanh toán"
                ></MButton>
              </div>
              <div
                v-if="isOTPAuthentication === false"
                class="continue-payment"
              >
                <MButton
                  style="background-color: #0051c8"
                  text="Tiếp tục"
                  @click="this.isOTPAuthentication = true"
                ></MButton>
              </div>
              <div v-if="isOTPAuthentication" class="action-payment">
                <MButton
                  style="background-color: #0051c8"
                  text="Thanh toán"
                ></MButton>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="payment__footer">
        <div class="payment__footer-contact">
          <div class="icon contact-phone">
            <div class="phone-icon">
              <i class="fa-solid fa-phone"></i>
            </div>
            <p>1900.6364.67</p>
          </div>
          <div class="icon contact-email">
            <div class="email-icon">
              <i class="fa-solid fa-envelope"></i>
            </div>
            <p>cskh@fahasa.com.vn</p>
          </div>
        </div>
        <div class="payment__footer-sercurity"></div>
      </div>
    </div>
  </div>
</template>
<script>
import TEXT_FIELD from "@/js/resource/text-field";
export default {
  name: "OnlinePayment",
  computed: {
    textFields: function () {
      return TEXT_FIELD[this.$languageCode].onlinePayment;
    },
  },
  data() {
    return {
      onlinePayment: {},
      isOTPAuthentication: false,
    };
  },
};
</script>
<style scoped>
@import url(./pay.css);
</style>
