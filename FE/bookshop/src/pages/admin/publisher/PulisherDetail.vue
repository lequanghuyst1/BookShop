<template>
  <div id="m-dialog__info-publisher" class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container">
      <div class="m-dialog__header">
        <h3 class="m-dialog__header-title">
          {{
            this.formMode === this.$Enum.FormMode.Add
              ? "Thêm mới NXB"
              : "Sửa thông tin NXB"
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
                v-model:imagePath="image.ImagePath"
                v-model="imageFile"
                label="Chọn ảnh"
              ></MInputImage>
            </div>
            <div class="col-10">
              <div class="row">
                <div class="col l-3">
                  <MInput
                    :ref="textFields.publisherCode.ref"
                    :label="textFields.publisherCode.label"
                    :rules="textFields.publisherCode.rules"
                    v-model="publisher.PublisherCode"
                  ></MInput>
                </div>
                <div class="col l-5">
                  <MInput
                    :ref="textFields.publisherName.ref"
                    :label="textFields.publisherName.label"
                    :rules="textFields.publisherName.rules"
                    v-model="publisher.PublisherName"
                  ></MInput>
                </div>
                <div class="col l-4">
                  <MInput
                    :ref="textFields.phoneNumber.ref"
                    :label="textFields.phoneNumber.label"
                    :rules="textFields.phoneNumber.rules"
                    v-model="publisher.PhoneNumber"
                  ></MInput>
                </div>
              </div>
              <div class="row">
                <div class="col l-3">
                  <MInput
                    :ref="textFields.address.ref"
                    :label="textFields.address.label"
                    :rules="textFields.address.rules"
                    v-model="publisher.Address"
                  ></MInput>
                </div>
              </div>
            </div>
          </div>

          <div class="row">
            <div class="col l-12">
              <MTextarea
                :ref="textFields.description.ref"
                :label="textFields.description.label"
                :rules="textFields.description.rules"
                v-model="publisher.Description"
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
import publisherService from "@/utils/PublisherService";
export default {
  name: "publisherDetail",
  props: {
    publisherIdSelected: {
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
    this.$refs[this.textFields.publisherCode.ref].setFocus();
  },
  computed: {
    textFields() {
      return TEXT_FIELD[this.$languageCode].publisher;
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
        this.getPublisherDetail();
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
          this.editPublisher();
        } else {
          this.addNewPublisher();
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
              this.publisher[nameField] === "" ||
              this.publisher[nameField] === null ||
              this.publisher[nameField] === undefined
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
     * Hàm thực gọi API hiện thêm mới một publisher
     * @author LQHUY(13/04/2024)
     */
    async addNewPublisher() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.publisher));
        const res = await publisherService.post(formData);
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
     * Hàm thực hiện gọi API sửa thông tin publisher theo id
     * @author LQHUY(13/04/2024)
     */
    async editPublisher() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.publisher));
        const res = await publisherService.put(
          this.publisherIdSelected,
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
     * Hàm thực hiện gọi API lấy ra thông tin chi tiết publisher theo id
     * @author LQHUY(13/04/2024)
     */
    async getPublisherDetail() {
      try {
        const res = await publisherService.getById(this.publisherIdSelected);
        switch (res.status) {
          case 200:
            this.publisher = res.data;
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
        const res = await publisherService.getNewCode();
        switch (res.status) {
          case 200:
            this.publisher.PublisherCode = res.data;
            this.$emitter.emit("toggleShowLoading", false);
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
     * Hàm thực hiện gọi API lấy hình ảnh thep publisherId
     * @author LQHUY(13/04/2024)
     */
    async getImageDetail() {
      try {
        var res = await this.$httpRequest.get(
          "Images/" + this.publisherIdSelected
        );
        switch (res.status) {
          case 200:
            this.image = res.data;
            console.log(this.image);
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
  },
  data() {
    return {
      publisher: {},
      imageFile: null,
      listErr: [],
      image: {},
    };
  },
};
</script>
<style scoped>
</style>
