<template>
  <div class="chart-container">
    <Chart type="line" :data="chartData" :options="chartOptions" />
  </div>
</template>

<script>
import Chart from "primevue/chart";
export default {
  components: { Chart },
  props: {
    chartData: {
      type: Object,
      required: true,
    }
  },
  data() {
    return {
      chartOptions: null,
    };
  },
  mounted() {
    this.chartOptions = this.setChartOptions();
  },
  methods: {
    setChartOptions() {
      const documentStyle = getComputedStyle(document.documentElement);
      const textColor = documentStyle.getPropertyValue("--text-color");
      const textColorSecondary = documentStyle.getPropertyValue(
        "--text-color-secondary"
      );
      const surfaceBorder = documentStyle.getPropertyValue("--surface-border");

      return {
        plugins: {
          legend: {
            labels: {
              color: textColor,
            },
          },
        },
        scales: {
          x: {
            ticks: {
              color: textColorSecondary,
            },
            grid: {
              color: surfaceBorder,
            },
          },
          y: {
            beginAtZero: true,
            ticks: {
              color: textColorSecondary,
            },
            grid: {
              color: surfaceBorder,
            },
          },
        },
      };
    },
  },
};
</script>
<style scoped>
.p-chart {
  height: 350px !important;
  width: 700px !important;
}
</style>
