<template>
  <div class="my-account">
    <div class="page-title">
      <h1>Thông tin tài khoản</h1>
    </div>
    <form v-on:submit.prevent action="" id="form-account-info">
      <InputAccount
        title="Họ và tên"
        id="fullname"
        :required="true"
        placeholder="Nhập họ và tên"
        v-model="user.Fullname"
        ref="fullname"
        :errorMessage="errorsMessage.fullname"
      ></InputAccount>
      <InputAccount
        title="Số điện thoại"
        id="honenumber"
        placeholder="Nhập số điện thoại"
        v-model="user.PhoneNumber"
      ></InputAccount>
      <InputAccount
        title="Email"
        id="email"
        placeholder="Nhập email"
        v-model="user.Email"
      ></InputAccount>
      <InputAccount
        title="Địa chỉ"
        id="address"
        placeholder="Nhập địa chỉ"
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
        <div class="col-8 d-flex align-items-start">
          <input type="checkbox" v-model="isChangePassword" />
          <label for="" class="ms-2 change-password">Đổi mật khẩu</label>
        </div>
      </div>

      <div v-if="isChangePassword" class="change-password-layout">
        <InputAccount
          title="Mật khẩu hiện tại"
          id="password-old"
          :required="true"
          ref="password"
          placeholder="Nhập mật khẩu hiện tại"
          :errorMessage="errorsMessage.password"
          v-model="user.password"
        ></InputAccount>
        <InputAccount
          title="Mật khẩu mới"
          id="password-new"
          :required="true"
          ref="newPassword"
          placeholder="Nhập mật khẩu mới"
          :errorMessage="errorsMessage.newPassword"
          v-model="user.newPassword"
        ></InputAccount>
        <InputAccount
          title="Nhập mật lại khẩu mới"
          id="password-new"
          :required="true"
          ref="renewPassword"
          placeholder="Nhập lại mật khẩu mới"
          :errorMessage="errorsMessage.renewPassword"
          v-model="user.renewPassword"
        ></InputAccount>
      </div>
      <div @click="handleSaveUserInfo" class="btn-save-confirm">
        <button>Cập nhật thông tin</button>
      </div>
    </form>
  </div>
</template>
<script>
import InputAccount from "./GroupInput.vue";
import localStorageService from "@/js/storage/LocalStorageService";
export default {
  name: "InfoAccountUserPage",
  components: { InputAccount },
  created() {
  },
  mounted() {
    this.user = localStorageService.getItemFromLocalStorage("userInfo")
      ? localStorageService.getItemFromLocalStorage("userInfo")
      : {};
    console.log(this.user);

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
  },
  methods: {
    handleSaveUserInfo() {
      this.validateData();
      if (Object.keys(this.errorsMessage).length > 0) {
        for (const key in this.errorsMessage) {
          const value = this.errorsMessage[key];
          if (value !== null) {
            this.$refs[key].focusInput();
            return;
          }
        }
      }
      this.user.dateOfBirth = this.date + "/" + this.month + "/" + this.year;
      console.log(this.user);
    },
    validateData() {
      this.setError("fullname", this.$refs["fullname"].title);
      if (this.isChangePassword) {
        this.setError("password", this.$refs["password"].title);
        this.setError("newPassword", this.$refs["newPassword"].title);
        this.setError("renewPassword", this.$refs["renewPassword"].title);
      }
    },
    setError(field, title) {
      if (
        this.user[field] === "" ||
        this.user[field] === null ||
        this.user[field] === undefined
      ) {
        this.errorsMessage[field] = `${title} không được phép để trống`;
      } else {
        this.errorsMessage[field] = null;
        delete this.errorsMessage[field];
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
      errorsMessage: {},
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
