<template>
  <div class="chart-container">
      <Line
        v-if="loaded"
        id="my-chart-id"
        :options="chartOptions"
        :data="chartData"
      />
  </div>
</template>

<script>
import { Line } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  LineElement,
  PointElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import orderService from "@/utils/OrderService";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale,
  LinearScale
);
export default {
  name: "BarChart",
  components: { Line },
  created() {
    this.getCalculateTotalSalesPerMonth();
  },
  methods: {
    async getCalculateTotalSalesPerMonth() {
      try {
        const params = {
          year: 2024,
        };
        const res = await orderService.CalculateTotalSalesPerMonth({ params });
        if (res.status === 200) {
          res.data.forEach((item) => {
            this.chartData.labels.push(`Tháng ${item.Month}`);
            this.data.push(item.TotalSales);
          });
          this.chartData.datasets[0].data = this.data;
          this.loaded = true;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      data: [],
      chartData: {
        labels: [],
        datasets: [
          {
            label: "Doanh theo thu từng tháng năm 2024",
            backgroundColor: "#BEE1F3",
            borderColor: "#10A5B2",
            data: [],
          },
        ],
      },
      chartOptions: {
        responsive: true,
      },
      loaded: false,
    };
  },
};
</script>
<style scoped>
.chart-container {
  height: 420px; /* Đặt chiều cao của container chứa biểu đồ */
  margin-left: 60px;
}
</style>
