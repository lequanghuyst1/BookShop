<template>
  <MForm id="m-dialog__info-book" title="Thêm mới sách">
    <template #form>
      <form action="" style="width: 100%; height: 100%;">
        <b-row></b-row>
        <div class="row">
          <div class="col-2">
            <MInputImage
              v-model:imagePath="book.image"
              v-model="imageFile"
              label="Chọn ảnh"
            ></MInputImage>
          </div>
          <div class="col l-10">
            <div class="row">
              <div class="col l-3">
                <MInput
                  ref="bookCode"
                  v-model="book.bookCode"
                  label="Mã sách"
                  :message="lstErrorMessage.bookCode"
                ></MInput>
              </div>
              <div class="col l-5">
                <MInput
                  ref="bookName"
                  v-model="book.bookName"
                  label="Tên sách"
                  :message="lstErrorMessage.bookName"
                ></MInput>
              </div>
              <div class="col l-4">
                <MInput
                  ref="author"
                  v-model="book.author"
                  label="Tác giả"
                ></MInput>
              </div>
            </div>
            <div class="row">
              <div class="col l-6">
                <MCombobox
                  label="Danh mục"
                  url="Categories"
                  v-model="book.categoryId"
                  propValue="categoryId"
                  propText="categoryName"
                  ref="category"
                  id="cbCategory"
                ></MCombobox>
              </div>
              <div class="col l-6">
                <MCombobox
                  label="Nhà xuất bản"
                  url="Publishers"
                  v-model="book.publisherId"
                  propValue="publisherId"
                  propText="publisherName"
                  ref="publisher"
                  id="cbPublisher"
                ></MCombobox>
              </div>
            </div>
            <div class="row">
              <div class="col l-3">
                <MInput
                  ref="publicationDate"
                  type="date"
                  v-model="book.publicationDate"
                  label="Ngày xuất bản"
                ></MInput>
              </div>
              <div class="col l-3">
                <MInput ref="price" v-model="book.price" label="Giá"></MInput>
              </div>
              <div class="col l-3">
                <MInput
                  ref="size"
                  v-model="book.size"
                  label="Khổ sách"
                ></MInput>
              </div>
              <div class="col l-3">
                <MInput
                  ref="size"
                  v-model="book.heavy"
                  label="Khối lượng"
                ></MInput>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col l-3">
            <MInput label="Số lượng nhập" v-model="book.quantityImported"></MInput>
          </div>
        </div>
        <div class="row">
          <div class="col l-12">
            <MTextarea label="Mô tả" v-model="book.description"></MTextarea>
          </div>
        </div>
      </form>
    </template>
    <template #footer>
      <MButton class="m-button--sub" text="Hủy"></MButton>
      <div
        v-if="
          formMode === this.$Enum.FormMode.Add ||
          formMode === this.$Enum.FormMode.Clone
        "
        class="m-dialog__group-button"
      >
        <MButton
          v-tippy="{
            content: 'Lưu',
            placement: 'top',
          }"
          @click="onSaveData"
          text="Lưu"
          class="m-button--sub"
        ></MButton>
        <MButton
          v-tippy="{
            content: 'Lưu và thêm mới',
            placement: 'top',
          }"
          @click="onSaveData"
          text="Lưu và thêm mới"
        ></MButton>
      </div>
      <div
        v-else-if="formMode === this.$Enum.FormMode.Edit"
        class="m-dialog__group-button"
      >
        <MButton
          v-tippy="{
            content: 'Sửa thông tin',
            placement: 'top',
          }"
          @click="onSaveData"
          text="Sửa thông tin"
        ></MButton>
      </div>
    </template>
  </MForm>
</template>

<script>
import bookService from "@/utils/BookService";
export default {
  name: "BookDetail",
  props: {
    bookIdSelected: {
      type: String,
    },
  },
  emits: ["loadData"],
  created() {
    if (this.formMode == this.$Enum.FormMode.Edit) {
      this.getBookDetail();
    } else {
      this.getNewCode();
    }
  },
  mounted() {
    this.$refs["bookCode"].setFocus();
  },
  computed: {
    /**
     * Trạng thái của form (thêm, sửa, nhân bản)
     * Author: LQHUY (26/11/2023)
     */
    formMode() {
      if (this.bookIdSelected) {
        return this.$Enum.FormMode.Edit;
      } else {
        return this.$Enum.FormMode.Add;
      }
    },
  },
  methods: {
    onSaveData() {
      try {
        this.lstErorr = [];
        this.validateData();
        if (this.lstErorr.length > 0) {
          this.setFocusInputFirstError();
          return;
        }
        if (this.formMode === this.$Enum.FormMode.Add) {
          this.addNewBook();
        } else if (this.formMode === this.$Enum.FormMode.Clone) {
          this.addNewEmployee();
        } else {
          this.editBook();
        }
      } catch (error) {
        console.error(error);
      }
    },
    validateData() {
      try {
        this.setError("bookCode", this.$refs.bookCode.label);
        this.setError("bookName", this.$refs.bookName.label);
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Hàm xét message lỗi
     * @param {string} field
     * @param {string} title
     * Author: LQHUY (26/11/2023)
     */
    setError(field, title) {
      try {
        if (
          this.book[field] === "" ||
          this.book[field] === null ||
          this.book[field] === undefined
        ) {
          this.lstErrorMessage[`${field}`] =
            this.$Resource[this.$languageCode].ErrorMessage(title);
          this.lstErorr.push(field);
        } else {
          this.lstErorr.filter((item) => item !== field);
        }
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Set vào ô input lỗi đầu tiên
     * Author: LQHUY(07/12/2002)
     */
    setFocusInputFirstError() {
      this.$refs[this.lstErorr[0]].setFocus();
    },

    async addNewBook() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.book));
        const res = await bookService.postHaveImage(formData);
        switch (res.status) {
          case 201:
            this.successResponse("Thêm mới thành công");
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        console.log(error);
      }
    },

    async editBook() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.book));
        const res = await bookService.putHaveImage(formData);
        switch (res.status) {
          case 200:
            this.successResponse("Sửa thành công");
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        console.log(error);
      }
    },

    async getBookDetail() {
      try {
        const res = await bookService.getById(this.bookIdSelected);
        switch (res.status) {
          case 200:
            this.book = res.data;
            break;
          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getNewCode() {
      try {
        const res = await bookService.getNewCode();
        switch (res.status) {
          case 200:
            this.book.bookCode = res.data;
            break;
          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lí khi thêm hoặc sửa thông tin thành công
     * @param {string} message
     * Author: LQHUY
     */
    successResponse(message) {
      try {
        this.$emitter.emit("toggleShowForm");
        this.$emitter.emit(
          "onShowToastMessage",
          this.$Resource[this.$languageCode].ToastMessage.Type.Success,
          message,
          this.$Resource[this.$languageCode].ToastMessage.Status.Success
        );
        this.$emit("loadData");
      } catch (error) {
        console.error(error);
      }
    },
  },
  data() {
    return {
      book: {},
      lstErrorMessage: {},
      lstErorr: [],
      imageFile: null,
    };
  },
};
</script>
<style scoped>

</style>
