<template>
  <div id="m-dialog__info-book" class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container">
      <div class="m-dialog__header">
        <h3 class="m-dialog__header-title">
          {{
            this.formMode === this.$Enum.FormMode.Add
              ? "Thêm mới sách"
              : "Sửa thông tin sách"
          }}
        </h3>
        <div class="m-dialog__header-action">
          <div
            v-tippy="{
              content: 'Giúp',
              placement: 'bottom',
            }"
            class="m-dialog__header-help"
          >
            <i class="fa-regular fa-circle-question"></i>
          </div>
          <div
            v-tippy="{
              content: 'Đóng',
              placement: 'bottom',
            }"
            class="m-dialog__header-close"
            @click="this.$emit('onCloseForm')"
          >
            <i class="fa-solid fa-xmark"></i>
          </div>
        </div>
      </div>
      <div class="m-dialog__content">
        <form action="" style="width: 100%; height: 100%">
          <div class="row">
            <div class="col-2">
              <MInputImage
                id="image"
                v-model:imagePath="image.ImagePath"
                v-model="imageFile"
                label="Chọn ảnh"
              ></MInputImage>
            </div>
            <div class="col l-10">
              <div class="row">
                <div class="col-2">
                  <MInput
                    :ref="textFields.bookCode.ref"
                    :label="textFields.bookCode.label"
                    :rules="textFields.bookCode.rules"
                    v-model="book.BookCode"
                  ></MInput>
                </div>
                <div class="col-6">
                  <MInput
                    :ref="textFields.bookName.ref"
                    :label="textFields.bookName.label"
                    :rules="textFields.bookName.rules"
                    v-model="book.BookName"
                  ></MInput>
                </div>
                <div class="col-4">
                  <MInput
                    :ref="textFields.author.ref"
                    :label="textFields.author.label"
                    :rules="textFields.author.rules"
                    v-model="book.Author"
                  ></MInput>
                </div>
              </div>
              <div class="row">
                <div class="col l-6">
                  <MCombobox
                    propValue="CategoryId"
                    propText="CategoryName"
                    :dataCombobox="comboboxCategoryData"
                    :ref="textFields.categoryId.ref"
                    :label="textFields.categoryId.label"
                    :rules="textFields.categoryId.rules"
                    v-model="book.CategoryId"
                    id="cbCategory"
                  ></MCombobox>
                </div>
                <div class="col l-6">
                  <MCombobox
                    propValue="PublisherId"
                    propText="PublisherName"
                    :dataCombobox="comboboxPublisherData"
                    :ref="textFields.publisherId.ref"
                    :label="textFields.publisherId.label"
                    :rules="textFields.publisherId.rules"
                    v-model="book.PublisherId"
                    id="cbPublisher"
                  ></MCombobox>
                </div>
              </div>
              <div class="row">
                <div class="col l-3">
                  <MInput
                    type="date"
                    ref="PublicationDate"
                    label="Ngày xuất bản"
                    v-model="book.PublicationDate"
                  ></MInput>
                </div>
                <div class="col l-3">
                  <MInput
                    :ref="textFields.quantityImported.ref"
                    :label="textFields.quantityImported.label"
                    :rules="textFields.quantityImported.rules"
                    v-model="book.QuantityImported"
                  ></MInput>
                </div>
                <div class="col l-3">
                  <MInput
                    ref="Size"
                    label="Khổ sách"
                    v-model="book.Size"
                  ></MInput>
                </div>
                <div class="col l-3">
                  <MInput
                    ref="Heavy"
                    label="Khối lượng"
                    v-model="book.Heavy"
                  ></MInput>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col l-3">
              <MInput
                :ref="textFields.originalPrice.ref"
                :label="textFields.originalPrice.label"
                :rules="textFields.originalPrice.rules"
                v-model="book.OriginalPrice"
              ></MInput>
            </div>
            <div class="col l-3">
              <MInput
                :ref="textFields.discount.ref"
                :label="textFields.discount.label"
                :rules="textFields.discount.rules"
                v-model="book.Discount"
              ></MInput>
            </div>
            <div class="col l-3">
              <MInput
                :ref="textFields.numberOfPage.ref"
                :label="textFields.numberOfPage.label"
                :rules="textFields.numberOfPage.rules"
                v-model="book.NumberOfPage"
              ></MInput>
            </div>
          </div>
          <div class="row">
            <div class="col l-12">
              <MTextarea
                id="description"
                label="Mô tả"
                v-model="book.Description"
              ></MTextarea>
            </div>
          </div>
        </form>
      </div>
      <div class="m-dialog__footer">
        <div class="m-dialog__footer-left">
          <MButton
            v-tippy="{
              content: 'Hủy',
              placement: 'top',
            }"
            class="m-button--sub"
            @click="this.$emit('onCloseForm')"
            text="Hủy"
          ></MButton>
        </div>
        <div class="m-dialog__footer-right">
          <div class="m-dialog__group-button">
            <MButton
              v-tippy="{
                content: 'Thêm mới',
                placement: 'top',
              }"
              @click="handleSaveDataWithMode"
              :text="
                this.formMode === this.$Enum.FormMode.Add
                  ? 'Thêm mới'
                  : 'Sửa thông tin'
              "
              class="m-button"
            ></MButton>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import bookService from "@/utils/BookService";
