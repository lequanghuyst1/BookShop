<template>
  <div id="customer-login">
    <div  class="m-dialog">
      <div class="m-dialog__overlay"></div>
      <div class="m-dialog__container">
        <div class="m-dialog__header">
          <h3 class="m-dialog__header-title">
            {{
              this.statusForm === this.$Enum.FormAccount.Login
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
              @click="onCloseDialog"
            >
              <i class="fa-solid fa-xmark"></i>
            </div>
          </div>
        </div>
        <div class="m-dialog__content">
          <div
            v-if="this.statusForm === this.$Enum.FormAccount.Login"
            class="login-form"
          >
            <form action="">
              <div class="row">
                <div class="col-12">
                  <MInput
                    placeholder="Nhập email"
                    label="Email"
                    autocomplete="email"
                    v-model="account.email"
                  ></MInput>
                </div>
                <div class="form-group input-password">
                  <label for="" class="m-lable">Mật khẩu</label>
                  <input
                    class="m-textfield login__form-input"
                    :class="{ ' login__form-input-error': errorList.Password }"
                    :type="showPass ? 'text' : 'password'"
                    autocomplete="current-password"
                    placeholder="Nhập mật khẩu"
                    v-model="account.password"
                  />
                  <div
                    class="btn-show-pass"
                    :class="{ active: showPass }"
                    @click="this.showPass = !this.showPass"
                  ></div>
                  <span class="form-input-error-message">{{
                    errorList.Password
                  }}</span>
                </div>
              </div>
            </form>
            <button class="m-button m-btn-login">Đăng nhập</button>
            <div class="login-group-action d-flex justify-content-between">
              <div class="not-account">
                <span>Chưa có tài khoản? </span>
                <p
                  @click="this.statusForm = this.$Enum.FormAccount.Register"
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
            v-if="this.statusForm === this.$Enum.FormAccount.Register"
            class="register-form"
          >
            <form action="">
              <div class="row">
                <div class="col-12">
                  <MInput
                    placeholder="Nhập email"
                    label="Email"
                    v-model="account.email"
                  ></MInput>
                </div>
                <div class="form-group input-password">
                  <label for="" class="m-lable">Mật khẩu</label>
                  <input
                    class="m-textfield login__form-input"
                    :class="{ ' login__form-input-error': errorList.Password }"
                    :type="showPass ? 'text' : 'password'"
                    placeholder="Nhập mật khẩu"
                    autocomplete="current-password"
                    v-model="account.password"
                  />
                  <div
                    class="btn-show-pass"
                    :class="{ active: showPass }"
                    @click="this.showPass = !this.showPass"
                  ></div>
                  <span class="form-input-error-message">{{
                    errorList.Password
                  }}</span>
                </div>
                <div class="form-group input-password">
                  <label for="" class="m-lable">Nhập lại mật khẩu</label>
                  <input
                    class="m-textfield login__form-input"
                    :class="{ ' login__form-input-error': errorList.Password }"
                    :type="showPass ? 'text' : 'password'"
                    autocomplete="current-password"
                    placeholder="Nhập mật khẩu"
                    v-model="account.password"
                  />
                  <div
                    class="btn-show-pass"
                    :class="{ active: showPass }"
                    @click="this.showPass = !this.showPass"
                  ></div>
                  <span class="form-input-error-message">{{
                    errorList.Password
                  }}</span>
                </div>
              </div>
            </form>
            <button class="m-button m-btn-login">Đăng Ký</button>
            <div class="login-group-action d-flex justify-content-between">
              <div class="have-account">
                <span>Đã có tài khoản? </span>
                <p
                  @click="this.statusForm = this.$Enum.FormAccount.Login"
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
            v-if="this.statusForm === this.$Enum.FormAccount.Login"
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
            v-if="this.statusForm === this.$Enum.FormAccount.Register"
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
export default {
  name: "TheLoginUser",
  created() {
    this.statusForm = this.formAccount;
  },
  props: {
    formAccount: {
      typeof: Number,
      required: true,
    },
  },
  methods: {},
  data() {
    return {
      account: {},
      showPass: false,
      statusForm: null,
      errorList: {},
    };
  },
};
</script>
<style scoped>
@import url(../css/login.css);
</style>
