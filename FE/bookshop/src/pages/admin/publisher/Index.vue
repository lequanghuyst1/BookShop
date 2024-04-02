<template>
    <div class="content__header">
      <div class="content__header-title">
        <h3>Quản lý nhà xuất bản</h3>
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
              icon="fa-solid fa-trash"
              class="m-button-icon m-button--sub m-button-none"
              id="btn-delete-all"
              text="Xóa tất cả"
              @click="btnDeleteAllPublisher"
            >
            </MButton>
          </div>
        </div>
        <div class="content__toolbar-right">
          <div class="content__toolbar-search">
            <MInputIcon
              refEl="txtSearchString"
              v-model="searchString"
              placeholder="Tìm kiếm theo tên sách, tên tác giả"
            ></MInputIcon>
          </div>
        </div>
      </div>
  
      <!-- Table -->
  
      <MTable
        :columnsTable="columnsTable"
        idObject="publisherId"
        codeObject="publisherCode"
        :pageData="pageData"
        :selectAll="selectAll"
        :image="true"
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
      <PulisherDetail    
        v-if="isShowForm"
        @loadData="loadData"
        :publisherIdSelected="publisherIdSelected"
      ></PulisherDetail>
    </div>
  </template>
  <script>
  import PulisherDetail from "./PulisherDetail.vue";
  import publisherService from "@/utils/PublisherService"
  import publisherColumns from "@/js/data/publisher"
  export default {
    name: "PulisherPage",
    components: { PulisherDetail },
    created() {
      this.loadData();
      this.$emitter.on("toggleShowForm", this.toggleShowForm);
      this.$emitter.on("deletePublisher", this.deletePublisher);
      this.$emitter.on("deleteManyPublisher", this.deleteManyPublisher);
    },
    mounted() {
      this.$emitter.emit("toggleShowLoadingTable", true);
    },
    beforeUnmount() {
      this.$emitter.off("toggleShowForm", this.toggleShowForm);
      this.$emitter.off("deletePublisher", this.deletePublisher);
      this.$emitter.off("deleteManyPublisher", this.deleteManyPublisher);
    },
    watch: {
      pageNumber(newValue) {
        if (newValue) {
          this.loadData();
        }
      },
      searchString() {
        this.loadData();
      },
    },
    methods: {
      async loadData() {
        this.$emitter.emit("toggleShowLoadingTable", true);
        try {
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
          const res = await publisherService.getFilterPaging({ params });
          switch (res.status) {
            case 200:
              this.pageData = res.data.data;
              this.totalRecord = res.data.totalRecord;
              this.totalPage = res.data.totalPage;
              this.$emitter.emit("toggleShowLoadingTable", false);
              break;
          }
        } catch (error) {
          console.log(error);
          this.$emitter.emit("handleApiError", error);
          this.$emitter.emit("toggleShowLoadingTable", false);
        }
      },
      onBtnAddNew() {
        this.toggleShowForm(true);
        this.publisherIdSelected = null;
      },
      toggleShowForm(isShow) {
        this.isShowForm = isShow;
      },
      updateItemId(value) {
        this.publisherIdSelected = value;
      },
      async deletePublisher() {
        try {
          const res = await publisherService.delete(this.publisherIdSelected);
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
      updateListItemId(ids) {
        this.lstPublisherIdSelected = ids;
      },
      btnDeleteAllPublisher() {
        this.$emitter.emit(
          "toggleDialogNotice",
          true,
          true,
          "Xác nhận xóa",
          this.$Resource[this.$languageCode].ConfirmDeleteAll("Nhà xuất bản"),
          this.$Resource[this.$languageCode].Dialog.Type.Question
        );
      },
      async deleteManyPublisher() {
        try {
          const res = await publisherService.deleteMany({
            data: this.lstPublisherIdSelected,
          });
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
              this.btnRemoveRowSelected();
              break;
          }
        } catch (error) {
          console.log(error);
        }
      },
      toggleShowToolbarAction(isShow) {
        this.isShowToolbarAction = isShow;
      },
      updateTotalRecordSelected(total) {
        this.totalRecordSelected = total;
      },
      btnRemoveRowSelected() {
        this.selectAll = false;
        setTimeout(() => {
          this.selectAll = null;
        }, 500);
      },
    },
    data() {
      return {
        columnsTable: publisherColumns,
        pageData: [],
        isShowForm: false,
        publisherIdSelected: null,
        lstPublisherIdSelected: [],
        selectAll: false,
        /**chuỗi tìm kiếm nhanh các bản ghi */
        searchString: null,
        isShowToolbarAction: false,
        totalRecordSelected: null,
        pageSize: 10,
        pageNumber: 1,
        totalRecord: null,
        totalPage: null,
      };
    },
  };
  </script>
  <style scoped>
  @import url(./pulisher.css);
  </style>
  