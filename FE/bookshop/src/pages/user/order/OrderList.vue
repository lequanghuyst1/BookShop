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
              style="width: 150px"
              v-for="(tab, index) in computedTabs"
              :key="index"
              :class="[
                'tab-history-item',
                'swiper-slide',
                { 'tab-history-item-active': index === activeTabIndex },
              ]"
              @click="getOrdersPagingData(tab.columnName, tab.status, index)"
            >
              <div class="tab-history-item-border-left"></div>
              <div class="tab-history-item-number">{{ tab.quantity }}</div>
              <div class="tab-history-item-text">{{ tab.label }}</div>
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

    <div class="my-account mt-4">
      <div class="table-order-container">
        <div class="table-order-row table-order-header">
          <div class="table-order-cell" style="width: 120px">Mã đơn hàng</div>
          <div class="table-order-cell" style="text-align: center">
            Ngày mua
          </div>
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
              <div style="text-align: center">
                {{ this.$helper.formatOrderDate(order.OrderDate) }}
              </div>
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
            {{
              this.$helper.hanldeValueTypeEnum(
                "ORDER_STATUS",
                order.OrderStatus
              )
            }}
          </div>
          <div
            class="table-order-cell table-order-link-more hidden-max-width-992"
            @click="goToOrderDetail(order.OrderId)"
          >
            Xem chi tiết
          </div>
        </div>
      </div>
      <div v-if="this.orders.length > 0" id="pages" class="paging">
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
            <span style="cursor: default; display: block" v-else>...</span>
          </li>
        </ol>
        <div
          v-show="
            this.filterData.pageNumber !== this.totalPage &&
            this.orders.length > 0
          "
          class="paging-button btn-next"
          @click="this.filterData.pageNumber++"
        >
          <i class="fa-solid fa-chevron-right"></i>
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
  created() {},
  mounted() {
    this.getTotalOrdersData();

    this.getOrdersPagingData();
    document.title = "Đơn hàng của tôi";
  },
  data() {
    return {
      //Lưu danh sách các đơn hàng đã đặt
      orders: [],
      //Lưu giá trị tổng số đơn hàng bị hủy
      quantityOrder: 0,
      quantityOrderWatting: 0,
      quantityOrderConfirmed: 0,
      quantityOrderShipping: 0,
      quantityOrderDelivered: 0,
      quantityOrderComplete: 0,
      quantityOrderCancelled: 0,
      ordersCancelled: [],
      orderStatus: null,
      //Lưu trữ tổng số bản ghi
      totalPage: 0,
      //Lưu trữ phân trang tối đa được hiện
      maxPages: 4,
      filterData: {
        pageSize: 10,
        pageNumber: 1,
      },
      tabs: [
        { label: "Tất Cả", quantity: this.quantityOrder },
        {
          label: "Chờ xác nhận",
          columnName: "OrderStatus",
          status: this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION,
        },
        {
          label: "Đã xác nhận",
          columnName: "OrderStatus",
          status: this.$Enum.ORDER_STATUS.CONFIRMED,
        },
        {
          label: "Đang vận chuyển",
          columnName: "DeliveryStatus",
          status: this.$Enum.DELIVERY_STATUS.BEING_TRANSPORTED,
        },
        {
          label: "Hoàn tất",
          columnName: "OrderStatus",
          status: this.$Enum.ORDER_STATUS.COMPLETE,
        },
        {
          label: "Bị hủy",
          columnName: "OrderStatus",
          status: this.$Enum.ORDER_STATUS.CANCELLED,
        },
      ],
      activeTabIndex: 0, // Chỉ số của tab đang được chọn mặc định là 0
    };
  },
  watch: {
    //theo dõi biến pageSize
    "filterData.pageSize": function () {
      this.getOrdersPagingData();
    },
    //theo dõi biến pageNumber
    "filterData.pageNumber": function () {
      this.getOrdersPagingData();
    },
  },
  computed: {
    // Tạo computed property để tính toán giá trị quantity cho mỗi tab
    computedTabs() {
      return this.tabs.map((tab) => {
        switch (tab.label) {
          case "Tất Cả":
            return { ...tab, quantity: this.quantityOrder };
          case "Chờ xác nhận":
            return { ...tab, quantity: this.quantityOrderWatting };
          case "Đã xác nhận":
            return { ...tab, quantity: this.quantityOrderConfirmed };
          case "Đang vận chuyển":
            return { ...tab, quantity: this.quantityOrderShipping };
          case "Hoàn tất":
            return { ...tab, quantity: this.quantityOrderComplete };
          case "Bị hủy":
            return { ...tab, quantity: this.quantityOrderCancelled };
          default:
            return tab;
        }
      });
    },
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
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
  },
  methods: {
    /**
     * Hàm thực hiện lấy tổng các đơn hàng và tổng các trạng thái đơn hàng tương ứng
     * @author LQHUY(12/04/2024)
     */
    async getTotalOrdersData() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const params = {
          UserId: this.userInfo.UserId,
        };
        const res = await orderService.GetByUserId({ params });
        if (res.status === 200) {
          this.quantityOrder = res.data.TotalRecord;
          this.quantityOrderWatting = res.data.Data.filter(
            (item) =>
              item.OrderStatus === this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
          ).length;
          this.quantityOrderConfirmed = res.data.Data.filter(
            (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CONFIRMED
          ).length;
          this.quantityOrderShipping = res.data.Data.filter(
            (item) =>
              item.DeliveryStatus ===
              this.$Enum.DELIVERY_STATUS.BEING_TRANSPORTED
          ).length;
          this.quantityOrderDelivered = res.data.Data.filter(
            (item) =>
              item.DeliveryStatus === this.$Enum.DELIVERY_STATUS.DELIVERIED
          ).length;
          this.quantityOrderCancelled = res.data.Data.filter(
            (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
          ).length;
          this.quantityOrderComplete = res.data.Data.filter(
            (item) => item.OrderStatus === this.$Enum.ORDER_STATUS.COMPLETE
          ).length;
          this.$emitter.emit("toggleShowLoading", false);
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Hàm thực hiện lấy danh sách các bản ghi có phân trang
     * @param columnName tên cột cần lọc
     * @param value giá trị của cột cần lọc
     * @param index vị trí chọn
     * @author LQHUY
     */
    async getOrdersPagingData(columnName = null, value = null, index = 0) {
      try {
        this.activeTabIndex = index;
        this.$emitter.emit("toggleShowLoading", true);
        const params = {
          UserId: this.userInfo.UserId,
          PageSize: this.filterData.pageSize,
          PageNumber: this.filterData.pageNumber,
          ColumnName: columnName,
          Value: value ? `${value}` : null,
        };
        const res = await orderService.GetByUserId({ params });
        if (res.status === 200) {
          this.orders = res.data.Data;
          this.totalPage = res.data.TotalPage;
          this.$emitter.emit("toggleShowLoading", false);
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Thực hiện đến trang chi tiết đơn hàng khi click Xem chi tiết
     * @param orderId Id đơn hàng
     * @author LQHUY
     */
    goToOrderDetail(orderId) {
      this.$router.push("order/order-detail/" + orderId);
    },
  },
};
</script>
<style scoped>
@import url(./order.css);
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
</style>
