<template>
  <div class="my-account">
    <div class="page-title">
      <h1>Thông tin tài khoản</h1>
    </div>
    <form v-on:submit.prevent action="" id="form-account-info">
      <InputAccount
        id="fullname"
        :label="textFields.fullname.label"
        :placeholder="textFields.fullname.placeholder"
        :ref="textFields.fullname.ref"
        :rules="textFields.fullname.rules"
        v-model="user.Fullname"
      ></InputAccount>
      <InputAccount
        id="phonenumber"
        :label="textFields.phoneNumber.label"
        :placeholder="textFields.phoneNumber.placeholder"
        :ref="textFields.phoneNumber.ref"
        :rules="textFields.phoneNumber.rules"
        v-model="user.PhoneNumber"
      ></InputAccount>
      <InputAccount
        id="email"
        :label="textFields.email.label"
        :placeholder="textFields.email.placeholder"
        :ref="textFields.email.ref"
        :rules="textFields.email.rules"
        v-model="user.Email"
      ></InputAccount>
      <InputAccount
        id="address"
        :label="textFields.address.label"
        :placeholder="textFields.address.placeholder"
        :ref="textFields.address.ref"
        :rules="textFields.address.rules"
        v-model="user.address"
      ></InputAccount>

      <div class="row group-input-account group-input-dateofbirth">
        <div class="input-label">
          <label for="birthday" class="">Ngày sinh</label>
        </div>
        <div class="col-8">
          <div class="row">
            <div class="col-3">
              <input
                type="text"
                id="date"
                placeholder="DD"
                class="m-textfield"
                v-model="date"
              />
            </div>
            <div class="col-3">
              <input
                type="text"
                id="month"
                placeholder="MM"
                class="m-textfield"
                v-model="month"
              />
            </div>
            <div class="col-3">
              <input
                type="text"
                id="year"
                placeholder="YYYY"
                class="m-textfield"
                v-model="year"
              />
            </div>
          </div>
        </div>
      </div>

      <div class="row group-input-account mt-1">
        <div class="input-label">
          <label for="" class=""></label>
        </div>
        <div
          @click="onShowBlockChangePassword"
          class="col-8 d-flex align-items-start"
        >
          <input type="checkbox" v-model="isChangePassword" />
          <label for="" class="ms-2 change-password">Đổi mật khẩu</label>
        </div>
      </div>

      <div v-if="isChangePassword" class="change-password-layout">
        <InputAccount
          id="password-old"
          :label="textFields.currentPassword.label"
          :placeholder="textFields.currentPassword.placeholder"
          :ref="textFields.currentPassword.ref"
          :rules="textFields.currentPassword.rules"
          :errorMessage="this.lstErrorMessage.CurrentPassword"
          v-model="user.CurrentPassword"
        ></InputAccount>
        <InputAccount
          id="password-new"
          :label="textFields.newPassword.label"
          :placeholder="textFields.newPassword.placeholder"
          :ref="textFields.newPassword.ref"
          :rules="textFields.newPassword.rules"
          v-model="user.NewPassword"
        ></InputAccount>
        <InputAccount
          id="password-renew"
          :label="textFields.renewPassword.label"
          :placeholder="textFields.renewPassword.placeholder"
          :ref="textFields.renewPassword.ref"
          :rules="textFields.renewPassword.rules"
          v-model="user.RenewPassword"
        ></InputAccount>
      </div>
      <div @click="handleUpdateUserInfo" class="btn-save-confirm">
        <button>Cập nhật thông tin</button>
      </div>
    </form>
  </div>
