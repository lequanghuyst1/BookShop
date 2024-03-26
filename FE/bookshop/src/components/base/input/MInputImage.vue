<template>
  <div class="form-group">
    <label :for="id" class="m-lable">{{ label }}</label>
    <label :for="id">
      <input
        class="m-textfield"
        style="display: none"
        :value="value"
        :id="id"
        type="file"
        accept="image/*"
        @change="previewFiles($event)"
      />
      <img
        :src="
          newImage ||
          'https://localhost:7015/images/upload-image.png'
        "
        style="
          width: 100%;
          border: 1px solid #e0e0e0;
          border-radius: 4px;
          cursor: pointer;
          padding: 16px;
        "
      />
    </label>
    <p style="font-size: 12px">
      (Vui lòng chọn ảnh có định dạng <b>.jpg, .jpeg, .png, .gif.</b> )
    </p>
  </div>
</template>
<script>
export default {
  name: "MInputImage",
  created() {},
  props: {
    label: {
      typeof: String,
    },
    modelValue: {
      typeof: String,
    },
    imagePath: {
      typeof: String,
    },
  },
  data() {
    return {
      value: null,
      newImage: null,
    };
  },
  watch: {
    imagePath(value) {
      if (value === null || value === undefined || value === "") {
        this.newImage = null;
      } else {
        this.newImage = `https://localhost:7015/${value}`;
      }
    },
  },
  methods: {
    previewFiles(event) {
      const file = event.target.files[0];
      this.$emit("update:modelValue", file);

      const theReader = new FileReader();
      theReader.onloadend = async () => {
        this.newImage = await theReader.result;
      };
      theReader.readAsDataURL(file);
    },
  },
};
</script>
<style scoped></style>
