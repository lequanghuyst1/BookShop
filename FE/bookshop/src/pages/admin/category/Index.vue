<template>
  <div class="content__header">
    <div class="content__header-title">
      <h3>Quản lý danh mục</h3>
    </div>

    <div class="content__header-group-button">
      <tippy content="Thêm mới" placement="bottom" animation="scale">
        <MButton
          class="m-button-icon"
          icon="fa-solid fa-plus"
          id="btn-add-employee"
          text="Thêm mới"
          style="font-weight: 600"
          @click="onBtnAddNew"
        >
        </MButton>
      </tippy>
    </div>
  </div>
  <div class="content__body">
    <div class="content__toolbar">
      <div class="content__toolbar-left">
        <div class="content__toolbar-left--action" style="display: flex">
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
            icon="fa-solid fa-trash"
            class="m-button-icon m-button--sub m-button-none"
            id="btn-delete-all"
            text="Xóa tất cả"
            @click="btnDeleteAllEmployee"
          >
          </MButton>
        </div>
      </div>
      <div class="content__toolbar-right">
        <div class="content__toolbar-search">
          <MInputIcon
            refEl="txtSearchString"
            v-model="searchString"
            placeholder="Tìm kiếm theo tên danh mục"
          ></MInputIcon>
        </div>
      </div>
    </div>

    <!-- Table -->

    <MTable
      :columnsTable="columnsTable"
      idObject="categoryId"
      codeObject="categoryCode"
      :pageData="pageData"
      :selectAll="selectAll"
      @toggleShowForm="toggleShowForm"
      @updateItemId="updateItemId"
      @updateListItemId="updateListItemId"
      @updateItemIdClone="updateItemIdClone"
      @toggleShowToolbarAction="toggleShowToolbarAction"
      @updateTotalRecordSelected="updateTotalRecordSelected"
    >
    </MTable>

    <!-- Pagination -->
    <MPagination
      :total="totalRecord"
      :totalPage="totalPage"
      v-model:pageNumber="pageNumber"
      v-model:pageSize="pageSize"
    ></MPagination>
    <CategoryDetail
      v-if="isShowForm"
      @loadData="loadData"
      :categoryIdSelected="categoryIdSelected"
    ></CategoryDetail>
  </div>
</template>
<script>
import categoryColumns from "@/js/data/category";
import categoryService from "@/utils/CategoryService";
import CategoryDetail from "./CategoryDetail.vue";
export default {
  name: "CategoryPage",
  components: { CategoryDetail },
  created() {
    this.loadData();
    this.$emitter.on("toggleShowForm", this.toggleShowForm);
    this.$emitter.on("deleteCategory", this.deleteCategory);
  },
  mounted() {
    this.$emitter.emit("toggleShowLoadingTable", true);
  },
  beforeUnmount() {
    this.$emitter.off("toggleShowForm", this.toggleShowForm);
    this.$emitter.off("deleteCategory", this.deleteCategory);
  },
  methods: {
    async loadData() {
      this.$emitter.emit("toggleShowLoadingTable", true);
      try {
        const res = await categoryService.getAll();
        switch (res.status) {
          case 200:
            this.pageData = res.data;
            this.$emitter.emit("toggleShowLoadingTable", false);
            break;
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoadingTable", false);
      }
    },
    onBtnAddNew() {
      this.toggleShowForm(true);
      this.categoryIdSelected = null;
    },
    toggleShowForm(isShow) {
      this.isShowForm = isShow;
    },
    updateItemId(value) {
      this.categoryIdSelected = value;
    },
    async deleteCategory() {
      try {
        const res = await categoryService.delete(this.categoryIdSelected);
        switch (res.status) {
          case 200:
          this.$emitter.emit("toggleDialogNotice", false);
            this.$emitter.emit(
              "onShowToastMessage",
              this.$Resource[this.$languageCode].ToastMessage.Type.Success,
              "Xoá thành công",
              this.$Resource[this.$languageCode].ToastMessage.Status.Success
            );
            this.loadData();
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      columnsTable: categoryColumns,
      pageData: [],
      isShowForm: false,
      categoryIdSelected: null,
    };
  },
};
</script>
<style scoped>
@import url(./category.css);
</style>
