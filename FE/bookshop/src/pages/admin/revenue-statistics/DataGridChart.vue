<template>
  <DataTable
    size="Normal"
    showGridlines
    stripedRows
    lazy
    :loading="loading"
    scrollable
    scrollHeight="400px"
    :value="this.$props.gridData"
  >
    <Column field="OrderCode" header="Mã đơn hàng"></Column>
    <Column field="Fullname" header="Tên khách hàng"></Column>
    <Column field="PhoneNumber" header="Số điện thoại"></Column>

    <Column field="OrderDate" header="Ngày đặt hàng">
      <template #body="slotProps">
        {{ this.$helper.formatOrderDate(slotProps.data.OrderDate) }}
      </template>
    </Column>
    <Column field="ShippingFee" header="Tiền ship">
      <template #body="slotProps">
        {{ this.$helper.formatMoney(slotProps.data.ShippingFee) }}đ
      </template></Column
    >
    <Column field="" header="Tiền hàng">
      <template #body="slotProps">
        {{
          this.$helper.formatMoney(
            slotProps.data.TotalAmount - slotProps.data.ShippingFee
          )
        }}đ
      </template></Column
    >
    <Column field="TotalAmount" header="Tổng tiền">
      <template #body="slotProps">
        {{ this.$helper.formatMoney(slotProps.data.TotalAmount) }}đ
      </template></Column
    >
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
}
.p-datatable .p-column-header-content {
  padding: 16px !important;
  height: 48px !important;
}
</style>
