<template>
  <div class="row group-input">
    <div class="input-label">
      <label :for="id" class=""
        >{{ label
        }}<span v-if="rules?.required" class="field-required">*</span></label
      >
    </div>
    <div class="col-8">
      <input
        type="text"
        :id="id"
        :placeholder="placeholder"
        v-model="inputValue"
        ref="refInput"
        class="m-textfield"
        :class="{ 'm-textfield-error': errMessage }"
      />
      <span class="m-error-message">{{ errMessage }}</span>
    </div>
  </div>
</template>
<script>
import { validateValue } from "@/js/validate/validate";
export default {
  name: "InputAccount",
  props: {
    label: {
      type: String,
      required: false,
    },
    placeholder: {
      type: String,
      required: false,
    },
    required: {
      type: Boolean,
      required: false,
    },
    id: {
      type: String,
      required: false,
    },
    errorMessage: {
      type: String,
      required: false,
    },
    modelValue: {
      type: [String, Number, Boolean, Array],
      required: false,
    },
    rules: {
      type: Object,
      required: false,
    },
  },
  created() {},
  watch: {
    /**
     * Hàm theo dõi giá trị thay đổi của value
     * @param {string} newValue
     * Author: LQHUY(08/12/2023)
     */
    inputValue: function (newValue) {
      if (newValue === null || newValue === "" || newValue === undefined) {
        this.validate();
        //nếu giá trị mới là rỗng hoặc null thì cập nhật giá trị = null
        this.$emit("update:modelValue", null);
      } else {
        this.errMessage = "";
        this.$emit("update:modelValue", newValue);
      }
    },

    modelValue: function (newValue) {
 
      this.inputValue = newValue;
    },
    errorMessage: function (newValue) {
      this.errMessage = newValue;
    },
  },
  methods: {
    focusInput() {
      this.$refs["refInput"].focus();
    },
    validate() {
      if (this.rules) {
        if (this.rules?.required === true) {
          this.errMessage = validateValue.required(
            this.inputValue,
            this.label
          );
          if (!this.errMessage) {
            if (this.rules?.rule.length > 0) {
              this.rules?.rule.forEach((item) => {
                this.errMessage = validateValue[item](this.inputValue);
              });
            }
          }
        }
      }
    },
  },
  data() {
    return {
      errMessage: null,
      inputValue: null,
    };
  },
};
</script>
<style scoped>
.group-input {
  padding: 4px 0;
}
.group-input .input-label {
  width: 186px;
  height: 32px;
  transform: translateY(0px);
}
.group-input label {
  height: 100%;
  display: flex;
  align-items: center;
  font-size: 14px;
}
.field-required {
  color: #c92127;
  margin-left: 3px;
}
.group-input .m-textfield {
  height: 32px;
  padding: 8px 12px 9px 12px;
}
.group-input .m-textfield::placeholder {
  font-style: normal;
}
.group-input-dateofbirth input {
  text-align: center;
}
.group-input-dateofbirth input::placeholder {
  text-align: center;
}
</style>
