<template>
  <div class="login">
    <div class="login__container">
      <div class="login__wrapper">
        <div class="login__header">Đăng nhập</div>
        <div class="login__form">
          <form action="">
            <div class="row">
              <div class="col-12">
                <MInput
                  :label="textFields.email.label"
                  :placeholder="textFields.email.placeholder"
                  :ref="textFields.email.ref"
                  :rules="textFields.email.rules"
                  :name="textFields.email.name"
                  autocomplete="email"
                  v-model="user.Email"
                ></MInput>
              </div>
            </div>
            <div class="col-12">
              <MInput
                :label="textFields.password.label"
                :placeholder="textFields.password.placeholder"
                :ref="textFields.password.ref"
                :rules="textFields.password.rules"
                :name="textFields.password.name"
                type="password"
                v-model="user.Password"
              ></MInput>
            </div>
          </form>
        </div>
        <span style="margin-bottom: 16px" class="error-account-failure">{{
          this.errorMessageLogin
        }}</span>
        <a href="" class="forgot-password">Quên mật khẩu</a>

        <div @click="onLoginInSystem" type="" class="login__form-btn">
          Đăng nhập
        </div>
        <div class="login-method">
          <div class="login-with">
            <div class="login-with--line"></div>
            <div class="login-with--title">Đăng nhập với</div>
            <div class="login-with--line"></div>
          </div>
          <div class="login-method-list">
            <div class="login-method-item" method="Google"></div>
            <div class="login-method-item" method="Apple"></div>
            <div class="login-method-item" method="Microsoft"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import TEXT_FIELD from "@/js/resource/text-field";
