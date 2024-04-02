<template>
  <MForm id="m-dialog__info-publisher" title="Thêm mới sách">
    <template #form>
      <form action="" style="width: 100%; height: 100%">
        <div class="row">
          <div class="col-2">
            <MInputImage
              v-model:imagePath="publisher.image"
              v-model="imageFile"
              label="Chọn ảnh"
            ></MInputImage>
          </div>
          <div class="col-10">
            <div class="row">
              <div class="col l-3">
                <MInput
                  ref="publisherCode"
                  v-model="publisher.publisherCode"
                  label="Mã NXB"
                  :message="lstErrorMessage.publisherCode"
                ></MInput>
              </div>
              <div class="col l-5">
                <MInput
                  ref="publisherName"
                  v-model="publisher.publisherName"
                  label="Tên NXB"
                  :message="lstErrorMessage.publisherName"
                ></MInput>
              </div>
              <div class="col l-4">
                <MInput
                  ref="phoneNumber"
                  v-model="publisher.phoneNumber"
                  label="Số điện thoại"
                ></MInput>
              </div>
            </div>
            <div class="row">
              <div class="col l-3">
                <MInput
                  ref="address"
                  v-model="publisher.price"
                  label="Địa chỉ"
                ></MInput>
              </div>
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col l-12">
            <MTextarea
              label="Mô tả"
              v-model="publisher.description"
            ></MTextarea>
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
import publisherService from "@/utils/PublisherService";
export default {
  name: "PulisherDetail",
  props: {
    publisherIdSelected: {
      type: String,
    },
  },
  emits: ["loadData"],
  created() {
    if (this.formMode == this.$Enum.FormMode.Edit) {
      this.getPublisherDetail();
    } else {
      this.getNewCode();
    }
  },
  mounted() {
    this.$refs["publisherCode"].setFocus();
  },
  computed: {
    /**
     * Trạng thái của form (thêm, sửa, nhân bản)
     * Author: LQHUY (26/11/2023)
     */
    formMode() {
      if (this.publisherIdSelected) {
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
          this.addNewPublisher();
        } else if (this.formMode === this.$Enum.FormMode.Clone) {
          this.addNewEmployee();
        } else {
          this.editPublisher();
        }
      } catch (error) {
        console.error(error);
      }
    },
    validateData() {
      try {
        this.setError("publisherCode", this.$refs.publisherCode.label);
        this.setError("publisherName", this.$refs.publisherName.label);
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
          this.publisher[field] === "" ||
          this.publisher[field] === null ||
          this.publisher[field] === undefined
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

    async addNewPublisher() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.publisher));
        const res = await publisherService.postHaveImage(formData);
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

    async editPublisher() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.publisher));
        const res = await publisherService.putHaveImage(formData);
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

    async getPublisherDetail() {
      try {
        const res = await publisherService.getById(this.publisherIdSelected);
        switch (res.status) {
          case 200:
            this.publisher = res.data;
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
        const res = await publisherService.getNewCode();
        switch (res.status) {
          case 200:
            this.publisher.publisherCode = res.data;
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
      publisher: {},
      lstErrorMessage: {},
      lstErorr: [],
      imageFile: null,
    };
  },
};
</script>
<style scoped></style>