</template>
<script>
import InputAccount from "./GroupInput.vue";
import localStorageService from "@/js/storage/LocalStorageService";
import userService from "@/utils/UserService";
import TEXT_FIELD from "@/js/resource/text-field";
export default {
  name: "InfoAccountUserPage",
  components: { InputAccount },
  created() {
  },
  mounted() {
    this.$refs[this.textFields.fullname.ref].focusInput();
    this.user = localStorageService.getItemEncodeFromLocalStorage("userInfo")
      ? localStorageService.getItemEncodeFromLocalStorage("userInfo")
      : {};
  },
  watch: {
    date(newValue) {
      if (newValue > 31) {
        this.date = 31;
      }
      if (newValue < 1) {
        this.date = 1;
      }
    },
    month(newValue) {
      if (newValue > 12) {
        this.month = 12;
      }
      if (newValue < 1) {
        this.month = 1;
      }
    },
    year(newValue) {
      if (newValue > this.yearNow) {
        this.year = this.yearNow;
      }
      if (newValue < 1) {
        this.year = 1;
      }
    },
  },
  computed: {
    yearNow() {
      let date = new Date();
      return date.getFullYear();
    },
    textFields: function () {
      return TEXT_FIELD[this.$languageCode].user;
    },
  },
  methods: {
    onShowBlockChangePassword() {
      // console.log(this.$refs[this.textFields.currentPassword.ref])
      // this.$refs[this.textFields.currentPassword.ref].focusInput();
    },

    /**
     * Thực hiện save khi click vào btn lưu thông tin
     * Author: LQHUY(08/04/2024)
     */
    handleUpdateUserInfo() {
      this.lstErrorMessage = {};

      //validate dữ liệu
      this.handleValidateField();

      if (Object.keys(this.lstErrorMessage).length > 0) {
        for (const key in this.lstErrorMessage) {
          const value = this.lstErrorMessage[key];
          if (value !== null) {
            this.$refs[key].focusInput();
            return;
          }
        }
      }

      //update dữ liệu bản ghi
      this.hanldeOnEdit();
    },

    /**
     * Thực hiện validate dữ liệu khi update
     * Author: LQHUY(08/04/2024)
     */
    handleValidateField() {
      try {
        for (let key in this.textFields) {
          let ref = this.textFields[key].ref;

          //Nếu không thay đổi mật khẩu thì dừng luôn không cần check
          if (
            this.isChangePassword === false &&
            ref === this.textFields.currentPassword.ref
          ) {
            break;
          }

          //validate dữ liệu
          this.$refs[ref].validate();
          let rules = this.textFields[key].rules;
          let nameField = this.textFields[key].name;

          if (rules.required === true) {
            if (
              this.user[nameField] === "" ||
              this.user[nameField] === null ||
              this.user[nameField] === undefined
            ) {
              this.lstErrorMessage[ref] = key;
            } else {
              delete this.lstErrorMessage[ref];
            }
          }
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Thực hiện update bản ghi
     * Author: LQHUY(08/04/2024)
     */
    async hanldeOnEdit() {
      try {
        this.$emitter.emit("toggleShowLoading", true);

        //this.user.DateOfBirth = this.date + "/" + this.month + "/" + this.year;
        var formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.user));

        //gọi Api update bản ghi
        const res = await userService.put(this.user.UserId, formData);
        if (res.status === 200) {
          this.$emitter.emit("toggleShowLoading", false);
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Cập nhật thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );

          //đóng form thay đổi mật khẩu
          this.isChangePassword =
            this.isChangePassword === true
              ? !this.isChangePassword
              : this.isChangePassword;

          //Lấy lại thông tin người dùng lưu vào local storage
          const res = await userService.getById(this.user.UserId);
          if (res.status === 200) {
            localStorageService.setItemEncodeToLocalStorage(
              "userInfo",
              res.data
            );
          }
        }
      } catch (error) {
        const errorObject = error.response.data.errors;
        //hiển thi cho người dùng lỗi từ backend
        for (let key in errorObject) {
          this.lstErrorMessage[key] = errorObject[key].join("");
          //focus vào ô lỗi
          this.$refs[`ref${key}`].focusInput();
        }
        this.$emitter.emit("toggleShowLoading", false);
      }
    },
  },

  data() {
    return {
      isChangePassword: false,
      user: {},
      date: null,
      month: null,
      year: null,
      lstErrorMessage: {},
    };
  },
};
</script>
<style scoped>
.my-account {
  padding: 15px;
}
.page-title h1 {
  font-size: 15px;
  text-transform: uppercase;
  font-weight: 600;
  line-height: 36px;
  color: #333333;
  padding-bottom: 10px;
}

input[type="checkbox"] {
  -webkit-appearance: none;
  border: 2px solid #cacece;
  padding: 9px;
  border-radius: 4px;
  display: inline-block;
  position: relative;
  top: 3px;
  transition-duration: 0.1s;
  height: 0;
}
input[type="checkbox"]:checked {
  /* background-color: #ffffff;
    border: 1px solid #50b83c;
    color: white; */
  background-color: #fff;
  border: 2px solid #c92127;
  color: #c92127;
}

input[type="checkbox"]:checked::after {
  display: block;
  position: absolute;
  content: "\2714";
  font-size: 17px;
  top: calc(-8px / 2);
  left: calc(5px / 2);
}

.group-input-account {
  padding: 4px 0;
}
.group-input-account .input-label {
  width: 186px;
  height: 32px;
  transform: translateY(0px);
}
.group-input-account label {
  height: 100%;
  display: flex;
  align-items: center;
  font-size: 14px;
}
.field-required {
  color: #c92127;
  margin-left: 3px;
}
.group-input-account .m-textfield {
  height: 32px;
  padding: 8px 12px 9px 12px;
}
.group-input-account .m-textfield::placeholder {
  font-style: normal;
}
.group-input-dateofbirth input {
  text-align: center;
}
.group-input-dateofbirth input::placeholder {
  text-align: center;
}

.change-password {
  transform: translateY(-3px);
}
.btn-save-confirm {
  padding: 20px 0 10px 0;
  text-align: center;
}
.btn-save-confirm button {
  background: #c92127;
  height: 40px;
  width: 200px;
  color: white;
  font-weight: 700;
  font-size: 16px;
  border: none;
  font-weight: 700;
  border-radius: 8px;
  -moz-border-radius: 8px;
  -webkit-border-radius: 8px;
}
</style>
