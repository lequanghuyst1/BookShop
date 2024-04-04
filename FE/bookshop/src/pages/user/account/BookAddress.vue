<template>
  <div class="my-address">
    <div class="page-title">
      <h1 v-if="numberPage === 1">Thông tin địa chỉ</h1>
      <h1 v-if="numberPage === 2">
        {{
          formMode === this.$Enum.FormMode.Add
            ? "Thêm mới địa chỉ"
            : "Sửa địa chỉ"
        }}
      </h1>
    </div>
    <div v-if="numberPage === 1" class="block-address">
      <div class="address-item">
        <div class="item__image">
          <img
            src="https://cellphones.com.vn/smember/_nuxt/img/office-building%203.3224147.png"
            alt="cps-image"
          />
        </div>
        <div class="item__content">
          <div class="content__title d-flex gap-3">
            <p class="title__name">Lê Huy</p>
            <p class="title__type">Mặc định</p>
          </div>
          <div class="content__address">
            Số nhà 270/ Đường 417,Huyện Đan Phượng,Hà Nội
          </div>
        </div>
        <div class="button__edit-address">
          <i
            v-tippy="{ content: 'Chỉnh sửa', placement: 'top' }"
            class="fa-solid fa-pen-to-square"
            @click="
              this.isShowActionEditAddress = !this.isShowActionEditAddress
            "
          ></i>
          <div v-if="isShowActionEditAddress" class="group-action-address">
            <ul class="p-0 mb-0">
              <li class="action-item">Đặt làm mặc định</li>
              <li @click="onShowFormEdit" class="action-item">
                Chỉnh sửa địa chỉ
              </li>
              <li class="action-item">Xóa địa chỉ địa chỉ</li>
            </ul>
          </div>
        </div>
      </div>
      <div @click="onShowFormAdd" class="btn-add-address">
        <button>Thêm địa chỉ mới</button>
      </div>
    </div>

    <div v-if="this.numberPage === 2" class="block-address-form">
      <form v-on:submit.prevent action="">
        <InputAccount
          title="Tên gợi nhớ"
          placeholder="Nhập tên gợi nhớ"
          ref="reminiscentName"
          :required="true"
          v-model="address.reminiscentName"
          :errorMessage="errorsMessage.reminiscentName"
        ></InputAccount>
        <InputAccount
          title="Số điện thoại liên lạc"
          placeholder="Nhập số điện thoại"
          ref="phoneNumber"
          :required="true"
          v-model="address.phoneNumber"
          :errorMessage="errorsMessage.phoneNumber"
        ></InputAccount>

        <div class="row group-input">
          <div class="input-label">
            <label for="id" class=""
              >Chọn tỉnh<span class="field-required">*</span></label
            >
          </div>
          <div class="col-8">
            <select
              ref="province"
              id="cbProvince"
              class="m-textfield comboxbox p-0 ps-2 w-100"
              :class="{ 'm-textfield-error': errorsMessage.province }"
              v-model="provinceSelected"
            >
              <option value="" selected>Chọn tỉnh/thành phố</option>
              <option
                v-for="item in provinceData"
                :key="item.province_id"
                :value="{ id: item.province_id, name: item.province_name }"
              >
                {{ item.province_name }}
              </option>
            </select>
            <span class="m-error-message">{{ errorsMessage.province }}</span>
          </div>
        </div>

        <div class="row group-input">
          <div class="input-label">
            <label for="cbDistrict" class=""
              >Chọn quận/huyện<span class="field-required">*</span></label
            >
          </div>
          <div class="col-8">
            <select
              id="cbDistrict"
              class="m-textfield comboxbox p-0 ps-2 w-100"
              v-model="districtSelected"
              :class="{ 'm-textfield-error': errorsMessage.district }"
              ref="district"
            >
              <option value="" selected>Chọn quận/huyện</option>
              <option
                v-for="item in districtData"
                :key="item.district_id"
                :value="{ id: item.district_id, name: item.district_name }"
              >
                {{ item.district_name }}
              </option>
            </select>
            <span class="m-error-message">{{ errorsMessage.district }}</span>
          </div>
        </div>

        <div class="row group-input">
          <div class="input-label">
            <label for="id" class=""
              >Chọn xã/phường<span class="field-required">*</span></label
            >
          </div>
          <div class="col-8">
            <select
              id="ward"
              class="m-textfield comboxbox p-0 ps-2 w-100"
              v-model="wardSelected"
              :class="{ 'm-textfield-error': errorsMessage.ward }"
            >
              <option value="" selected>Chọn phường/Xã</option>
              <option
                v-for="item in wardData"
                :key="item.ward_id"
                :value="{ id: item.ward_id, name: item.ward_name }"
              >
                {{ item.ward_name }}
              </option>
            </select>
            <span class="m-error-message">{{ errorsMessage.ward }}</span>
          </div>
        </div>
        <InputAccount
          title="Số nhà/tên đường"
          placeholder="Nhập số nhà/tên đường"
          :required="true"
          ref="houseNumber"
          v-model="address.houseNumber"
          :errorMessage="errorsMessage.houseNumber"
        ></InputAccount>

        <div class="row group-input">
          <div class="input-label"></div>
          <div class="col-8 d-flex gap-2">
            <input id="address-default" type="checkbox" />
            <label for="address-default" class="set-address-default"
              >Đặt làm địa chỉ mặc định</label
            >
          </div>
        </div>

        <div class="block-form-footer">
          <div @click="this.numberPage = 1" class="prev-page">
            <i class="fa-solid fa-angles-left"></i>
            Quay lại
          </div>

          <div @click="hanldeSaveWithMode" class="btn-add-address">
            <button>Thêm địa chỉ</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import InputAccount from "./GroupInput.vue";

