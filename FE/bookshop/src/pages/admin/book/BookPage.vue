<template>
  <div class="content__header">
    <div class="content__header-title">
      <h3>Quản lý sách</h3>
    </div>
    <div class="content__header-group-button">
      <tippy content="Thêm mới" placement="bottom" animation="scale">
        <MButton
          class="m-button-icon"
          icon="fa-solid fa-plus"
          id="btn-add-employee"
          text="Thêm mới"
          style="font-weight: 600"
          @click="onCreateItem"
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
            @click="onDeleteAll"
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
        <MButtonNoText
          v-tippy="{
            content: 'Load dữ liệu',
            placement: 'bottom',
          }"
          @click="this.loadData"
          icon="fa-solid fa-solid fa-arrow-rotate-right"
        >
        </MButtonNoText>
      </div>
    </div>

    <!-- Table -->

    <MTable
      id="book-data"
      idObject="BookId"
      codeObject="BookCode"
      :columnsTable="columnsTable"
      :pageData="pageData"
      :selectAll="selectAll"
      :image="true"
      :imageData="imageData"
      @onDelete="onDeleteItem"
      @onUpdate="onUpdateItem"
      @updateListItemId="updateListItemId"
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
    <BookDetail
      v-if="isShowForm"
      @loadData="loadDataTable"
      @onCloseForm="onCloseFormDetail"
      :formMode="formMode"
      :bookIdSelected="bookIdSelected"
    ></BookDetail>

    <MDialog
      v-if="isShowDialogDelete"
      title="Xác nhận xóa"
      @onCloseDialog="onHideDialogDelete"
      :message="messageDialog"
      :type="this.$Resource[this.$languageCode].Dialog.Type.Question"
    >
      <template #footerLeft>
        <MButton
          @click="onHideDialogDelete"
          class="m-button--sub"
          text="Hủy"
        ></MButton>
      </template>
      <template #footerRight>
        <MButton @click="handleDeleteConfirm" text="Xác nhận"></MButton>
      </template>
    </MDialog>
  </div>