import TEXT_FIELD from "@/js/resource/text-field";
import categoryService from "@/utils/CategoryService";
import publisherService from "@/utils/PublisherService";
export default {
  name: "BookDetail",
  props: {
    bookIdSelected: {
      type: String,
    },
    formMode: {
      type: Number,
    },
  },
  emits: ["loadData", "onCloseForm"],
  created() {
    this.checkModeForm();
    this.getCategoriesData();
    this.getPublishersData();
  },
  mounted() {
    this.$refs[this.textFields.bookCode.ref].setFocus();
  },
  computed: {
    textFields() {
      return TEXT_FIELD[this.$languageCode].book;
    },
  },
  methods: {
    /**
     * Thực hiện kiểm tra giá trị formMode
     * @author LQHUY(13/04/2024)
     */
    checkModeForm() {
      if (this.formMode == this.$Enum.FormMode.Edit) {
        this.getImageDetail();
        this.getBookDetail();
      } else {
        this.getNewCode();
      }
    },

    /**
     * Hàm thực hiện save dữ liệu theo formMode khi click btn Thêm mới hoặc sửa
     * @author LQHUY(13/04/2024)
     */
    handleSaveDataWithMode() {
      this.handleValidateField();
      try {
        if (this.listErr.length > 0) {
          this.$refs[this.listErr[0]].setFocus();
          return;
        }
        if (this.formMode === this.$Enum.FormMode.Edit) {
          this.editBook();
        } else {
          this.addNewBook();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Hàm thực hiện validate dữ liệu
     * @author LQHUY(13/04/2024)
     */
    handleValidateField() {
      try {
        for (let key in this.textFields) {
          let ref = this.textFields[key].ref;
          this.$refs[ref].validate();
          let rules = this.textFields[key].rules;
          let nameField = this.textFields[key].name;
          if (rules.required === true) {
            if (
              this.book[nameField] === "" ||
              this.book[nameField] === null ||
              this.book[nameField] === undefined
            ) {
              this.listErr.push(ref);
            } else {
              this.listErr = this.listErr.filter((item) => item !== ref);
            }
          }
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Hàm thực hiện gọi API thêm mới một book
     * @author LQHUY(13/04/2024)
     */
    async addNewBook() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.book));
        const res = await bookService.post(formData);
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

    /**
     * Hàm thực hiện gọi API sửa thông tin category theo id
     * @author LQHUY(13/04/2024)
     */
    async editBook() {
      try {
        this.book.QuantityInStock += this.book.QuantityImported;
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.book));
        const res = await bookService.put(this.bookIdSelected, formData);
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

    /**
     * Hàm thực hiện gọi API lấy ra thông tin chi tiết book theo id
     * @author LQHUY(13/04/2024)
     */
    async getBookDetail() {
      try {
        const res = await bookService.getById(this.bookIdSelected);
        switch (res.status) {
          case 200:
            this.book = res.data;
            this.book.QuantityImported = Number(0);
            this.$emitter.emit("toggleShowLoading", false);
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện gọi API lấy ra mã code mới
     * @author LQHUY(13/04/2024)
     */
    async getNewCode() {
      try {
        const res = await bookService.getNewCode();
        switch (res.status) {
          case 200:
            this.book.BookCode = res.data;
            this.$emitter.emit("toggleShowLoading", false);

            break;
          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện gọi API lấy hình ảnh thep bookId
     * @author LQHUY(13/04/2024)
     */
    async getImageDetail() {
      try {
        var res = await this.$httpRequest.get("Images/" + this.bookIdSelected);
        switch (res.status) {
          case 200:
            this.image = res.data;
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
        this.$emit("onCloseForm");
        this.$emit("loadData");
      } catch (error) {
        console.error(error);
      }
    },

    async getCategoriesData() {
      try {
        const res = await categoryService.getAll();
        if (res.status == 200) {
          this.comboboxCategoryData = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getPublishersData() {
      try {
        const res = await publisherService.getAll();
        if (res.status === 200) {
          this.comboboxPublisherData = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      book: {},
      imageFile: null,
      listErr: [],
      image: {},
      comboboxCategoryData: [],
      comboboxPublisherData: [],
    };
  },
};
</script>
<style scoped></style>
