<template>
  <MForm id="m-dialog__info-category" title="Thêm mới danh mục">
    <template #form>
      <form action="" style="width: 100%">
        <div class="row">
          <div class="col l-12">
            <MInput
              ref="categoryCode"
              v-model="category.categoryCode"
              label="Mã danh mục"
              :message="lstErrorMessage.categoryCode"
            ></MInput>
          </div>
        </div>
        <div class="row">
          <div class="col l-12">
            <MInput
              ref="categoryName"
              v-model="category.categoryName"
              label="Tên danh mục"
              :message="lstErrorMessage.categoryName"
            ></MInput>
          </div>
        </div>
        <div class="row">
          <div class="col l-12">
            <MTextarea label="Mô tả" v-model="category.description"></MTextarea>
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
import categoryService from "@/utils/CategoryService";
export default {
  name: "CategoryDetail",
  props: {
    categoryIdSelected: {
      type: String,
    },
  },
  emits: ["loadData"],
  created() {
    if (this.formMode == this.$Enum.FormMode.Edit) {
      this.getCategoryDetail();
    } else {
      this.getNewCode();
    }
  },
  mounted() {
    this.$refs["categoryCode"].setFocus();
  },
  computed: {
    /**
     * Trạng thái của form (thêm, sửa, nhân bản)
     * Author: LQHUY (26/11/2023)
     */
    formMode() {
      if (this.categoryIdSelected) {
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
          this.addNewCategory();
        } else if (this.formMode === this.$Enum.FormMode.Clone) {
          this.addNewEmployee();
        } else {
          this.editCategory();
        }
      } catch (error) {
        console.error(error);
      }
    },
    validateData() {
      try {
        this.setError("categoryCode", this.$refs.categoryCode.label);
        this.setError("categoryName", this.$refs.categoryName.label);
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
          this.category[field] === "" ||
          this.category[field] === null ||
          this.category[field] === undefined
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

    async addNewCategory() {
      try {
        const res = await categoryService.post(this.category);
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

    async editCategory() {
      try {
        const res = await categoryService.put(
          this.categoryIdSelected,
          this.category
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

    async getCategoryDetail() {
      try {
        const res = await categoryService.getById(this.categoryIdSelected);
        switch (res.status) {
          case 200:
            this.category = res.data;
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
        const res = await categoryService.getNewCode();
        switch (res.status) {
          case 200:
            this.category.categoryCode = res.data;
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
      category: {},
      lstErrorMessage: {},
      lstErorr: [],
    };
  },
};
</script>
<style scoped>
@import url(./category.css);
</style>