export default {
  name: "BookAddressPage",
  components: { InputAccount },
  created() {
    this.getDataProvince();
  },
  mounted() {
    document.addEventListener("click", (e) => {
      if (!e.target.closest(".button__edit-address")) {
        this.isShowActionEditAddress = false;
      }
    });
  },
  watch: {
    /**
     * Theo dõi biến provinceSelected
     * Author: LQHUY(04/04/2024)
     */
    provinceSelected: async function (newValue) {
      if (newValue !== null) {
        this.errorsMessage.province = null;
        await this.getDataDistrict();
      }
    },

    /**
     * Theo dõi biến districtSelected
     * Author: LQHUY(04/04/2024)
     */
    districtSelected: async function (newValue) {
      if (newValue !== null) {
        this.errorsMessage.district = null;
        await this.getDataWard();
      }
    },

    /**
     * Theo dõi biến wardSelected
     * Author: LQHUY(04/04/2024)
     */
    wardSelected: function (newValue) {
      if (newValue !== null) {
        this.errorsMessage.ward = null;
      }
    },
  },
  computed: {
    // formMode: function () {
    //   if (this.address.AddressId) {
    //     return this.$Enum.FormMode.Edit;
    //   } else {
    //     return this.$Enum.FormMode.Add;
    //   }
    // },
  },
  methods: {
    /**
     * Hàm lấy dữ liệu tỉnh, thành phố
     * Author: LQHUY(04/04/2024)
     */
    async getDataProvince() {
      const res = await this.$axios.get(`${this.urlApiAddress}`);
      this.provinceData = res.data.results;
    },

    /**
     * Hàm lấy dữ liệu quận, huyện
     * Author: LQHUY(04/04/2024)
     */
    async getDataDistrict() {
      const res = await this.$axios.get(
        `${this.urlApiAddress}/district/${this.provinceSelected.id}`
      );
      this.districtData = res.data.results;
    },

    /**
     * Hàm lấy dữ liệu xã, phường
     * Author: LQHUY(04/04/2024)
     */
    async getDataWard() {
      const res = await this.$axios.get(
        `${this.urlApiAddress}/ward/${this.districtSelected.id}`
      );
      this.wardData = res.data.results;
    },

    /**
     * Thực hiên mở form thêm mới
     * Author: LQHUY(04/04/2024)
     */
    onShowFormAdd() {
      this.formMode = this.$Enum.FormMode.Add;
      this.numberPage = 2;
      this.errorsMessage = {};
    },

    /**
     *  Thực hiện mở form sửa
     * Author: LQHUY(04/04/2024)
     */

    onShowFormEdit() {
      this.formMode = this.$Enum.FormMode.Edit;
      this.numberPage = 2;
      this.errorsMessage = {};
    },

    /**
     * Thực hiện save theo formMode
     * Author: LQHUY(04/04/2024)
     */
    hanldeSaveWithMode() {
      this.validateData();
      if (Object.keys(this.errorsMessage).length > 0) {
        for (const key in this.errorsMessage) {
          const value = this.errorsMessage[key];
          if (value !== null) {
            this.$refs[key].focusInput();
            break;
          }
        }
        return;
      }
      this.address.addressName =
        `${this.address.houseNumber},${this.wardSelected.name},` +
        `${this.districtData.name},${this.provinceSelected.name}`;
      if (this.formMode === this.$Enum.FormMode.Edit) {
        this.handleEditAddress();
      } else {
        this.handleAddNewAddress();
      }
    },

    /**
     * Thực hiện validate dữ liệu
     * Author: LQHUY(04/04/2024)
     */
    validateData() {
      this.setError(
        this.address.reminiscentName,
        "reminiscentName",
        this.$refs["reminiscentName"].title
      );
      this.setError(
        this.address.phoneNumber,
        "phoneNumber",
        this.$refs["phoneNumber"].title
      );
      this.setError(
        this.address.houseNumber,
        "houseNumber",
        this.$refs["houseNumber"].title
      );
      this.setError(this.provinceSelected.id, "province", "Tỉnh/thành phố");
      this.setError(this.districtSelected.id, "district", "Quận/huyện");
      this.setError(this.wardSelected.id, "ward", "Xã/phường");
    },

    /**
     * Thực hiện set message dữ liệu không hợp lệ
     * Author: LQHUY(04/04/2024)
     */
    setError(value, field, title) {
      if (value === "" || value === null || value === undefined) {
        this.errorsMessage[field] = `${title} không được phép để trống`;
      } else {
        this.errorsMessage[field] = null;
      }
    },

    /**
     * Thực hiện thêm mới một địa chỉ
     * Author: LQHUY(04/04/2024)
     */
    handleAddNewAddress() {},
    /**
     * Thực hiện sửa thông tin địa chỉ
     * Author: LQHUY(04/04/2024)
     */
    handleEditAddress() {},
  },
  data() {
    return {
      isShowActionEditAddress: false,
      formMode: null,
      numberPage: 1,

      /**giá trị của thành phố, huyện, xã được chọn */
      districtSelected: {},
      provinceSelected: {},
      wardSelected: {},

      /**dữ liệu của thành phố, huyện, xã */
      districtData: [],
      provinceData: [],
      wardData: [],

      urlApiAddress: "https://vapi.vnappmob.com/api/province",

      address: {},
      errorsMessage: {},
    };
  },
};
</script>
<style scoped>
.my-address {
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
.address-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-radius: 10px;
  margin-bottom: 20px;
  padding: 10px 16px;
  position: relative;
}
.address-item .item__image {
  padding: 5px;
  border-radius: 50%;
  border: 1px solid #e2e2e2;
  margin-right: 16px;
}
.address-item .item__image image {
  height: 35px;
}
.address-item .item__content {
  text-align: left;
  flex: 1;
}
.item__content .content__title .title__name {
  font-size: 16px;
  font-weight: 700;
}
.item__content .content__title .title__type {
  background: #ffecec;
  border-radius: 5px;
  color: #fd2424;
  font-size: 15px;
  height: -moz-fit-content;
  height: fit-content;
  padding: 1px 11px !important;
  white-space: nowrap;
}
.item__content .content__address {
  font-size: 13px;
  margin-top: 2px;
  color: #333;
}

