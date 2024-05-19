<template>
  <div class="payment-success">
    <div class="container w-100">
      <div class="success-main">
        <div class="success__icon">
          <i class="fa-solid fa-circle-check"></i>
        </div>
        <div class="success__title">{{ this.$props.title }} thành công</div>
        <div class="success__content w-75">
          {{ this.$props.content }}. Fahasa sẽ sớm liên hệ với quý khách sớm để
          bàn giao sản phẩm.
        </div>
        <div class="see-order-detail">
          Bạn có thể xem chi tiết đơn hàng trong
          <a style="color: #32a5ea" href="http://localhost:8080/customer/order"
            >Đơn hàng của tôi</a
          >
        </div>
        <div class="button-purchase">
          <button @click="goToHomePage">Tiếp tục mua hàng</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    title: {
      type: String,
      default: "Thanh toán",
    },
    content: {
      type: String,
      default: "Đơn hàng của quý khách đã thanh toán thành công",
    },
  },
  methods: {
    goToHomePage() {
      location.href = "http://localhost:8080/";
    },
  },
};
</script>
<style scoped>
.payment-success {
  background-color: #f1f1f1;
  padding: 32px 0;
  
}
.success-main {
  background-color: #fff;
  width: 50%;
  margin: 0 auto;
  padding: 16px;
  text-align: center;
  box-shadow: 0 2px 5px #bbb9b9;
  border-radius: 12px;
}
.success__icon {
  color: #04be04;
  font-size: 80px;
}
.success__title {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 16px;
}
.success__content {
  margin: 0 auto 12px;
  font-size: 14px;
}
.button-purchase {
  margin: 20px 0;
}
.button-purchase button {
  height: 44px;
  width: 200px;
  font-size: 14px;
  border-radius: 6px;
  background-color: #008fe5;
  outline: none;
  color: #fff;
  border: none;
}
</style>
