<template>
  <div class="form-group">
    <label :for="id" class="m-lable">{{ label }}</label>
    <textarea
      class="m-textarea"
      :id="id"
      v-model="value"
      name="message"
      :rows="rows ? rows : 10"
      style="width: 100%"
      :placeholder="placeholder"
    ></textarea>
  </div>
</template>
<script>
import { validateValue } from "@/js/validate/validate";
export default {
  name: "MTextarea",
  props: {
    label: {
      type: String,
    },
    modelValue: {
      type: String,
    },
    id: {
      type: String,
    },
    rules: {
      type: Object,
      required: false,
    },
    placeholder: {
      type: String,
    },
    rows: {
      type: Number,
    },
  },
  watch: {
    value(newValue) {
      if (newValue) {
        this.$emit("update:modelValue", newValue);
      } else {
        this.$emit("update:modelValue", null);
      }
    },
    modelValue(newValue) {
      console.log(newValue)

      this.value = newValue;
    },
  },
  methods: {
    validate() {
      if (this.rules) {
        if (this.rules?.required === true) {
          this.messageError = validateValue.required(
            this.inputValue,
            this.label
          );
          if (!this.messageError) {
            if (this.rules?.rule.length > 0) {
              this.rules?.rule.forEach((item) => {
                this.messageError = validateValue[item](this.inputValue);
              });
            }
          }
        }
      }
    },
  },
  data() {
    return {
      value: "",
    };
  },
};
</script>
<style scoped>
.m-textarea {
  outline: none;
  border: 1px solid #e0e0e0;
  border-radius: 4px;
  font-size: 14px;
  padding: 8px 12px 9px 12px;
}
.m-textarea:focus {
  background-color: #ffffff;
  border-color: #50b83c;
}
</style>