.comboxbox {
  color: #333;
}
.set-address-default {
  transform: translateY(-4px);
  font-size: 14px;
}
.button__edit-address {
  position: relative;
  cursor: pointer;
}
.group-action-address {
  position: absolute;
  min-width: 150px;
  right: 0;
  background-color: #fff;
  box-shadow: 0 2px 5px #bbb9b9;
  border-radius: 4px;
  flex: 1;
}
.group-action-address .action-item {
  list-style: none;
  padding: 6px 8px;
  font-size: 14px;
  text-align: center;
}
.group-action-address .action-item:hover {
  background-color: #fee;
  color: #c92127;
}

.block-form-footer {
  display: flex;
  align-items: center;
  margin: 20px 0;
}

.prev-page {
  font-size: 14px;
}
.prev-page i {
  font-size: 13px;
}
.prev-page:hover {
  color: #c92127;
  cursor: pointer;
}
.btn-add-address {
  flex: 1;
  display: flex;
  justify-content: center;
}
.btn-add-address button {
  background: #e0052b;
  border: none;
  border-radius: 5px;
  color: #fff;
  cursor: pointer;
  display: block;
  font-size: 14px;
  font-weight: 600;
  padding: 11px 0;
  text-align: center;
  width: 50%;
}

input[type="checkbox"] {
  -webkit-appearance: none;
  border: 2px solid #cacece;
  padding: 8px;
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
  top: calc(-9px / 2);
  left: calc(3px / 2);
}

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
