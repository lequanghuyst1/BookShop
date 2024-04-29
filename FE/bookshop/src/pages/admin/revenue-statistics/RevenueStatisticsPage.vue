<template>
  <div class="revenue-statistics">
    <div>Báo cáo doanh thu</div>
    <div class="filter-wrap">
      <div
        class="filter-item"
        v-for="(type, index) in typeRenvenueStatistics"
        :class="{ active: selectedTypeRenvenueValue === type.value }"
        :key="index"
        @click="
          () => {
            this.selectedTypeRenvenueValue = type.value;
            this.indexAxis = type.indexAxis;
          }
        "
      >
        {{ type.typeName }}
      </div>
    </div>
    <div class="condition-wrap">
      <div class="select-filter">
        <div class="item">
          <div class="item-title">Loại thời gian</div>
          <div class="item-condition">
            <Dropdown
              v-model="selectedTypeOfTime"
              :options="typeOfTime"
              optionLabel="title"
              placeholder="Chọn loại thời gian"
              :highlightOnSelect="false"
              class="w-full w-100"
            />
          </div>
        </div>
        <div class="item">
          <div class="item-title">Danh mục</div>
          <div class="item-condition">
            <MCombobox
              propValue="CategoryId"
              propText="CategoryName"
              :dataCombobox="categories"
              v-model="selectedCategoryId"
              id="cbCategory"
            ></MCombobox>
          </div>
        </div>
        <div class="item">
          <div class="item-title">Lọc tất cả</div>
          <div class="item-condition">
            <Dropdown
              v-model="selectedTypeOfTime"
              :options="filter"
              optionLabel="title"
              placeholder="Tất cả"
              :highlightOnSelect="false"
              class="w-full w-100"
            />
          </div>
        </div>
      </div>
      <dic class="">
        <div
          v-if="selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.DATE"
          class="time wrap-date"
        >
          <div class="item">
            <div class="item-title">Ngày bắt đầu</div>
            <div class="item-condition">
              <Calendar
                placeholder="DD/MM/YYYY"
                v-model="rangeDate.startDate"
                showIcon
                :showOnFocus="false"
                iconDisplay="input"
                class="w-100"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Ngày kết thúc</div>
            <div class="item-condition">
              <Calendar
                v-model="rangeDate.endDate"
                placeholder="DD/MM/YYYY"
                showIcon
                :showOnFocus="false"
                iconDisplay="input"
                dateFormat="dd/mm/yy"
                class="w-100"
              />
            </div>
          </div>
        </div>
        <div
          v-if="selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.MONTH"
          class="time wrap-month"
        >
          <div class="item">
            <div class="item-title">Tháng bắt đầu</div>
            <div class="item-condition">
              <Calendar
                placeholder="MM/YYYY"
                v-model="rangeMonth.startMonth"
                showIcon
                view="month"
                :showOnFocus="false"
                iconDisplay="input"
                dateFormat="mm/yy"
                class="w-100"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Tháng kết thúc</div>
            <div class="item-condition">
              <Calendar
                v-model="rangeMonth.endMonth"
                placeholder="MM/YYYY"
                showIcon
                view="month"
                :showOnFocus="false"
                iconDisplay="input"
                dateFormat="mm/yy"
                class="w-100"
              />
            </div>
          </div>
        </div>
        <div
          v-if="selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.YEAR"
          class="time wrap-year"
        >
          <div class="item">
            <div class="item-title">Năm bắt đầu</div>
            <div class="item-condition">
              <Calendar
                placeholder="YYYY"
                v-model="rangeYear.startYear"
                showIcon
                view="year"
                :showOnFocus="false"
                iconDisplay="input"
                dateFormat="yy"
                class="w-100"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Năm kết thúc</div>
            <div class="item-condition">
              <Calendar
                v-model="rangeYear.endYear"
                placeholder="YYYY"
                showIcon
                view="year"
                :showOnFocus="false"
                iconDisplay="input"
                dateFormat="yy"
                class="w-100"
              />
            </div>
          </div>
        </div>
        <div class="item">
          <div class="item-condition">
            <button
              @click="getDataChartAndGridWithTypeRevenue"
              class="search m-button"
              style="background-color: #0051c8"
            >
              Tìm kiếm
            </button>
          </div>
        </div>
      </dic>
    </div>
    <div class="revenue">
      <div class="revenue-item">
        <div class="revenue-item__icon">
          <i class="fa-solid fa-sack-dollar"></i>
        </div>
        <div class="revenue-item__content">
          <p>
            <b>{{ this.$helper.formatMoney(this.totalRevenue) }}đ</b>
          </p>
          <p>Tổng doanh thu</p>
        </div>
      </div>
      <div class="revenue-item">
        <div class="revenue-item__icon">
          <i class="fa-solid fa-sack-dollar"></i>
        </div>
        <div class="revenue-item__content">
          <p>
            <b>{{ this.$helper.formatMoney(this.totalRevenueByCondition) }}đ</b>
          </p>
          <p>Doanh thu theo thời gian</p>
        </div>
      </div>
      <div class="revenue-item">
        <div class="revenue-item__icon">
          <i class="fa-solid fa-sack-dollar"></i>
        </div>
        <div class="revenue-item__content">
          <p><b>288.002.111</b></p>
          <p>Doanh thu từ thanh toán Online</p>
        </div>
      </div>
      <div class="revenue-item">
        <div class="revenue-item__icon">
          <i class="fa-solid fa-sack-dollar"></i>
        </div>
        <div class="revenue-item__content">
          <p><b>288.002.111</b></p>
          <p>Doanh thu từ COD</p>
        </div>
      </div>
    </div>
    <div class="revenue-chart mt-2">
      <div class="revenue-chart-title f-flex justify-content-between">
        <div class="d-flex gap-2">
          <p
            class="view-mode"
            @click="this.viewMode = 1"
            :class="{ active: this.viewMode === 1 }"
          >
            Tổng quan
          </p>
          <p
            class="view-mode"
            @click="this.viewMode = 2"
            :class="{ active: this.viewMode === 2 }"
          >
            Chi tiết
          </p>
        </div>
        <p
          class="view-mode"
          style="background-color: #29c24d; color: #fff"
          @click="hanldeOnExport"
        >
          Xuất Excel
        </p>
      </div>
      <div v-if="this.viewMode === 1" class="wrap-chart">
        <div class="wrap-chart-title">Doanh thu theo thời gian</div>
        <RevenueChart
          :chartData="chartData"
          :selectedTypeRenvenueValue="selectedTypeRenvenueValue"
        ></RevenueChart>
      </div>
      <div v-if="this.viewMode === 2" class="data-grid">
        <DataGridChart :gridData="gridData" :columns="columns"></DataGridChart>
      </div>
    </div>
  </div>
