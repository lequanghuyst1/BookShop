<template>
  <div class="chart-container">
    <Chart
      v-show="selectedTypeRenvenueValue === 0"
      type="line"
      :data="chartData"
      :options="chartOptions"
    />
    <Chart
      v-show="selectedTypeRenvenueValue === 1"
      type="bar"
      :data="chartData"
      :options="chartOptionsBar"
    ></Chart>
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
    },
    indexAxis: {
      type: String,
    },
    selectedTypeRenvenueValue: {
      type: Number,
    },
  },
  data() {
    return {
      chartOptions: null,
      chartOptionsBar: null,
    };
  },
  mounted() {
    this.chartOptions = this.setChartOptions();
    this.chartOptionsBar = this.setChartOptionsBar();
    const canvas = document.querySelectorAll(".p-chart canvas");
    canvas.forEach((item) => {
      item.style.width = "680px";
    });
  },
  methods: {
    setChartOptionsBar() {
      const documentStyle = getComputedStyle(document.documentElement);
      const textColor = documentStyle.getPropertyValue("--text-color");
      const textColorSecondary = documentStyle.getPropertyValue(
        "--text-color-secondary"
      );
      const surfaceBorder = documentStyle.getPropertyValue("--surface-border");

      return {
        indexAxis: "y",
        maintainAspectRatio: false,
        aspectRatio: 0.8,
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
    setChartOptions() {
      const documentStyle = getComputedStyle(document.documentElement);
      const textColor = documentStyle.getPropertyValue("--text-color");
      const textColorSecondary = documentStyle.getPropertyValue(
        "--text-color-secondary"
      );
      const surfaceBorder = documentStyle.getPropertyValue("--surface-border");

      return {
        indexAxis: this.$props.indexAxis,
        maintainAspectRatio: false,
        aspectRatio: 0.8,
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
.p-chart canvas {
}
/* CSS */
.p-chart .p-chart-canvas {
  /* Chiều cao mong muốn */
}
</style>
