<template>
  <div class="content__header">
    <div class="content__header-title">
      <h3>Quản lý đơn hàng</h3>
    </div>
    <div class="content__header-group-button">
      <!-- <tippy content="Thêm mới" placement="bottom" animation="scale">
        <MButton
          class="m-button-icon"
          icon="fa-solid fa-plus"
          id="btn-add-employee"
          text="Thêm mới"
          style="font-weight: 600"
          @click="onCreateItem"
        >
        </MButton>
      </tippy> -->
    </div>
  </div>
  <div class="content__body">
    <div class="content__filter d-flex">
      <div class="item-filter">
        <Dropdown
          v-model="orderStatusSelected"
          :options="orderStatusData"
          optionLabel="title"
          placeholder="Trạng thái"
          :highlightOnSelect="false"
          class="w-full w-100"
        />
      </div>
      <div class="item-filter">
        <Dropdown
          v-model="deliveryStatusSelected"
          :options="deliveryStatusData"
          optionLabel="title"
          placeholder="Vận chuyển"
          :highlightOnSelect="false"
          class="w-full w-100"
        />
      </div>
      <div class="item-filter">
        <Dropdown
          v-model="paymentStatusSelected"
          :options="paymentStatusData"
          optionLabel="title"
          placeholder="Thanh toán"
          :highlightOnSelect="false"
          class="w-full w-100"
        />
      </div>
      <button
        @click="handleOnFilter"
        data-v-208e19d7=""
        class="search m-button"
        style="background-color: rgb(0, 81, 200)"
      >
        Lọc
      </button>
      <button
        v-if="this.filterData.filterInput.length > 0"
        @click="deleteFilterCondtion"
        data-v-208e19d7=""
        class="search m-button"
        style="background-color: rgb(0, 81, 200)"
      >
        Xóa bộ lọc
      </button>
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
            id="m-button-uncheked"
            text="Chọn tất cả các trang"
            style="color: #1565c0"
            @click="handleSelectAllPage"
          >
          </MButton>
          <MButton
            class="m-button--sub m-button-none"
            id="m-button-uncheked"
            text="Xuất file"
            style="color: #1565c0"
            @click="exportListRecord"
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
            v-model="filterData.searchString"
            placeholder="Tìm kiếm theo tên khách hàng, mã đơn hàng"
          ></MInputIcon>
        </div>
        <MButtonNoText
          v-tippy="{
            content: 'Xuất file',
            placement: 'bottom',
          }"
          @click="exportAllRecord"
          icon="fa-solid fa-file-excel"
        ></MButtonNoText>
        <MButtonNoText
          v-tippy="{
            content: 'Load dữ liệu',
            placement: 'bottom',
          }"
          @click="this.getOrdersData"
          icon="fa-solid fa-solid fa-arrow-rotate-right"
        >
        </MButtonNoText>
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
      <div class="no-data-table" v-if="this.orders.length === 0">
        Không có dữ liệu nào
      </div>
    </div>
    <MPagination
      :total="totalRecord"
      :totalPage="totalPage"
      v-model:pageNumber="filterData.pageNumber"
      v-model:pageSize="filterData.pageSize"
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
import Dropdown from "primevue/dropdown";
import { saveAs } from "file-saver";
export default {
  name: "OrderManagementPage",
  components: { Dropdown },
  created() {
    this.$emitter.on("updatePageSize", this.updatePageSize);
  },
  mounted() {
    this.getOrdersData();

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

      filterData: {
        pageSize: 10,
        pageNumber: 1,
        searchString: null,
        sortColumn: {
          columnName: "CreatedDate",
          type: this.$Enum.SORT_TYPE.DESC,
          label: "Mới nhất",
        },
        filterInput: [],
        rangeColumn: [],
      },
      //Chuỗi tìm kiếm nhanh

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

      //Vị trí filter được chọn
      selectedFilterIndex: 0, // Mặc định chọn 'Tất cả'
      orderStatusSelected: {},
      paymentStatusSelected: {},
      deliveryStatusSelected: {},
    };
  },
  watch: {
    orderStatusSelected: function (newValue) {
      if (this.filterData.filterInput.length > 0) {
        const index = this.filterData.filterInput.findIndex(
          (item) => item.ColumnName === "OrderStatus"
        );
        if (index !== -1) {
          this.filterData.filterInput.splice(index, 1);
        }
      }
      this.filterData.filterInput.push({
        ColumnName: "OrderStatus",
        Value: `${newValue.value}`,
      });
    },
    deliveryStatusSelected: function (newValue) {
      if (this.filterData.filterInput.length > 0) {
        const index = this.filterData.filterInput.findIndex(
          (item) => item.ColumnName === "DeliveryStatus"
        );
        if (index !== -1) {
          this.filterData.filterInput.splice(index, 1);
        }
      }
      this.filterData.filterInput.push({
        ColumnName: "DeliveryStatus",
        Value: `${newValue.value}`,
      });
    },
    paymentStatusSelected: function (newValue) {
      if (this.filterData.filterInput.length > 0) {
        const index = this.filterData.filterInput.findIndex(
          (item) => item.ColumnName === "PaymentStatus"
        );
        if (index !== -1) {
          this.filterData.filterInput.splice(index, 1);
        }
      }
      this.filterData.filterInput.push({
        Value: `${newValue.value}`,
        ColumnName: "PaymentStatus",
      });
    },
    //theo dõi biến pageSize
    "filterData.pageSize": function () {
      this.getOrdersData();
    },
    //Theo dõi biến pageNumber
    "filterData.pageNumber": function () {
      this.getOrdersData();
    },
    //Theo dõi biến searchString
    "filterData.searchString": function () {
      this.filterData.pageNumber = 1;
      this.getOrdersData();
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
    orderStatusData: function () {
      const data = [];
      for (const key in this.$Enum.ORDER_STATUS) {
        const value = this.$Enum.ORDER_STATUS[key];
        const title = this.$helper.hanldeValueTypeEnum("ORDER_STATUS", value);
        const item = {
          value: value,
          title: title,
        };
        data.push(item);
      }
      return data;
    },
    deliveryStatusData: function () {
      const data = [];
      for (const key in this.$Enum.DELIVERY_STATUS) {
        const value = this.$Enum.DELIVERY_STATUS[key];
        const title = this.$helper.hanldeValueTypeEnum(
          "DELIVERY_STATUS",
          value
        );
        const item = {
          value: value,
          title: title,
        };
        data.push(item);
      }
      return data;
    },
    paymentStatusData: function () {
      const data = [];
      for (const key in this.$Enum.PAYMENT_STATUS) {
        const value = this.$Enum.PAYMENT_STATUS[key];
        const title = this.$helper.hanldeValueTypeEnum("PAYMENT_STATUS", value);
        const item = {
          value: value,
          title: title,
        };
        data.push(item);
      }
      return data;
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
    async getOrdersData() {
      try {
        this.onToggleLoadingTable(true);
        let params = {
          PageSize: this.filterData.pageSize,
          PageNumber: this.filterData.pageNumber,
          SearchString: this.filterData.searchString,
          FilterInput: this.filterData.filterInput,
        };

        const res = await orderService.filter(params);
        if (res.status === 200) {
          this.onToggleLoadingTable(false, 300);
          this.orders = res.data.Data;
          this.totalPage = res.data.TotalPage;
          this.totalRecord = res.data.TotalRecord;
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
          this.getOrdersData();
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
      // location.href = "http://localhost:8080/admin/order-manegement/" + id;
      this.$router.push("order-manegement/" + id);
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
          this.getOrdersData();
          this.btnRemoveRowSelected();
        }
      } catch (error) {
        console.log(error);
      }
    },
    deleteFilterCondtion() {
      this.filterData.filterInput = [];
      this.orderStatusSelected = {};
      this.paymentStatusSelected = {};
      this.deliveryStatusSelected = {};
      this.getOrdersData();
    },
    async handleSelectAllPage() {
      try {
        let params = {
          SearchString: this.filterData.searchString,
          FilterInput: this.filterData.filterInput,
        };
        const res = await orderService.filter(params);
        if (res.status === 200) {
          this.orderIdsSelected = res.data.Data.map((item) => item.OrderId);
        }
        if (res.status === 200) {
          this.countSelectedRow();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm thực hiện Export tất cả các bản ghi vào file excel
     * Author: LQHUY(10/01/2024)
     */
    async exportAllRecord() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const excelRequest = {
          TitleHeader: "Danh sách đơn hàng",
          EntityIds: [],
          WorksheetName: "Danh sách đơn hàng",
        };
        let res = await orderService.exportRecord(excelRequest);
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        var fileName = "Danh sách đơn hàng";

        if (res.status == 200) {
          this.$emitter.emit("toggleShowLoading", false);
          // Mở cửa sổ thoại mở thư mục và cho phép thay tên file
          saveAs(blob, fileName, { autoBom: false });
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Hàm thực hiện Export danh sách các bản ghi vào file excel
     * Author: LQHUY(10/01/2024)
     */
    async exportListRecord() {
      try {
        const excelRequest = {
          TitleHeader: "Danh sách đơn hàng",
          EntityIds: this.orderIdsSelected,
          WorksheetName: "Danh sách đơn hàng",
        };
        let res = await orderService.exportRecord(excelRequest);
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        var fileName = "Danh sách đơn hàng";
        if (res.status == 200) {
          this.$emitter.emit("toggleShowLoading", false);
          // Mở cửa sổ thoại mở thư mục và cho phép thay tên file
          saveAs(blob, fileName, { autoBom: false });
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },
    handleOnFilter() {
      this.filterData.pageNumber = 1;
      this.getOrdersData();
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
