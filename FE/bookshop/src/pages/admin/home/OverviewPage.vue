<template>
  <div class="home-admin">
    <div class="row">
      <div class="col-9">
        <div class="content__header">
          <div class="content__header-title">
            <h3>Tổng quan</h3>
          </div>
        </div>
        <div class="statistical-wrap">
          <div class="row">
            <div class="col-4">
              <div class="statistical-item">
                <div class="statistical-item__image">
                  <img
                    src="data:image/jpeg;base64,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"
                    alt=""
                  />
                </div>
                <div class="statistical-item__info">
                  <p class="statistical-item__title">Tổng số sách</p>
                  <p class="statistical-item__total">{{ totalProduct }}</p>
                </div>
              </div>
            </div>
            <div class="col-4">
              <div class="statistical-item">
                <div class="statistical-item__image">
                  <img
                    src="data:image/png;base64,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"
                    alt=""
                  />
                </div>
                <div class="statistical-item__info">
                  <p class="statistical-item__title">Tổng số đơn hàng</p>
                  <p class="statistical-item__total">{{ totalOrder }}</p>
                </div>
              </div>
            </div>
            <div class="col-4">
              <div class="statistical-item">
                <div class="statistical-item__image">
                  <img
                    src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjEwpi14yS4VLQQhoFJp5HwudbvGZGEuRATA&s"
                    alt=""
                  />
                </div>
                <div class="statistical-item__info">
                  <p class="statistical-item__title">Tổng số khách hàng</p>
                  <p class="statistical-item__total">{{ totalCustomer }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="line-chart">
          <LineChart></LineChart>
        </div>
      </div>
      <div class="col-3">
        <div class="notification">
          <div class="content__header">
            <div class="content__header-title">
              <h3>Thông báo</h3>
            </div>
          </div>
          <div class="notification-warp">
            <div class="notification-item">
              <div class="notification-item__image">
                <img
                  src="https://cdn.sforum.vn/sforum/wp-content/uploads/2024/02/avatar-anh-meo-cute-1.jpg"
                  alt=""
                />
              </div>
              <div class="notification-item__body">
                <p class="notification-item__content">
                  Lê Quang Huy đã đặt một đơn hàng mới
                </p>
                <p class="time">3 phút trước</p>
              </div>
            </div>
            <div class="notification-item">
              <div class="notification-item__image">
                <img
                  src="https://cdn.sforum.vn/sforum/wp-content/uploads/2024/02/avatar-anh-meo-cute-1.jpg"
                  alt=""
                />
              </div>
              <div class="notification-item__body">
                <p class="notification-item__content">
                  Lê Quang Huy đã đặt một đơn hàng mới
                </p>
                <p class="time">3 phút trước</p>
              </div>
            </div>
            <div class="notification-item">
              <div class="notification-item__image">
                <img
                  src="https://cdn.sforum.vn/sforum/wp-content/uploads/2024/02/avatar-anh-meo-cute-1.jpg"
                  alt=""
                />
              </div>
              <div class="notification-item__body">
                <p class="notification-item__content">
                  Lê Quang Huy đã đặt một đơn hàng mới
                </p>
                <p class="time">3 phút trước</p>
              </div>
            </div>
          </div>
        </div>
        <div class="sales-analytis">
          <div class="content__header">
            <div class="content__header-title">
              <h3>Phân tích bán hàng</h3>
            </div>
          </div>
          <div class="analytis-item">
            <div
              style="background-color: #f77f00; color: #fff"
              class="analytis-item__icon"
            >
              <i class="fa-solid fa-cart-plus"></i>
            </div>
            <div class="analytis-item__body">
              <div @click="goToOrderManagement" class="analytis-item__title">
                <p>Đơn hàng mới</p>
                <p>24 giờ qua</p>
              </div>
              <div class="analytis-item-total">
                <b>{{ totalNewOrder }}</b>
              </div>
            </div>
          </div>
          <div class="analytis-item">
            <div
              style="background-color: #2ec4b6; color: #fff"
              class="analytis-item__icon"
            >
              <i class="fa-solid fa-user-plus"></i>
            </div>
            <div class="analytis-item__body">
              <div class="analytis-item__title">
                <p>Khách hàng mới</p>
                <p>24 giờ qua</p>
              </div>
              <div class="analytis-item-total">
                <b>{{ totalNewCustomer }}</b>
              </div>
            </div>
          </div>
          <div class="analytis-item">
            <div
              style="background-color: #e56b6f; color: #fff"
              class="analytis-item__icon"
            >
              <i class="fa-solid fa-cart-shopping"></i>
            </div>
            <div class="analytis-item__body">
              <div @click="goToOrderManagement" class="analytis-item__title">
                <p>Đơn cần duyệt</p>
                <p>24 giờ qua</p>
              </div>
              <div class="analytis-item-total">
                <b>{{ totalComfirmedNewOrder }}</b>
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
import LineChart from "./LineChart.vue";
import orderService from "@/utils/OrderService";
import userService from "@/utils/UserService";
export default {
  name: "HomeAdminPage",
  components: { LineChart },
  mounted() {
    this.getTotalProduct();
    this.getTotalOrder();
    this.getTotalCustomer();
    this.getTotalNewOrder();
    this.getTotalConfirmedNewOrder();
    this.getTotalNewCustomer();
    document.title = "Tổng quan";
  },
  data() {
    return {
      //Lưu tổng số sách
      totalProduct: 0,
      //Lưu tổng số đơn hàng
      totalOrder: 0,
      //Lưu tổng số khách hàng
      totalCustomer: 0,
      //Lưu tổng số khách hàng mới trong 24h
      totalNewCustomer: 0,
      //Lưu tổng số đơn hàng mới trong 24h
      totalNewOrder: 0,
      //Lưu tổng số đơn hàng mới cần xác nhận trong 24h
      totalComfirmedNewOrder: 0,
    };
  },
  methods: {
    /**
     * Thực hiện lấy tổng số sách
     * @author LQHUY(23/04/2024)
     */
    async getTotalProduct() {
      try {
        const res = await bookService.getAll();
        if (res.status === 200) {
          this.totalProduct = res.data.length;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thực hiện lấy tổng số đơn hàng
     * @author LQHUY(23/04/2024)
     */
    async getTotalOrder() {
      try {
        const res = await orderService.getAll();
        if (res.status === 200) {
          this.totalOrder = res.data.length;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy tổng số khách hàng
     * @author LQHUY(23/04/2024)
     */
    async getTotalCustomer() {
      try {
        const res = await userService.getAll();
        if (res.status === 200) {
          const customers = res.data.filter((item) => item.RoleName === "User");
          this.totalCustomer = customers.length;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy tổng số đơn hàng trong vòng 24 giờ qua
     * @author LQHUY(23/04/2024)
     */
    async getTotalNewOrder() {
      try {
        const params = {
          conditionField: null,
        };
        const res = await orderService.getTotalOrderByConditionIn24Hour({
          params,
        });
        if (res.status === 200) {
          this.totalNewOrder = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy tổng số đơn hàng cần xác nhận trong vòng 24 giờ qua
     * @author LQHUY(23/04/2024)
     */
    async getTotalConfirmedNewOrder() {
      try {
        const params = {
          conditionField: "OrderStatus",
        };
        const res = await orderService.getTotalOrderByConditionIn24Hour({
          params,
        });
        if (res.status === 200) {
          this.totalComfirmedNewOrder = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy tổng số khách hàng mới trong vòng 24 giờ qua
     * @author LQHUY(23/04/2024)
     */
    async getTotalNewCustomer() {
      try {
        const res = await userService.getTotalCustomer();
        if (res.status === 200) {
          this.totalNewCustomer = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    goToOrderManagement() {
      this.$router.push("order-manegement");
    },
  },
};
</script>
<style scoped>
.statistical-wrap {
  margin-bottom: 32px;
}
.statistical-item {
  background-color: #fff;
  border-radius: 8px;
  padding: 16px;
  display: flex;
  column-gap: 20px;
  box-shadow: 0 1px 2px 0 rgba(60, 64, 67, 0.1),
    0 2px 6px 2px rgba(60, 64, 67, 0.15);
}
.statistical-item .statistical-item__image {
  width: 64px;
  height: 64px;
  text-align: center;
}
.statistical-item .statistical-item__image img {
  max-height: 100%;
}
.statistical-item__info {
  font-size: 16px;
  text-align: center;
}
.line-chart {
  background-color: #fff;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 1px 2px 0 rgba(60, 64, 67, 0.1),
    0 2px 6px 2px rgba(60, 64, 67, 0.15);
}
.notification {
}
.notification-warp {
  background-color: #fff;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 1px 2px 0 rgba(60, 64, 67, 0.1),
    0 2px 6px 2px rgba(60, 64, 67, 0.15);
}
.notification-item {
  display: flex;
  column-gap: 12px;
}
.notification-item + .notification-item {
  margin-top: 12px;
}
.notification-item__image {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  overflow: hidden;
  flex-shrink: 0;
}
.notification-item__image img {
  width: 100%;
  height: 100%;
}
.notification-item__body {
  font-size: 14px;
  width: 100%;
}
.analytis-item {
  background-color: #fff;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 1px 2px 0 rgba(60, 64, 67, 0.1),
    0 2px 6px 2px rgba(60, 64, 67, 0.15);
  display: flex;
  margin-bottom: 12px;
  column-gap: 20px;
  align-items: center;
  cursor: pointer;
}
.analytis-item__icon {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  overflow: hidden;
  flex-shrink: 0;
  text-align: center;
  line-height: 36px;
}
.analytis-item__body {
  display: flex;
  justify-content: space-between;
  font-size: 14px;
  flex: 1;
}
.analytis-item__title {
}
.analytis-item__title p:nth-child(1) {
  font-size: 15px;
  font-weight: 600;
}
.notification-item__body p:nth-child(2),
.analytis-item__title p:nth-child(2) {
  font-size: 13px;
  color: #767575;
}
.analytis-item-total {
}
</style>