</template>
<script>
import Dropdown from "primevue/dropdown";
import Calendar from "primevue/calendar";
import RevenueChart from "./RevenueChart.vue";
import orderService from "@/utils/OrderService";
import DataGridChart from "./DataGridChart.vue";
import { saveAs } from "file-saver";
import categoryService from "@/utils/CategoryService";
export default {
  created() {
    this.setUpTimeFilter();
  },
  mounted() {
    this.getTotalRevenue();

    this.getDataChartAndGridWithTypeRevenue();
    this.getCategoriesData();
    document.title = "Thống kê doanh thu";
  },
  components: { Dropdown, Calendar, RevenueChart, DataGridChart },

  data() {
    return {
      typeOfTime: [
        { value: this.$Enum.TYPE_OF_TIME.DATE, title: "Báo cáo theo ngày" },
        { value: this.$Enum.TYPE_OF_TIME.MONTH, title: "Báo cáo theo tháng" },
        { value: this.$Enum.TYPE_OF_TIME.YEAR, title: "Báo cáo theo năm" },
      ],

      typeRenvenueStatistics: [
        {
          typeName: "Thời gian",
          value: 0,
        },
        {
          typeName: "Sản phẩm",
          value: 1,
        },
      ],
      selectedTypeRenvenueValue: 0,

      totalRevenue: 0,
      totalRevenueByCondition: 0,

      selectedTypeOfTime: {
        value: this.$Enum.TYPE_OF_TIME.DATE,
        title: "Báo cáo theo ngày",
      },

      rangeDate: {
        startDate: null,
        endDate: null,
      },
      rangeMonth: {
        startMonth: null,
        endMonth: null,
      },
      rangeYear: {
        startYear: null,
        endYear: null,
      },

      fromDate: null,
      toDate: null,

      chartData: {},

      viewMode: 1,

      gridData: [],

      columns: [],

      revenueByTimeColumns: [
        {
          field: "OrderCode",
          nameField: "Mã đơn hàng",
          class: "table-align-text",
          width: "132px",
        },
        {
          field: "Fullname",
          nameField: "Tên khách hàng",
          class: "table-align-text",
          width: "132px",
        },
        {
          field: "PhoneNumber",
          nameField: "Số điện thoại",
          class: "table-align-text",
          width: "132px",
        },
        {
          field: "OrderDate",
          nameField: "Ngày đặt hàng",
          class: "table-align-text",
          width: "132px",
          textAlign: "center",
          type: "orderDate",
        },
        {
          field: "ShippingFee",
          nameField: "Tiền ship",
          class: "table-align-text",
          width: "132px",
          textAlign: "center",
          type: "money",
        },
        {
          field: "TotalProductCost",
          nameField: "Tiền hàng",
          class: "table-align-text",
          width: "132px",
          textAlign: "center",
          type: "money",
        },
        {
          field: "TotalAmount",
          nameField: "Tổng tiền",
          class: "table-align-text",
          width: "132px",
          textAlign: "center",
          type: "money",
        },
      ],
      revenueByProductColumns: [
        {
          field: "BookName",
          nameField: "Tên sách",
          class: "table-align-text",
          width: "132px",
        },
        {
          field: "TotalQuantitySold",
          nameField: "Số lượng",
          class: "table-align-text",
          width: "132px",
        },
        {
          field: "Profit",
          nameField: "Tổng tiền",
          class: "table-align-text",
          width: "132px",
          textAlign: "right",
          type: "Boney",
        },
      ],
      categories: [],
      selectedCategoryId: null,
      filter: [
        {
          value: 0,
          title: "Tất cả",
        },
      ],
    };
  },
  watch: {
    "selectedTypeOfTime.value": function () {
      this.rangeMonth = {};
      this.rangeYear = {};
      this.rangeYear = {};
    },
    "rangeDate.startDate": function (newValue) {
      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.DATE) {
        this.fromDate = this.$helper.formatDate(newValue, true);
      }
    },
    "rangeDate.endDate": function (newValue) {
      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.DATE) {
        this.toDate = this.$helper.formatDate(newValue, true);
      }
    },
    "rangeMonth.startMonth": function (newValue) {
      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.MONTH) {
        this.fromDate = this.$helper.formatDate(newValue, true);
      }
    },
    "rangeMonth.endMonth": function (newValue) {
      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.MONTH) {
        this.toDate = this.$helper.formatDate(newValue, true);
      }
    },
    "rangeYear.startYear": function (newValue) {

      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.YEAR) {
        this.fromDate = this.$helper.formatDate(newValue, true);
      }
    },
    "rangeYear.endYear": function (newValue) {
      if (this.selectedTypeOfTime.value === this.$Enum.TYPE_OF_TIME.YEAR) {
        this.toDate = this.$helper.formatDate(newValue, true);
      }
    },
    selectedTypeRenvenueValue: function () {
      this.getDataChartAndGridWithTypeRevenue();
    },
  },
  methods: {
    setUpTimeFilter() {
      const now = new Date();
      // const day = now.getDate();
      // const month = now.getMonth() + 1;
      // const year = now.getFullYear();
      this.rangeDate.endDate = now;

      // Lấy ra timestamp của thời điểm hiện tại
      const currentTimeStamp = now.getTime();
      // Lấy ra timestamp của thời điểm 7 ngày trước
      const sevenDaysAgoTimeStamp = currentTimeStamp - 7 * 24 * 60 * 60 * 1000; // 7 ngày * 24 giờ * 60 phút * 60 giây * 1000 milliseconds
      // Tạo đối tượng Date từ timestamp của thời điểm 7 ngày trước
      const sevenDaysAgoDate = new Date(sevenDaysAgoTimeStamp);
      // Format ngày và giờ 7 ngày trước theo ý muốn
      // Lưu ngày 7 ngày trước vào biến sevenDaysAgo để sử dụng trong template
      this.rangeDate.startDate = sevenDaysAgoDate;
    },

    getDataChartAndGridWithTypeRevenue() {
      if (this.selectedTypeRenvenueValue === 0) {
        this.getChartDataByTime();
        this.getGridData();
      } else {
        this.getChartDataByProduct();
      }
    },

    async getChartDataByTime() {
      try {
        const params = {
          typeOfTime: this.selectedTypeOfTime.value,
          fromDate: this.fromDate,
          toDate: this.toDate,
          categoryId: this.selectedCategoryId,
        };
        const res = await orderService.calculateTotalAmountByTypeOfTime({
          params,
        });
        if (res.status === 200) {
          const label = res.data.map((item) => item.order_date);
          const data = res.data.map((item) => item.total_order_value);

          this.totalRevenueByCondition = data.reduce(
            (previousValue, currentValue) => previousValue + currentValue,
            0
          );
          this.chartData = {
            labels: label,
            datasets: [
              {
                label: "VNĐ",
                type: "line",
                backgroundColor: ["rgba(249, 115, 22, 0.2)"],
                borderColor: ["rgb(249, 115, 22)"],
                borderWidth: 1,
                data: data,
              },
            ],
          };
          this.columns = this.revenueByTimeColumns;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async getChartDataByProduct() {
      try {
        const params = {
          typeOfTime: this.selectedTypeOfTime.value,
          fromDate: this.fromDate,
          toDate: this.toDate,
          quantityFilter: 10,
          categoryId: this.selectedCategoryId,
        };
        const res = await orderService.getRevenueByProduct({
          params,
        });
        if (res.status === 200) {
          const label = res.data.map((item) => item.BookName);
          const data = res.data.map((item) => item.Profit);

          this.totalRevenueByCondition = data.reduce(
            (previousValue, currentValue) => previousValue + currentValue,
            0
          );
          this.chartData = {
            labels: label,
            datasets: [
              {
                label: "Doanh thu",
                type: "bar",
                backgroundColor: ["#3466D1"],
                borderColor: ["3466D1"],
                borderWidth: 1,
                data: data,
              },
            ],
          };
          this.gridData = res.data;
          this.columns = this.revenueByProductColumns;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getTotalRevenue() {
      try {
        const res = await orderService.getTotalRevenvue();
        if (res.status === 200) {
          this.totalRevenue = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    async getGridData() {
      try {
        const params = {
          typeOfTime: this.selectedTypeOfTime.value,
          fromDate: this.fromDate,
          toDate: this.toDate,
          categoryId: this.selectedCategoryId,
        };
        const res = await orderService.getOrderByTypeOfTime({
          params,
        });
        if (res.status === 200) {
          this.gridData = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getCategoriesData() {
      try {
        const res = await categoryService.getAll();
        if (res.status == 200) {
          this.categories = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async hanldeOnExport() {
      try {
        if(this.selectedTypeRenvenueValue === 0){
          await this.exportRenvenueByTime();
        }
        else if(this.selectedTypeRenvenueValue === 1){
          await this.exportRenvenueByProduct();

        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },
    async exportRenvenueByTime() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const res = await orderService.exportRevenueByTime(this.gridData);
        console.log(res);
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        var fileName = this.$Resource[this.$languageCode].TEXT.FileNameExcel;
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

    async exportRenvenueByProduct() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const res = await orderService.exportRevenueByProduct(this.gridData);
        console.log(res);
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        var fileName = this.$Resource[this.$languageCode].TEXT.FileNameExcel;
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
  },
};
</script>
<style scoped>
@import url(./revenue.css);
</style>
