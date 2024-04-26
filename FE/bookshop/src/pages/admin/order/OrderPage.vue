<template>
  <div class="content__header">
    <div class="content__header-title">
      <h3>Quản lý đơn hàng</h3>
    </div>
    <div class="content__header-group-button">
      <tippy content="Thêm mới" placement="bottom" animation="scale">
        <MButton
          class="m-button-icon"
          icon="fa-solid fa-plus"
          id="btn-add-employee"
          text="Thêm mới"
          style="font-weight: 600"
          @click="onCreateItem"
        >
        </MButton>
      </tippy>
    </div>
  </div>
  <div class="content__body">
    <div class="content__filter">
      <ul>
        <li
          class="content__filter-item"
          v-for="(item, index) in filterOptions"
          :key="index"
          @click="
            getOrdersDataFilter(item.fieldConditon, item.filterCondition, index)
          "
          :class="{ current: index === selectedFilterIndex }"
        >
          {{ item.title }}
        </li>
      </ul>
    </div>

    <div class="content__toolbar">
      <div class="content__toolbar-left">
        <div
          v-show="isShowToolbarAction"
          class="content__toolbar-left--action"
          style="display: flex; column-gap: 20px"
        >
          <p class="content__toolbar-left--selected">
            Đã chọn
            <b class="content__toolbar-left--selected-quantity">{{
              totalRecordSelected
            }}</b>
          </p>
          <MButton
            class="m-button--sub m-button-none"
            id="m-button-uncheked"
            text="Bỏ chọn"
            @click="btnRemoveRowSelected"
          >
          </MButton>
          <MButton
            class="m-button--sub m-button-none"
            id="btn-delete-all"
            text="Xác nhận tất cả"
            @click="onConfirmAllOrder"
          >
          </MButton>
        </div>
      </div>
      <div class="content__toolbar-right">
        <div style="width: 350px" class="content__toolbar-search">
          <MInputIcon
            refEl="txtSearchString"
            v-model="searchString"
            placeholder="Tìm kiếm theo tên khách hàng, mã đơn hàng"
          ></MInputIcon>
        </div>
      </div>
    </div>

    <div class="m-grid" id="order">
      <table class="m-table">
        <thead>
          <tr>
            <th class="" style="width: 50px; text-align: center">
              <input type="checkbox" v-model="selectAllRecord" />
            </th>
            <th
              :style="{
                'text-align': column.textAlign || 'left',
                'min-width': column.width || null,
              }"
              v-for="(column, index) in orderColumns"
              :key="index"
            >
              {{ column.nameField }}
            </th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody v-if="isShowLoadingTable === false">
          <tr v-for="(order, index) in orders" :key="order.OrderId">
            <td class="" style="width: 50px; text-align: center">
              <input
                type="checkbox"
                v-model="orderIdsSelected"
                :value="order.OrderId"
                @click="onClickCheckboxItem(order)"
              />
            </td>
            <td
              :style="{
                textAlign: column.textAlign || 'left',
              }"
              v-for="(column, index) in orderColumns"
              :key="index"
            >
              {{ handleCellValueByColumnType(order, column) }}
            </td>
            <td style="text-align: center; min-width: 100px">
              <div data-v-7c375989="" class="button__edit-address">
                <i
                  @click="onToggleShowAction(index)"
                  data-v-7c375989=""
                  class="fa-solid fa-pen-to-square"
                ></i>
                <div
                  v-if="isShowActionRowTable[index]"
                  data-v-7c375989=""
                  class="group-action-address"
                >
                  <ul data-v-7c375989="" class="p-0 mb-0">
                    <li
                      v-if="
                        order.OrderStatus ===
                          this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION &&
                        order.OrderStatus !== this.$Enum.ORDER_STATUS.CANCELLED
                      "
                      @click="handleOnConfirmOrder(order, index)"
                      data-v-7c375989=""
                      class="action-item"
                    >
                      Xác nhận
                    </li>
                    <li
                      @click="goToOrderDetailPage(order.OrderId)"
                      data-v-7c375989=""
                      class="action-item"
                    >
                      Xem chi tiết
                    </li>
                  </ul>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <MLoadingData v-if="isShowLoadingTable"></MLoadingData>
    </div>
    <MPagination
      :total="totalRecord"
      :totalPage="totalPage"
      v-model:pageNumber="pageNumber"
      v-model:pageSize="pageSize"
    >
    </MPagination>
  </div>

  <MDialog
    v-if="isShowDialog"
    title="Xác nhận "
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
        class="m-button--sub"
        text="Hủy"
        @click="this.isShowDialog = false"
      ></MButton>
    </template>
    <template #footerRight>
      <MButton @click="hanldeOnConfirmAllOrder" text="Xác nhận"></MButton>
    </template>
  </MDialog>
