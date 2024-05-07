<template>
  <div class="member-home-page">
    <div class="block-smem-info pb-4">
      <div class="block-welcome">
        <div class="welcome-avatar">
          <img
            src="data:image/jpeg;base64,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"
            alt=""
          />
        </div>
        <div class="welcome-smember">
          <div class="welcome-smember__name">{{ userInfo.Fullname }}</div>
          <div class="welcome-smember__phone">{{ userInfo.PhoneNumber }}</div>
        </div>
      </div>
      <div class="block-homepage-menu mt-4">
        <div class="block-homepage-menu__content">
          <div class="content-item">
            <p class="content-item__title">
              {{ this.$helper.formatDate(userInfo.CreatedDate) }}
            </p>
            <p class="content-item__text">Ngày tham gia</p>
          </div>
          <div class="content-item">
            <p class="content-item__title">{{ totalOrder }}</p>
            <p class="content-item__text">đơn hàng</p>
          </div>
          <div class="content-item">
            <p class="content-item__title">
              {{ this.$helper.formatMoney(totalAccumulatedAmount) }}đ
            </p>
            <p class="content-item__text">Tồng tiền tích lũy</p>
          </div>
        </div>
      </div>
    </div>
    <div class="block-order-recent p-4">
      <div
        class="box-header mb-4 d-flex justify-content-between align-items-center"
      >
        <h2>Những đơn hàng gần đây</h2>
        <a style="color: #c92127" href="http://localhost:8080/customer/order"
          >Xem tất cả</a
        >
      </div>
      <div class="data-table">
        <table class="w-100">
          <thead>
            <tr>
              <th style="width: 120px">Mã đơn hàng</th>
              <th style="text-align: center">Ngày đặt</th>
              <th>Người nhận</th>
              <th>Trạng thái</th>
              <th>Tổng cộng</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in recentOrders" :key="item.OrderId">
              <td>{{ item.OrderCode }}</td>
              <td style="text-align: center">
                {{ this.$helper.formatDate(item.OrderDate) }}
              </td>
              <td>{{ item.Fullname }}</td>
              <td>
                {{
                  this.$helper.hanldeValueTypeEnum(
                    "ORDER_STATUS",
                    item.OrderStatus
                  )
                }}
              </td>
              <td>{{ this.$helper.formatMoney(item.TotalAmount) }}đ</td>
              <td>
                <a
                  :href="
                    'http://localhost:8080/customer/order/order-detail' +
                    item.OrderId
                  "
                  style="color: #c92127"
                  >Xem</a
                >
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="block-deliver-address p-4">
      <div class="box-title">Sổ địa chỉ</div>
      <div class="box-content mt-3 pb-3">
        <h4>Địa chỉ giao hàng mặc định</h4>
        <address>
          {{ deliveryAddressDefault.ReminiscentName }}
          <br />
          {{ deliveryAddressDefault.HomeNumber }}
          <br />
          {{ deliveryAddressDefault.DeliveryAddressName }}
          <br />
          {{ deliveryAddressDefault.PhoneNumber }}
          <br />
          <a style="color: #c92127" href="">Sửa địa chỉ</a>
        </address>
      </div>
    </div>
  </div>
</template>
<script>
import localStorageService from "@/js/storage/LocalStorageService";
import orderService from "@/utils/OrderService";
import deliveryAddressService from "@/utils/DeliveryAddressService";
export default {
  name: "HomeAccountUserPage",
  data() {
    return {
      totalOrder: 0,
      totalAccumulatedAmount: 0,
      recentOrders: [],
      deliveryAddressDefault: {},
    };
  },
  mounted() {
    this.getInfoPurchaseByUser();
    this.getDeliveryAddressDefault();
    document.title = "Tài khoản";
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  methods: {
    async getInfoPurchaseByUser() {
      try {
        const params = {
          UserId: this.userInfo.UserId,
        };
        const res = await orderService.GetByUserId({ params });
        if (res.status === 200) {
          this.recentOrders = res.data.Data.slice(0, 5);
          this.totalOrder = res.data.Data.length;
          this.totalAccumulatedAmount = res.data.Data.reduce(
            (previousValue, item) => {
              if (item.PaymentStatus === this.$Enum.PAYMENT_STATUS.PAID) {
                return item.TotalAmount + previousValue;
              }
              // Trả về giá trị hiện tại nếu không thỏa điều kiện
              return previousValue;
            },
            0
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getDeliveryAddressDefault() {
      try {
        const res = await deliveryAddressService.getAllByUserId(
          this.userInfo.UserId
        );
        if (res.status === 200) {
          this.deliveryAddressDefault = res.data.filter(
            (item) => item.DeliveryAddressDefault === true
          )[0];
          console.log(this.deliveryAddressDefault);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
.block-smem-info {
  background-color: #f0f0f0;
}
.block-welcome {
  display: flex;
  column-gap: 10px;
}
.welcome-avatar {
  border-radius: 50%;
  background-color: #fff;
  border: 1px solid #a83288;
  overflow: hidden;
}
.welcome-avatar img {
  height: 60px;
}
.welcome-smember {
  display: flex;
  flex-direction: column;
  font-size: 18px;
  line-height: 18px;
  padding: 3px 10px;
}
.welcome-smember .welcome-smember__name {
  color: #a83288;
  font-size: 19px;
  font-weight: 700;
  margin-bottom: 4px;
  text-transform: uppercase;
}
.welcome-smember .welcome-smember__phone {
  font-size: 14px;
  color: #637381;
}
.block-homepage-menu {
  background-color: #fff;
  border-radius: 8px;
}
.block-homepage-menu__content {
  display: flex;
  padding-bottom: 15px;
}
.content-item {
  flex-basis: 50%;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 15px;
}
.content-item + .content-item {
  border-left: 1px solid #111;
}
.content-item__title {
  font-size: 28px;
  font-weight: 700;
  margin-bottom: 5px;
  padding-top: 10px !important;
}
.content-item__text {
  font-size: 14px;
  padding-top: 10px !important;
}
.block-order-recent {
  border-radius: 8px !important;
  overflow: hidden;
}
.box-header h2 {
  font-size: 17px;
  font-weight: normal;
  background-position: 0 0;
  background-repeat: no-repeat;
  color: #333;
  text-transform: uppercase;
}
.data-table th,
.data-table tr,
.data-table td {
  padding: 0 16px;
  height: 42px;
}
.data-table tr:nth-child(odd) {
  background-color: #f8f8f8;
}

.data-table th {
  background-color: #fff;
}
address {
  font-style: normal;
  line-height: 25px;
  font-size: 14px;
}
.block-deliver-address .box-title {
  border-bottom: 1px solid #e5e5e5;
  margin: 0 0 10px;
  padding: 0 0 2px;
  margin: 0;
  text-transform: uppercase;
  font-weight: 400;
  font-size: 17px;
  color: #c92127;
  line-height: 39px;
}
.box-content h4 {
  font-size: 16px;
  color: #333;
  margin-bottom: 12px !important;
}
</style>