import userService from "@/utils/UserService";
import { setInfoTokensToStorage } from "@/js/token/TokenService";
import localStorageService from "@/js/storage/LocalStorageService";
import { mapActions, mapGetters } from "vuex";
export default {
  name: "LoginAdminPage",
  created() {},
  mounted() {
    document.addEventListener("keydown", (e) => {
      switch (e.keyCode) {
        case 13:
          this.onClickLogin();
          break;
        default:
          break;
      }
    });
  },
  beforeUnmount() {
    this.setGlobalValidateDefault();
  },
  computed: {
    ...mapGetters(["globalErrorMsg"]),
    //Lấy ra tên và các rằng buộc của người dùng khi đăng nhập
    textFields: function () {
      return TEXT_FIELD[this.$languageCode].userLogin;
    },
  },
  methods: {
    ...mapActions(["setGlobalValidateDefault"]),
    /**
     * Hàm đăng nhập vào hệ thống
     * Author: LQHUY(22/02/2024)
     */
    onLoginInSystem() {
      this.validateTextField();
      if (this.globalErrorMsg.length > 0) {
        const ref = `ref${this.globalErrorMsg[0].name}`;
        this.$refs[ref].setFocus();
        return;
      }
      this.handleLogin();
    },
    /**
     * Hàm kiểm tra dữ liệu thông tin đăng nhập
     * Author: LQHUY(22/02/2024)
     */
    validateTextField() {
      try {
        this.setGlobalValidateDefault();
        for (let key in this.textFields) {
          let ref = this.textFields[key].ref;
          this.$refs[ref].validate();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Hàm gọi API lấy token phân quyền để có thể đăng nhập
     * Author: LQHUY(22/02/2024)
     */
    async handleLogin() {
      this.errorMessageLogin = null;
      try {
        const res = await userService.Login(this.user);
        switch (res.status) {
          case 201:
            setInfoTokensToStorage(
              res.data.AccessToken,
              res.data.RefreshToken,
              res.data.UserDto
            );
            if (
              localStorageService.getItemFromLocalStorage("userInfo")
                .RoleName === "Admin"
            ) {
              this.$router.push("/admin/book-management");
              this.$emitter.emit(
                "onShowToastMessage",
                this.$Resource[this.$languageCode].ToastMessage.Type.Success,
                "Đăng nhập thành công",
                this.$Resource[this.$languageCode].ToastMessage.Status.Success
              );
            } else {
              this.$emitter.emit(
                "onShowToastMessage",
                this.$Resource[this.$languageCode].ToastMessage.Type.Error,
                "Tài khoản này không thể đăng nhập vào hệ thống!",
                this.$Resource[this.$languageCode].ToastMessage.Status.Error
              );
            }
            break;
        }
      } catch (error) {
        this.errorMessageLogin = Object.values(error.response.data.errors).join(
          ""
        );
      }
    },
  },
  data() {
    return {
      user: {},
      errorMessageLogin: null,
    };
  },
};
</script>
<style scoped>
.login__container {
  width: 100%;
  min-height: 100vh;
  position: relative;
  background-image: url(../../../assets/img/275.jpg);
  background-repeat: no-repeat;
  background-size: 100% 100%;
  background-position: center;
}
/* .login__container::before {
    content: "";
    display: block; 
    position: absolute;
    z-index: 1;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    background: linear-gradient(
      rgba(0, 30, 61, 0.6) 0,
      rgba(0, 0, 0, 0.1) 41.42%,
      rgba(0, 0, 0, 0.3) 100%
    );
  }
   */
.login__wrapper {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 400px;
  border-radius: 8px;
  padding: 40px 48px 40px 48px;
  min-height: 340px;
  background: #fff;
  box-shadow: 0 12px 20px rgba(0, 0, 0, 0.12);
  z-index: 2;
}
.login__header {
  text-align: center;
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 16px;
}
.login__body {
}
.wrapper-input {
  margin-bottom: 12px;
}
.wrapper-input .login__form-input {
  font-size: 16px;
  color: #212121;
  line-height: 1.2;
  display: block;
  width: 100%;
  height: 48px;
  border: 1px solid #e0e5e9;
  background: 0 0;
  padding: 14px 16px 15px 16px;
  border-radius: 3px;
  outline: none;
}
.login__form-input:focus {
  border-color: #0073e6;
  transition: all 0.5s;
}
.login__form-input::placeholder {
  color: #a0a2a6;
}
.forgot-password {
  font-size: 14px;
  color: #0073e6;
  line-height: 17px;
  margin-top: 0;
  display: block;
  text-align: left;
  text-decoration: none;
  margin-bottom: 24px;
  margin-top: 4px;
}
.login__form-btn {
  width: 100%;
  font-size: 16px;
  color: #fff;
  line-height: 40px;
  font-weight: 500;
  text-align: center;
  cursor: pointer;
  height: 40px;
  border-radius: 3px;
  background: #0073e6;
  transition: all 0.4s;
}
.login__form-btn:hover {
  background-color: #384fd5;
  box-shadow: 0 2px 15px rgba(42, 126, 252, 0.25);
  transition: all 0.5s;
}
.login-with {
  display: flex;
  margin-top: 24px;
  align-items: center;
  margin-bottom: 16px;
}
.login-with--line {
  height: 1px;
  width: 70px;
  background-color: #9ea1a5;
}
.login-with--title {
  padding: 5px 10px;
  background-color: #fff;
  color: #9ea1a5;
  font-size: 14px;
}
.login-method-list {
  display: flex;
  justify-content: center;
}
.login-method-item {
  width: 40px;
  height: 40px;
  border-radius: 20px;
  margin: 0 4px;
  cursor: pointer;
  background-position: center;
  background-size: 40px;
  background-repeat: no-repeat;
}
.login-method-item[method="Google"] {
  background-image: url(../../../assets/icons/icon-google.svg);
}
.login-method-item[method="Apple"] {
  background-image: url(../../../assets/icons/icon-apple.svg);
}
.login-method-item[method="Microsoft"] {
  background-image: url(../../../assets/icons/icon-microsoft.svg);
  background-position: 0 0;
}
.form-input-error-message {
  font-size: 12px;
  color: #ff1d1d;
  height: 20px;
  line-height: 20px;
}
.login__form-input.login__form-input-error {
  border: 1px solid #ff1d1d;
}
.error-account-failure {
  color: #ff1d1d;
  font-size: 14px;
  margin-bottom: 16px;
  display: block;
}
.input-password {
  position: relative;
}
.btn-show-pass {
  display: block;
  position: absolute;
  right: 8px;
  top: 16px;
  width: 16px;
  height: 16px;
  background: url(../../../assets/icons/icon-hide-pass.svg) center no-repeat;
}
.btn-show-pass.active {
  display: block;
  background: url(../../../assets/icons/icon-show-pass.svg) center no-repeat;
}
</style>
