<template>
  <DataTable
    size="Normal"
    showGridlines
    stripedRows
    lazy
    :loading="loading"
    scrollable
    scrollHeight="380px"
    :value="this.$props.gridData"
  >
    <Column
      v-for="(col, index) in this.$props.columns"
      :header="col.nameField"
      :key="index"
      :style="{ textAlign: col.textAlign || 'left' }"
    >
      <template #body="slotProps">
        {{ setValueData(slotProps.data, col) }}
      </template>
    </Column>
  </DataTable>
</template>
<script>
import DataTable from "primevue/datatable";
import Column from "primevue/column";
// import ColumnGroup from 'primevue/columngroup';   // optional
// import Row from 'primevue/row';             // optional

export default {
  mounted() {
    this.loadLazyData();
    this.loading = true;
  },
  components: { DataTable, Column },
  props: {
    gridData: {
      type: Array,
      required: true,
    },
    columns: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      loading: false,
    };
  },
  methods: {
    loadLazyData() {
      this.loading = true;
      setTimeout(() => {
        this.loading = false;
      }, 500);
    },

    /**
     * Set dữ liệu cho từng ô trên table
     * @param {object} data
     * @param {string} col
     * Author: LQHUY(06/12/2023)
     */
    setValueData(data, col) {
      try {
        if (col.type === "enum") {
          return this.$helper.hanldeValueTypeEnum(
            col.enumType,
            data[col.field]
          );
        } else if (col.type === "money") {
          return this.$helper.formatMoney(data[col.field]) + "đ";
        } else if (col.type === "orderDate") {
          return this.$helper.formatOrderDate(data[col.field]);
        } else {
          return data[col.field];
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
.p-datatable-thead th {
  padding: 16px !important;
}

.p-datatable .p-datatable-thead > tr > th {
  padding: 16px !important;
  display: block;
  background-color: #ccc !important;

}
.p-datatable .p-column-header-content {
  padding: 16px !important;
  height: 48px !important;
}

</style>
