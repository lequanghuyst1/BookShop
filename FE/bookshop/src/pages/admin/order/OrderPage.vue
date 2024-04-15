<template>
  <div class="m-grid">
    <table class="m-table" id="order">
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
      <tbody>
        <tr v-for="(order, index) in orders" :key="order.OrderId">
          <td class="" style="width: 50px; text-align: center">
            <input type="checkbox" v-model="selectAllRecord" />
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
                      order.OrderStatus ==
                        this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION ||
                      order.ORDER_STATUS === this.$Enum.ORDER_STATUS.CANCELLED
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
                    Xem và xác nhận
                  </li>
                </ul>
              </div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <MPagination
    :total="totalRecord"
    :totalPage="totalPage"
    v-model:pageNumber="pageNumber"
    v-model:pageSize="pageSize"
  >
  </MPagination>
</template>
<script>
import orderService from "@/utils/OrderService";
import orderColumns from "@/js/data/orderColumns";

export default {
  name: "OrderManagementPage",
  created() {
    this.getOrdersData();
  },
  data() {
    return {
      orders: [],
      orderColumns: orderColumns,
      totalRecord: 0,
      totalPage: 0,
      pageNumber: 1,
      pageSize: 10,
      isShowActionRowTable: [],
    };
  },
  computed: {
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
  },
  methods: {
    onToggleShowAction(index) {
      if (this.isShowActionRowTable[index] === true) {
        this.isShowActionRowTable[index] = false;
        return;
      }
      this.isShowActionRowTable = this.orders.map(() => false);
      this.isShowActionRowTable[index] = true;
    },
    async getOrdersData() {
      try {
        const res = await orderService.getAll();
        if (res.status === 200) {
          this.orders = res.data;
          this.totalRecord = this.orders.length;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     *
     * @param {*} data
     * @param {*} column
     * @author LQHUY(15/04/2024)
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
    goToOrderDetailPage(id) {
      location.href = "http://localhost:8080/admin/order-manegement/" + id;
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