</template>
<script>
import orderService from "@/utils/OrderService";
import orderColumns from "@/js/data/orderColumns";

export default {
  name: "OrderManagementPage",
  created() {
    this.$emitter.on("updatePageSize", this.updatePageSize);
  },
  mounted() {
    this.getOrdersDataFilter(null, null, this.selectedFilterIndex);

    document.addEventListener("click", (e) => {
      if (!e.target.closest(".button__edit-address")) {
        this.isShowActionRowTable = [];
      }
    });
    document.title = "Quản lý đơn hàng";

  },
  beforeUnmount() {
    this.$emitter.off("updatePageSize", this.updatePageSize);
  },
  provide() {
    return {
      pageSizeDefault: 10,
    };
  },
  data() {
    return {
      //Lưu danh sách các đơn hàng
      orders: [],
      //Columns của bảng
      orderColumns: orderColumns,

      //Lưu tổng số bản ghi
      totalRecord: 0,
      //Lưu tổng số trang
      totalPage: 0,
      //Số trang hiện tại
      pageNumber: 1,
      //Số lượng bản ghi trong 1 trang
      pageSize: 10,
      //Chuỗi tìm kiếm nhanh
      searchString: null,

      //Lưu danh sách ẩn hiện của row action table
      isShowActionRowTable: [],
      //Lưu danh sách id của order được chọn
      orderIdsSelected: [],
      //Lưu tổng số bản ghi được chọn
      totalRecordSelected: 0,
      //Lưu giá trị ẩn hiện toolbar
      isShowToolbarAction: false,
      //Lưu giá trị ẩn hiện loading table
      isShowLoadingTable: false,
      //Giá trị ẩn hiện dialog
      isShowDialog: false,
      //Các giá trị của filter
      filterOptions: [
        {
          title: "Tất cả",
          fieldConditon: null,
          filterCondition: null,
        },
        {
          title: "Đơn hàng chờ xác nhận",
          fieldConditon: "OrderStatus",
          filterCondition: this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION,
        },
        {
          title: "Đơn hàng chưa giao",
          fieldConditon: "DeliveryStatus",
          filterCondition: this.$Enum.DELIVERY_STATUS.NOT_DELIVERY,
        },
        {
          title: "Đơn hàng chưa thanh toán",
          fieldConditon: "PaymentStatus",
          filterCondition: this.$Enum.PAYMENT_STATUS.UNPAID,
        },
      ],
      //Vị trí filter được chọn
      selectedFilterIndex: 0, // Mặc định chọn 'Tất cả'
    };
  },
  watch: {
    //theo dõi biến pageSize
    pageSize: function () {
      this.getOrdersDataFilter(null, null, 0);
    },
    //Theo dõi biến pageNumber
    pageNumber: function () {
      this.getOrdersDataFilter(null, null, 0);
    },
    //Theo dõi biến searchString
    searchString: function () {
      this.getOrdersDataFilter(null, null, 0);
    },
  },
  computed: {
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
    //biến chọn tất cả các bản ghi
    selectAllRecord: {
      // Khi truy cập giá trị computed property
      get: function () {
        return this.orders
          ? this.orders
              .map((item) => item.OrderId)
              .every((ele) => this.orderIdsSelected.includes(ele)) &&
              this.orderIdsSelected.length >= this.orders.length &&
              this.orderIdsSelected.length > 0
          : false;
      },
      // Khi thay đổi giá trị computed property
      set: function (value) {
        let orderIdsSelected = [];
        if (value && this.orders != null) {
          //duyệt dữ liệu push id vào mảng
          this.orders.forEach((item) => {
            let id = item.OrderId;
            //nếu trong orderIdsSelected chưa tồn tại id thì mới push
            if (!this.orderIdsSelected.map((item) => item).includes(id)) {
              orderIdsSelected.push(item.OrderId);
            }
          });
          this.orderIdsSelected = [
            ...this.orderIdsSelected,
            ...orderIdsSelected,
          ];
          this.countSelectedRow();
        } else {
          if (this.selectAll === false) {
            this.orderIdsSelected = [];
            this.countSelectedRow();
            return;
          }
          this.orderIdsSelected = this.orderIdsSelected.filter((ele) => {
            return !this.orders.map((item) => item.OrderId).includes(ele);
          });
          this.countSelectedRow();
        }
      },
    },
  },
  methods: {
    /**
     * Thực hiện show hoặc hide action row item
     * @param {boolean} index
     *@author LQHUY(19/04/2024)
     */
    onToggleShowAction(index) {
      if (this.isShowActionRowTable[index] === true) {
        this.isShowActionRowTable[index] = false;
        return;
      }
      this.isShowActionRowTable = this.orders.map(() => false);
      this.isShowActionRowTable[index] = true;
    },

    /**
     * Thực hiện lấy danh sách các bản ghi theo điều kiên
     * @param {string} fieldConditon
     * @param {enum} filterCondition
     * @param {number} index
     *@author LQHUY(19/04/2024)
     */
    async getOrdersDataFilter(fieldConditon, filterCondition, index) {
      this.selectedFilterIndex = index;
      try {
        this.onToggleLoadingTable(true);
        let params = !this.searchString
          ? {
              pageSize: this.pageSize,
              pageNumber: this.pageNumber,
            }
          : {
              searchString: this.searchString,
              pageSize: this.pageSize,
              pageNumber: this.pageNumber,
            };
        const res = await orderService.getFilterPaging({ params });
        if (res.status === 200) {
          this.onToggleLoadingTable(false, 300);
          switch (fieldConditon) {
            case "OrderStatus":
              this.orders = res.data.Data.filter(
                (item) => item.OrderStatus === filterCondition
              );
              this.totalRecord = this.orders.length;
              this.totalPage = Math.ceil(this.orders.length / this.pageSize);
              
              break;
            case "DeliveryStatus":
              this.orders = res.data.Data.filter(
                (item) => item.DeliveryStatus === filterCondition
              );
              this.totalRecord = this.orders.length;
              this.totalPage = Math.ceil(this.orders.length / this.pageSize);
              break;
            case "PaymentStatus":
              this.orders = res.data.Data.filter(
                (item) => item.PaymentStatus === filterCondition
              );
              this.totalRecord = this.orders.length;
              this.totalPage = Math.ceil(this.orders.length / this.pageSize);
              break;
            default:
              this.orders = res.data.Data;
              this.totalPage = res.data.TotalPage;
              this.totalRecord = res.data.TotalRecord;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Xử lý các kiểu dữ liệu của từng ô trên table
     * @param {*} data
     * @param {*} column
     * @author LQHUY(15/04/2024)
     * @returns dữ liệu được format
     *@author LQHUY(19/04/2024)
     */
    handleCellValueByColumnType(data, column) {
      try {
        if (column.type === "money") {
          return `${this.$helper.formatMoney(data[column.field])} đ`;
        } else if (column.type === "datetime") {
          return this.$helper.formatOrderDate(data[column.field]);
        } else if (column.type === "enum") {
          return this.$helper.hanldeValueTypeEnum(
            column.enumType,
            data[column.field]
          );
        } else {
          return data[column.field];
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện xác nhận đơn hàng khi click xác nhận
     * @param {object} order
     * @param {number} index
     *@author LQHUY(19/04/2024)
     */
    async handleOnConfirmOrder(order, index) {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        order.OrderStatus = this.$Enum.ORDER_STATUS.CONFIRMED;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(order));
        const res = await orderService.put(order.OrderId, formData);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Xác nhận thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.isShowActionRowTable[index] = false;
          this.getOrdersDataFilter(null, this.selectedFilterIndex);
          this.$emitter.emit("toggleShowLoading", false);
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Đi đến trang chi tiết của đơn hàng
     * @param {string} id
     *@author LQHUY(19/04/2024)
     *
     */
    goToOrderDetailPage(id) {
      location.href = "http://localhost:8080/admin/order-manegement/" + id;
    },

    /**
     * Chọn và hủy chọn 1 dòng trên table
     * @param {object} data
     * Author: LQHUY (6/12/2023)
     */
    onClickCheckboxItem(order) {
      if (this.orderIdsSelected.indexOf(order.OrderId) === -1) {
        this.orderIdsSelected.push(order.OrderId);
      } else {
        console.log(this.orderIdsSelected);
        this.orderIdsSelected = this.orderIdsSelected.filter(
          (item) => item !== order.OrderId
        );
      }
      this.countSelectedRow();
    },

    /**
     * Hàm cập nhật số lượng các dòng được chọn
     * @returns số lượng dòng được chọn
     * Author: LQHUY (6/12/2023)
     */
    countSelectedRow() {
      try {
        this.totalRecordSelected = this.orderIdsSelected.filter(
          (item) => item
        ).length;
        if (this.totalRecordSelected === 0) {
          this.isShowToolbarAction = false;
          return;
        }
        this.isShowToolbarAction = true;
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Thực hiện show hoặc hide loading của table
     * @param {boolean} isShow
     * @param {number} duration
     */
    onToggleLoadingTable(isShow, duration) {
      if (isShow) {
        this.isShowLoadingTable = isShow;
      }
      if (!isShow) {
        setTimeout(() => {
          this.isShowLoadingTable = isShow;
        }, duration);
      }
    },

    /**
     * Hàm thực hiện cập nhật giá trị pageSize
     * @param {number} value
     * @author LQHUY(19/03/2024)
     */
    updatePageSize(value) {
      this.pageSize = value;
    },

    /**
     * Thực hiện bỏ chọn tất cả các bạn ghi được chọn
     * @author LQHUY(19/03/2024)
     */
    btnRemoveRowSelected() {
      this.orderIdsSelected = [];
      this.isShowToolbarAction = false;
    },

    /**
     * Thực hiện show dialog xác nhận khi click btn xác nhận tất cả
     * @author LQHUY(19/03/2024)
     */
    onConfirmAllOrder() {
      this.isShowDialog = true;
      this.messageDialog =
        "Bạn có chắc chắn muốn xác nhận tất cả các đơn hàng?";
    },

    /**
     * Thực hiện xác nhận tất cả các đơn hàng
     * @author LQHUY(19/03/2024)
     */
    async hanldeOnConfirmAllOrder() {
      try {
        const res = await orderService.ConfirmAll();
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Xác nhận tất cả thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.isShowDialog = false;
          this.getOrdersDataFilter();
          this.btnRemoveRowSelected(null, null, 0);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
@import url(./order.css);
.action-item {
  text-align: left !important;
}
.action-item:hover {
  background-color: #f1ffef !important;
  color: #333 !important;
}
.group-action-address {
  min-width: 120px !important;
}
</style>