</template>
<script>
import BookDetail from "./BookDetail.vue";
import bookColumns from "@/js/data/book";
import bookService from "../../../utils/BookService";
export default {
  name: "BookPage",
  components: { BookDetail },
  created() {
    this.$emitter.on("updatePageSize", this.updatePageSize);
  },
  mounted() {
    this.loadData();
    this.loadDataImage();
    document.title = "Quản lý sách";
  },
  beforeUnmount() {
    this.$emitter.off("updatePageSize", this.updatePageSize);
  },
  watch: {
    //theo dõi biến pageSize
    pageSize: function () {
      this.loadData();
    },
    //Theo dõi biến pageNumber
    pageNumber: function () {
      this.loadData();
    },
    //Theo dõi biến searchString
    searchString: function () {
      this.pageNumber = 1;
      this.loadData();
    },
  },
  methods: {
    /**
     * Thực hiện ẩn dialog xác nhận xóa
     * @author LQHUY(19/03/2024)
     */
    onHideDialogDelete() {
      this.isShowDialogDelete = false;
    },

    /**
     * Thực hiện load dữ liệu cho table
     * @author LQHUY(19/03/2024)
     */
    loadDataTable() {
      this.loadData();
      this.loadDataImage();
    },

    /**
     * Thực hiện gọi API lấy dữ liệu cho page
     * @author LQHUY(19/03/2024)
     */
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
        console.log(this.pageNumber);
        const res = await bookService.getFilterPaging({ params });
        switch (res.status) {
          case 200:
            this.pageData = res.data.Data;
            this.totalRecord = res.data.TotalRecord;
            this.totalPage = res.data.TotalPage;
            this.$emitter.emit("toggleShowLoadingTable", false, 200);
            break;
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoadingTable", false);
      }
    },

    /**
     * Thực hiện gọi API lấy dữ liệu hình ảnh
     * @author LQHUY(19/03/2024)
     */
    async loadDataImage() {
      try {
        const res = await this.$httpRequest.get("Images");
        switch (res.status) {
          case 200:
            this.imageData = res.data;
            break;
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("handleApiError", error);
      }
    },

    /**
     * Thực hiện hiển thị form khi click btn thêm mới
     * @author LQHUY(19/03/2024)
     */
    onCreateItem() {
      this.bookIdSelected = null;
      this.isShowForm = true;
      this.formMode = this.$Enum.FormMode.Add;
    },

    /**
     * Thực hiện hiển thị form khi click icon sửa
     * @param {string} id
     * @author LQHUY(19/03/2024)
     */
    onUpdateItem(id) {
      this.bookIdSelected = id;
      this.isShowForm = true;
      this.formMode = this.$Enum.FormMode.Edit;
    },

    /**
     * Thực hiện hiển thị form
     * @author LQHUY(19/03/2024)
     */
    onShowFormDetail() {
      this.isShowForm = true;
    },

    /**
     * Thực hiện ẩn form
     * @author LQHUY(19/03/2024)
     */
    onCloseFormDetail() {
      this.isShowForm = false;
    },

    /**
     * Thực hiện update danh sách các bản ghi được chọn
     * @author LQHUY(19/03/2024)
     */
    updateListItemId(ids) {
      this.lstBookIdSelected = ids;
    },

    /**
     * Hiển thị dialog xóa khi click btn xóa tất cả
     * @author LQHUY(19/03/2024)
     */
    onDeleteItem(message, id) {
      this.typeDelete = "single";
      this.isShowDialogDelete = true;
      this.messageDialog = message;
      this.bookIdSelected = id;
    },

    /**
     * Thực hiện hiển thị dialog xóa khi click btn xóa tất cả
     * @author LQHUY(19/03/2024)
     */
    onDeleteAll() {
      this.typeDelete = "all";
      this.isShowDialogDelete = true;
      this.messageDialog =
        this.$Resource[this.$languageCode].ConfirmDeleteAll("Cuốn sách");
    },

    /**
     * Thực hiện gọi API xóa 1 bản ghi
     * @author LQHUY(19/03/2024)
     */
    async handleDeleteConfirm() {
      if (this.typeDelete === "single") {
        await this.handleDeteleItem();
      } else if (this.typeDelete === "all") {
        await this.handleDeleteMany();
      }
    },

    /**
     * Thực hiện gọi API xóa 1 bản ghi
     * @author LQHUY(19/03/2024)
     */
    async handleDeteleItem() {
      try {
        this.onHideDialogDelete();
        const res = await bookService.delete(this.bookIdSelected);
        switch (res.status) {
          case 200:
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
        this.$emitter.emit("handleApiError");
        console.log(error);
      }
    },

    /**
     * Thực hiện gọi API xóa nhiều bản ghi
     * @author LQHUY(19/03/2024)
     */
    async handleDeleteMany() {
      try {
        this.onHideDialogDelete();
        const res = await bookService.deleteMany({
          data: this.lstBookIdSelected,
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
            this.loadDataTable();
            this.btnRemoveRowSelected();
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError");
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện ẩn hiện toolbar
     * @param {boolean} isShow
     * @author LQHUY(19/03/2024)
     */
    toggleShowToolbarAction(isShow) {
      this.isShowToolbarAction = isShow;
    },

    /**
     * Hàm thực hiện update tổng số bản ghi được chọn
     * @param {number} total
     * @author LQHUY(19/03/2024)
     */
    updateTotalRecordSelected(total) {
      this.totalRecordSelected = total;
    },

    /**
     * Hàm thực hiện gỡ bỏ tất cả các dòng được chọn khi click btn bỏ chọn
     * @author LQHUY(19/03/2024)
     */
    btnRemoveRowSelected() {
      this.selectAll = false;
      setTimeout(() => {
        this.selectAll = null;
      }, 500);
    },

    /**
     * Hàm thực hiện cập nhật giá trị pageSize
     * @param {number} value
     * @author LQHUY(19/03/2024)
     */
    updatePageSize(value) {
      this.pageSize = value;
    },
  },
  provide() {
    return {
      pageSizeDefault: 10,
    };
  },
  data() {
    return {
      /** Ẩn hiển thị dialog xóa*/
      isShowDialogDelete: false,
      /** Ẩn hiển thị form*/
      isShowForm: false,
      /** Ẩn hiển thị boolbar*/
      isShowToolbarAction: false,

      //columns bảng danh mục
      columnsTable: bookColumns,
      /**chuỗi tìm kiếm nhanh các bản ghi */
      searchString: null,
      //Số bản ghi được hiển thị trong 1 trang
      pageSize: 10,
      //Số trang hiển thị
      pageNumber: 1,

      /**Trạng thái form */
      formMode: this.$Enum.FormMode.Add,
      //Trạng thái xóa bản ghi
      typeDelete: "single",

      //Lưu danh sách các bản ghi của page
      pageData: [],
      /**Lưu id bản ghi được chọn */
      bookIdSelected: null,
      /**Lưu danh sách id các bản ghi được chọn */
      lstBookIdSelected: [],
      //Lưu giá trị chọn tất cả
      selectAll: false,
      //Lưu tổng số bản ghi được chọn
      totalRecordSelected: null,
      //Lưu tổng số bản ghi của trang
      totalRecord: null,
      //Lưu tổng số trang
      totalPage: null,
      //Lưu danh sách các hình ảnh
      imageData: [],
    };
  },
};
</script>
<style scoped>
@import url(./book.css);
</style>
