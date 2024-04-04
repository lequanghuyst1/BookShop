<template>
  <div class="row group-input-account">
    <div class="input-label">
      <label :for="id" class=""
        >{{ title }}<span v-if="required" class="field-required">*</span></label
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
        :class="{ 'm-textfield-error': messageValue }"
      />
      <span class="m-error-message">{{ messageValue }}</span>
    </div>
  </div>
</template>
<script>
export default {
  name: "InputAccount",
  props: {
    title: {
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
  },
  watch: {
    errorMessage(newValue) {
      this.messageValue = newValue;
    },
    inputValue(newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
  methods: {
    focusInput() {
      this.$refs["refInput"].focus();
    },
  },
  data() {
    return {
      messageValue: null,
      inputValue: null,
    };
  },
};
</script>
<style scoped>
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
</style>
