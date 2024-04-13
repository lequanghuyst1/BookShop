<template>
  <div id="m-dialog__info-category" class="m-dialog">
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
        <form action="" style="width: 100%">
          <div class="row">
            <div class="col l-12">
              <MInput
                :ref="textFields.categoryCode.ref"
                :label="textFields.categoryCode.label"
                :rules="textFields.categoryCode.rules"
                v-model="category.CategoryCode"
              ></MInput>
            </div>
          </div>
          <div class="row">
            <div class="col l-12">
              <MInput
                :ref="textFields.categoryName.ref"
                :label="textFields.categoryName.label"
                :rules="textFields.categoryName.rules"
                v-model="category.CategoryName"
              ></MInput>
            </div>
          </div>
          <div class="row">
            <div class="col l-12">
              <MTextarea
                label="Mô tả"
                v-model="category.Description"
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
import TEXT_FIELD from "@/js/resource/text-field";
import categoryService from "@/utils/CategoryService";
export default {
  name: "CategoryDetail",
  props: {
    categoryIdSelected: {
      type: String,
    },
    formMode: {
      type: Number,
    },
  },
  emits: ["loadData", "onCloseForm"],
  created() {
    this.checkModeForm();
  },
  mounted() {
    this.$refs[this.textFields.categoryCode.ref].setFocus();
  },
  computed: {
    textFields() {
      return TEXT_FIELD[this.$languageCode].category;
    },
  },
  methods: {
    /**
     * Thực hiện kiểm tra giá trị formMode
     * @author LQHUY(13/04/2024)
     */
    checkModeForm() {
      if (this.formMode == this.$Enum.FormMode.Edit) {
        this.getCategoryDetail();
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
          this.editCategory();
        } else {
          this.addNewCategory();
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
              this.category[nameField] === "" ||
              this.category[nameField] === null ||
              this.category[nameField] === undefined
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
     * Hàm thực hiện gọi API thêm mới một category
     * @author LQHUY(13/04/2024)
     */
    async addNewCategory() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.category));
        const res = await categoryService.post(formData);
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
    async editCategory() {
      try {
        var formData = new FormData();
        console.log(this.category);
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.category));
        //gọi api thực hiện sửa
        const res = await categoryService.put(
          this.categoryIdSelected,
          formData
        );
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
     * Hàm thực hiện gọi API lấy ra thông tin chi tiết category theo id
     * @author LQHUY(13/04/2024)
     */
    async getCategoryDetail() {
      try {
        const res = await categoryService.getById(this.categoryIdSelected);
        switch (res.status) {
          case 200:
            this.category = res.data;
            this.category.QuantityImported = Number(null);
              this.$emitter.emit("toggleShowLoading", false);
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoading", false);
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện gọi API lấy ra mã code mới
     * @author LQHUY(13/04/2024)
     */
    async getNewCode() {
      try {
        const res = await categoryService.getNewCode();
        switch (res.status) {
          case 200:
            this.category.CategoryCode = res.data;
            setTimeout(() => {
              this.$emitter.emit("toggleShowLoading", false);
            }, 300);
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("toggleShowLoading", false);
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
  },
  data() {
    return {
      category: {},
      listErr: [],
    };
  },
};
</script>
<style scoped></style>
