<template>
  <div class="form-group">
    <label :for="id" class="m-lable" :class="{ 'label--required': required }">{{
      label
    }}</label>
    <textarea
      class="m-textarea"
      v-model="value"
      name="message"
      rows="10"
      style="width: 100%"
    ></textarea>
  </div>
</template>
<script>
export default {
  name: "MTextarea",
  props: {
    label: {
      type: String,
    },
    modelValue: {
      type: String,
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
      this.value = newValue;
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
