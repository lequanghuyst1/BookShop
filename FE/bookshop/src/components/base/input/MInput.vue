<template>
  <div class="form-group">
    <label
      :for="id"
      class="m-lable"
      :class="{ 'label--required': this.rules?.required }"
      >{{ label }}</label
    >
    <div class="group-input">
      <input
        class="m-textfield login__form-input"
        v-model="inputValue"
        :id="id"
        :type="type ? type : 'text'"
        :class="{ 'm-textfield-error': messageError }"
        :placeholder="placeholder"
        :style="{ 'text-align': textAlign || 'left' }"
        ref="input"
        @blur="onBulrInput"
        autocomplete="current-password"
      />
      <div
        class="btn-show-pass"
        v-if="this.type === 'password'"
        :class="{ active: isShowPassword }"
        @click="onShowPassword"
      ></div>
    </div>
    <span class="m-error-message">{{ messageError }}</span>
  </div>
</template>
<script>
import { validateValue } from "@/js/validate/validate";
import { mapActions } from "vuex";
export default {
  created() {},

  name: "MInput",
  props: {
    label: {
      type: String,
      required: false,
    },
    modelValue: {
      type: [String, Number, Boolean, Array],
      required: false,
    },
    type: {
      type: String,
      required: false,
    },
    placeholder: {
      type: String,
      required: false,
      default: "",
    },
    id: {
      type: String,
      required: false,
    },
    textAlign: {
      type: String,
      required: false,
    },
    rules: {
      type: Object,
      required: false,
    },
    errMsg: {
      type: String,
      required: false,
    },
    name: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      inputValue: null,
      messageError: "",
      isShowPassword: false,
    };
  },
  watch: {
    /**
     * Hàm theo dõi giá trị thay đổi của inputValue
     * @param {string} newValue
     * Author: LQHUY(08/12/2023)
     */
    inputValue: function (newValue) {
      if (newValue === null || newValue === "" || newValue === undefined) {
        this.messageError = this.message;
        //nếu giá trị mới là rỗng hoặc null thì cập nhật giá trị = null
        this.$emit("update:modelValue", null);
      } else {
        this.messageError = "";
        this.$emit("update:modelValue", newValue);
      }
    },
    /**
     * Hàm theo dõi giá trị truyển vào cho inputValue
     * @param {string} newValue
     * Author: LQHUY(08/12/2023)
     */
    modelValue(newValue) {
      this.inputValue = newValue;
    },
    /**
     * Hàm theo dõi message lỗi truyển vào
     * @param {string} newValue
     * Author: LQHUY(08/12/2023)
     */
    errMsg(newValue) {
      this.messageError = newValue;
    },
  },
  methods: {
    ...mapActions(["setGlobalValidateError", "checkErrorEmpty"]),
    // onInputinputValue() {
    //   if (this.formatMoney) {
    //     // Xóa các ký tự không phải số và định dạng do có chữ d nên phải định dạng toàn số
    //     let inputValueFormat = this.$helper.formatMoney(
    //       this.inputValue.replace(/[^0-9]/g, "")
    //     );
    //     this.inputValue = inputValueFormat;
    //   }
    // },
    /**
     * Set focus vào ô input
     * Author: LQHUY(07/12/2002)
     */
    onShowPassword() {
      this.isShowPassword = !this.isShowPassword;
      if (this.isShowPassword) {
        this.$refs["input"].setAttribute("type", "text");
      } else {
        this.$refs["input"].setAttribute("type", "password");
      }
    },
    setFocus() {
      this.$refs["input"].focus();
    },
    /**
     * Set border màu đỏ cho ô input lỗi
     * Author: LQHUY(18/03/2024)
     */
    setBorderError() {
      this.$refs["input"].classList.add("m-textfield-error");
    },

    /**
     * Blur ra ngoài ô input validate
     * Author: LQHUY(07/12/2002)
     */
    onBulrInput() {
      this.validate();
    },

    /**
     * Hàm thực hiện validate theo rule
     * @author LQHUY
     */
    validate() {
      let message = "";
      if (this.rules) {
        if (this.rules?.required === true) {
          message = validateValue.required(this.inputValue, this.label);
          this.hanldeValidate(message);
          if (!message) {
            if (this.rules?.rule.length > 0) {
              this.rules?.rule.forEach((item) => {
                const msgError = validateValue[item](this.inputValue);
                this.hanldeValidate(msgError);
              });
            }
          }
        }
      }
    },

    /**
     * Kiểm tra và thêm message lỗi vào mảng 
     * @param errMessage 
     */
    hanldeValidate(errMessage) {
      if (errMessage) {
        this.messageError = errMessage;
        this.setGlobalValidateError({
          name: this.$props.name,
          message: errMessage,
        });
      } else {
        this.checkErrorEmpty(this.$props.name);
        this.messageError = "";
      }
    },
  },
};
</script>
<style scoped>
@import url(./textfield.css);
.group-input {
  position: relative;
}
.btn-show-pass {
  display: block;
  position: absolute;
  right: 12px;
  top: calc(50% - 8px);
  width: 16px;
  height: 16px;
  background: url(../../../assets/icons/icon-hide-pass.svg) center no-repeat;
}
.btn-show-pass.active {
  display: block;
  background: url(../../../assets/icons/icon-show-pass.svg) center no-repeat;
}
</style>
