<template>
  <div id="user-login">
    <div class="m-dialog">
      <div class="m-dialog__overlay"></div>
      <div class="m-dialog__container">
        <div class="m-dialog__header">
          <h3 class="m-dialog__header-title">
            {{
              this.mode === this.$Enum.FormAccount.Login
                ? "Đăng nhập"
                : "Đăng ký"
            }}
          </h3>

          <div class="m-dialog__header-action">
            <div
              v-tippy="{
                content: 'Thoát',
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
          <div
            v-if="this.mode === this.$Enum.FormAccount.Login"
            class="login-form"
          >
            <form action="">
              <div class="row">
                <div class="col-12">
                  <MInput
                    :label="textFieldsLogin.email.label"
                    :placeholder="textFieldsLogin.email.placeholder"
                    :ref="textFieldsLogin.email.ref"
                    :rules="textFieldsLogin.email.rules"
                    autocomplete="email"
                    v-model="user.Email"
                  ></MInput>
                </div>
              </div>
              <div class="col-12">
                <MInput
                  :label="textFieldsLogin.password.label"
                  :placeholder="textFieldsLogin.password.placeholder"
                  :ref="textFieldsLogin.password.ref"
                  :rules="textFieldsLogin.password.rules"
                  type="password"
                  v-model="user.Password"
                ></MInput>
              </div>
              <span style="margin-bottom: 16px" class="error-account-failure">{{
                this.errsMsg.Login
              }}</span>
            </form>
            <button
              @click="handleSaveDataWithMode"
              class="m-button m-btn-login"
            >
              Đăng nhập
            </button>
            <div class="login-group-action d-flex justify-content-between">
              <div class="not-account">
                <span>Chưa có tài khoản? </span>
                <p
                  @click="this.mode = this.$Enum.FormAccount.Register"
                  class="d-inline-block"
                  href=""
                >
                  Đăng ký
                </p>
              </div>
              <div class="forgot-password">
                <a href="">Quên mật khẩu?</a>
              </div>
            </div>
          </div>
          <!--End Login form -->

          <!--Start Regiter form -->
          <div
            v-if="this.mode === this.$Enum.FormAccount.Register"
            class="register-form"
          >
            <form action="">
              <div class="row">
                <div class="col-12">
                  <MInput
                    :label="textFieldsRegister.fullname.label"
                    :placeholder="textFieldsRegister.fullname.placeholder"
                    :ref="textFieldsRegister.fullname.ref"
                    :rules="textFieldsRegister.fullname.rules"
                    v-model="user.Fullname"
                  ></MInput>
                  <MInput
                    :label="textFieldsRegister.email.label"
                    :placeholder="textFieldsRegister.email.placeholder"
                    :ref="textFieldsRegister.email.ref"
                    :rules="textFieldsRegister.email.rules"
                    :errMsg="errsMsg.Email"
                    v-model="user.Email"
                  ></MInput>
                </div>
                <MInput
                  :label="textFieldsRegister.password.label"
                  :placeholder="textFieldsRegister.password.placeholder"
                  :ref="textFieldsRegister.password.ref"
                  :rules="textFieldsRegister.password.rules"
                  type="password"
                  v-model="user.Password"
                ></MInput>
                <MInput
                  :label="textFieldsRegister.replicaPassword.label"
                  :placeholder="textFieldsRegister.replicaPassword.placeholder"
                  :ref="textFieldsRegister.replicaPassword.ref"
                  :rules="textFieldsRegister.replicaPassword.rules"
                  :errMsg="errsMsg.ReplicaPassword"
                  type="password"
                  v-model="user.ReplicaPassword"
                ></MInput>
              </div>
            </form>
            <button
              @click="handleSaveDataWithMode"
              class="m-button m-btn-login"
            >
              Đăng Ký
            </button>
            <div class="login-group-action d-flex justify-content-between">
              <div class="have-account">
                <span>Đã có tài khoản? </span>
                <p
                  @click="this.mode = this.$Enum.FormAccount.Login"
                  class="d-inline-block"
                  href=""
                >
                  Đăng nhập
                </p>
              </div>
            </div>
          </div>
        </div>
        <div class="m-dialog__footer">
          <div
            v-if="this.mode === this.$Enum.FormAccount.Login"
            class="login-method"
          >
            <div class="login-with">
              <div class="login-with--line"></div>
              <div class="login-with--title">Hoặc đăng nhập với</div>
              <div class="login-with--line"></div>
            </div>
            <div class="login-method-list">
              <button class="login-method-item" method="Google">
                <span>Google</span>
              </button>
              <button class="login-method-item" method="Facebook">
                <span>Facebook</span>
              </button>
            </div>
          </div>

          <div
            v-if="this.mode === this.$Enum.FormAccount.Register"
            class="register-policy"
          >
            Bằng việc đăng ký, bạn đã đồng ý với Vinabook.com về
            <a href="/">Điều khoản dịch vụ </a> &
            <a href="/"> Chính sách bảo mật</a>
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
import cartItemService from "@/utils/CartItemService";
export default {
  name: "TheLoginUser",
  emits: ["onCloseForm"],
  created() {
    this.mode = this.formAccount;
  },
  mounted() {
    if (this.formAccount === this.$Enum.FormAccount.Login) {
      this.$refs[this.textFieldsLogin.email.ref].setFocus();
    } else {
      this.$refs[this.textFieldsRegister.fullname.ref].setFocus();
    }
  },
  props: {
    formAccount: {
      typeof: Number,
      required: true,
    },
  },
  computed: {
    //Lấy ra tên và các rằng buộc của người dùng khi đăng nhập
    textFieldsLogin: function () {
      return TEXT_FIELD[this.$languageCode].userLogin;
    },

    //Lấy ra tên và các rằng buộc của người dùng khi đăng ký
    textFieldsRegister: function () {
      return TEXT_FIELD[this.$languageCode].userRegister;
    },
    userInfo: function () {
      return localStorageService.getItemEncodeFromLocalStorage("userInfo");
    },
  },
  methods: {
    /**
     * Thực hiện lưu thông tin người dùng khi click vào đăng ký hoặc đăng nhập
     * Author: LQHUY(06/04/2024)
     */
    handleSaveDataWithMode() {
      this.handleValidateField();
      if (this.listErr.length > 0) {
        this.$refs[this.listErr[0]].setFocus();
        return;
      }
      if (this.mode === this.$Enum.FormAccount.Login) {
        this.userLoginInSystem();
      } else {
        this.userRegisterNewAccount();
      }
    },

    /**
     * Hàm kiểm tra password nhập lại có trùng nhau hay không khi đăng ký
     * Author: LQHUY(06/04/2024)
     */
    checkPasswordDuplicateRegister() {
      if (this.user.ReplicaPassword !== this.user.Password) {
        this.errsMsg.ReplicaPassword = "Mật khẩu không trùng khớp.";
        this.listErr.push("refReplicaPassword");
      } else {
        this.errsMsg.ReplicaPassword = null;
        this.listErr = this.listErr.filter(
          (item) => item !== "refReplicaPassword"
        );
      }
    },

    /**
     * Hàm thực hiện validate dữ liệu
     * Author: LQHUY(06/04/2024)
     */
    handleValidateField() {
      try {
        let textFields;
        if (this.mode === this.$Enum.FormAccount.Login) {
          textFields = this.textFieldsLogin;
        } else {
          textFields = this.textFieldsRegister;
        }
        for (let key in textFields) {
          let ref = textFields[key].ref;
          this.$refs[ref].validate();
          let rules = textFields[key].rules;
          let nameField = textFields[key].name;
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
        if (
          this.mode === this.$Enum.FormAccount.Register &&
          this.listErr.length === 0
        ) {
          this.checkPasswordDuplicateRegister();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Người dùng thực hiện đăng nhập vào hệ thống khi click đăng nhập
     * Author: LQHUY(06/04/2024)
     */
    async userLoginInSystem() {
      this.errsMsg = {};
      try {
        const res = await userService.Login(this.user);
        switch (res.status) {
          case 201:
            this.$emit("onCloseForm");
            this.$emitter.emit(
              "onShowToastMessage",
              this.$Resource[this.$languageCode].ToastMessage.Type.Success,
              "Đăng nhập thành công",
              this.$Resource[this.$languageCode].ToastMessage.Status.Success
            );
            setInfoTokensToStorage(
              res.data.AccessToken,
              res.data.RefreshToken,
              res.data.UserDto
            );
            //lấy ra thông tin người dùng
            var userInfo =
              localStorageService.getItemEncodeFromLocalStorage("userInfo");
            //gọi api lấy ra danh sách các sản phẩm có trong giỏ hàng
            var result = await cartItemService.getByCartId(userInfo.CartId);
            if (result.status === 200) {
              localStorageService.setItemToLocalStorage("cart", result.data);
            }
            location.reload();
            break;
        }
      } catch (error) {
        this.errsMsg.Login = Object.values(error.response.data.errors).join("");
      }
    },

    /**
     * Hàm thực hiện đăng ký tài khoản cho user
     * Author: LQHUY(06/04/2024)
     */
    async userRegisterNewAccount() {
      try {
        this.errsMsg = {};
        const res = await userService.RegisterUser(this.user);
        switch (res.status) {
          case 201:
            this.$emit("onCloseForm");
            this.$emitter.emit(
              "onShowToastMessage",
              this.$Resource[this.$languageCode].ToastMessage.Type.Success,
              "Đăng ký thành công",
              this.$Resource[this.$languageCode].ToastMessage.Status.Success
            );
        }
      } catch (error) {
        let errors = error.response.data.errors;
        for (let key in errors) {
          this.errsMsg[key] = errors[key].join("");
        }
      }
    },
  },
  data() {
    return {
      user: {},
      mode: null,
      listErr: [],
      errsMsg: {},
    };
  },
};
</script>
<style scoped>
@import url(../css/login.css);
</style>
