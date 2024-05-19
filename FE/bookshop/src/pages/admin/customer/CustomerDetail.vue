<template>
  <div id="m-dialog__info-user" class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container w-50">
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
            <div class="col l-6">
              <MInput
                :ref="textFields.fullname.ref"
                :label="textFields.fullname.label"
                :rules="textFields.fullname.rules"
                v-model="user.Fullname"
              ></MInput>
            </div>
            <div class="col l-6">
              <MInput
                :ref="textFields.email.ref"
                :label="textFields.email.label"
                :rules="textFields.email.rules"
                v-model="user.Email"
              ></MInput>
            </div>
          </div>
          <div class="row"></div>
          <div class="row">
            <div class="col l-6">
              <MInput
                :ref="textFields.phoneNumber.ref"
                :label="textFields.phoneNumber.label"
                :rules="textFields.phoneNumber.rules"
                v-model="user.PhoneNumber"
              ></MInput>
            </div>
            <div class="col l-6">
              <MGenderRadio v-model="user.Gender"></MGenderRadio>
            </div>
          </div>
          <div class="row">
            <div class="col l-12">
              <!-- <MInput
                :ref="textFields.address.ref"
                :label="textFields.address.label"
                :rules="textFields.address.rules"
                v-model="user.Address"
              ></MInput> -->
              <div class="col-6">
                <MCombobox
                  propValue="value"
                  propText="name"
                  :dataCombobox="comboboxAccountData"
                  :ref="textFields.accountStatus.ref"
                  :label="textFields.accountStatus.label"
                  :rules="textFields.accountStatus.rules"
                  v-model="user.AccountStatus"
                  id="cbAccountStatus"
                ></MCombobox>
              </div>
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
import userService from "@/utils/UserService";
export default {
  props: {
    userIdSelected: {
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
    this.$refs[this.textFields.fullname.ref].setFocus();
    this.$emitter.emit("toggleShowLoading", false);
  },
  computed: {
    textFields() {
      return TEXT_FIELD[this.$languageCode].user;
    },
  },
  methods: {
    /**
     * Thực hiện kiểm tra giá trị formMode
     * @author LQHUY(13/04/2024)
     */
    checkModeForm() {
      if (this.formMode == this.$Enum.FormMode.Edit) {
        this.getUserDetail();
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
          this.editUser();
        } else {
          this.addNewUser();
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
              this.user[nameField] === "" ||
              this.user[nameField] === null ||
              this.user[nameField] === undefined
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
     * Hàm thực hiện gọi API thêm mới một user
     * @author LQHUY(13/04/2024)
     */
    async addNewUser() {
      try {
        var formData = new FormData();
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.user));
        const res = await userService.post(formData);
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
     * Hàm thực hiện gọi API sửa thông tin user theo id
     * @author LQHUY(13/04/2024)
     */
    async editUser() {
      try {
        var formData = new FormData();
        console.log(this.user);
        formData.append("imageFile", this.imageFile);
        formData.append("dataJson", JSON.stringify(this.user));
        //gọi api thực hiện sửa
        const res = await userService.put(this.userIdSelected, formData);
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
     * Hàm thực hiện gọi API lấy ra thông tin chi tiết user theo id
     * @author LQHUY(13/04/2024)
     */
    async getUserDetail() {
      try {
        const res = await userService.getById(this.userIdSelected);
        switch (res.status) {
          case 200:
            this.user = res.data;
            this.user.QuantityImported = Number(null);
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
      user: {},
      listErr: [],
      comboboxAccountData: [
        { value: 0, name: "Dừng hoạt động" },
        { value: 1, name: "Đang hoạt động" },
      ],
    };
  },
};
</script>
<style scoped></style>
