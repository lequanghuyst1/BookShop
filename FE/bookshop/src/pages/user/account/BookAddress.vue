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
      <div
        v-for="(item, index) in lstAddress"
        :key="item.DeliveryAddressId"
        class="address-item"
      >
        <div class="item__image">
          <img
            src="https://cellphones.com.vn/smember/_nuxt/img/office-building%203.3224147.png"
            alt="cps-image"
          />
        </div>
        <div class="item__content">
          <div class="content__title d-flex gap-3">
            <p class="title__name">{{ item.ReminiscentName }}</p>
            <p v-if="item.DeliveryAddressDefault" class="title__type">
              Mặc định
            </p>
          </div>
          <div class="content__address">
            {{ item.DeliveryAddressName }}
          </div>
        </div>
        <div class="button__edit-address">
          <i
            v-tippy="{ content: 'Chỉnh sửa', placement: 'top' }"
            class="fa-solid fa-pen-to-square"
            @click="onToggleShowActionItem(index, item.DeliveryAddressId)"
          ></i>

          <div
            v-if="isShowActionEditAddress[index]"
            class="group-action-address"
          >
            <ul class="p-0 mb-0">
              <li @click="handleSetAddressDefault" class="action-item">
                Đặt làm mặc định
              </li>
              <li @click="onShowFormEdit" class="action-item">
                Chỉnh sửa địa chỉ
              </li>
              <li
                @click="onDeleteItem(item.DeliveryAddressId)"
                class="action-item"
              >
                Xóa địa chỉ
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div @click="onShowFormAdd" class="btn-add-address">
        <button>Thêm địa chỉ mới</button>
      </div>
    </div>

    <div v-show="this.numberPage === 2" class="block-address-form">
      <form v-on:submit.prevent action="">
        <InputAccount
          :label="textFields.reminiscentName.label"
          :placeholder="textFields.reminiscentName.placeholder"
          :ref="textFields.reminiscentName.ref"
          :rules="textFields.reminiscentName.rules"
          v-model="address.ReminiscentName"
        ></InputAccount>
        <InputAccount
          :label="textFields.phoneNumber.label"
          :placeholder="textFields.phoneNumber.placeholder"
          :ref="textFields.phoneNumber.ref"
          :rules="textFields.phoneNumber.rules"
          v-model="address.PhoneNumber"
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
              :class="{ 'm-textfield-error': lstErrorMessage.province }"
              v-model="provinceSelected"
            >
              <option value="" selected>Chọn tỉnh/thành phố</option>
              <option
                v-for="item in provinceData"
                :key="item.province_id"
                :value="{
                  province_id: item.province_id,
                  province_name: item.province_name,
                  province_type: item.province_type,
                }"
              >
                {{ item.province_name }}
              </option>
            </select>
            <span class="m-error-message">{{ lstErrorMessage.province }}</span>
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
              :class="{ 'm-textfield-error': lstErrorMessage.district }"
              ref="district"
            >
              <option value="" selected>Chọn quận/huyện</option>
              <option
                v-for="item in districtData"
                :key="item.district_id"
                :value="{
                  district_id: item.district_id,
                  district_name: item.district_name,
                  district_type: item.district_type,
                  lat: null,
                  lng: null,
                  province_id: this.provinceSelected.province_id,
                }"
              >
                {{ item.district_name }}
              </option>
            </select>
            <span class="m-error-message">{{ lstErrorMessage.district }}</span>
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
              :class="{ 'm-textfield-error': lstErrorMessage.ward }"
            >
              <option value="" selected>Chọn phường/Xã</option>
              <option
                v-for="item in wardData"
                :key="item.ward_id"
                :value="{
                  district_id: this.districtSelected.district_id,
                  ward_id: item.ward_id,
                  ward_name: item.ward_name,
                  ward_type: item.ward_type,
                }"
              >
                {{ item.ward_name }}
              </option>
            </select>
            <span class="m-error-message">{{ lstErrorMessage.ward }}</span>
          </div>
        </div>

        <InputAccount
          :label="textFields.homeNumber.label"
          :placeholder="textFields.homeNumber.placeholder"
          :ref="textFields.homeNumber.ref"
          :rules="textFields.homeNumber.rules"
          v-model="address.HomeNumber"
        ></InputAccount>

        <div class="row group-input">
          <div class="input-label"></div>
          <div class="col-8 d-flex gap-2">
            <input
              v-model="this.address.DeliveryAddressDefault"
              id="address-default"
              type="checkbox"
            />
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

          <div @click="handleSave" class="btn-add-address">
            <button>
              {{
                this.formMode === this.$Enum.FormMode.Add
                  ? "Thêm địa chỉ"
                  : "Sửa địa chỉ"
              }}
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
  <MDialog
    v-if="isShowDialogDelete"
    title="Xác nhận xóa"
    @onCloseDialog="onHideDialogDelete"
    :message="messageDialog"
    :type="this.$Resource[this.$languageCode].Dialog.Type.Question"
  >
    <template #footerLeft>
      <MButton
        @click="onHideDialogDelete"
        class="m-button--sub"
        text="Hủy"
      ></MButton>
    </template>
    <template #footerRight>
      <MButton @click="handleOnDelete" text="Xác nhận"></MButton>
    </template>
  </MDialog>
</template>
<script>
import deliveryAddressService from "@/utils/DeliveryAddressService";
import InputAccount from "./GroupInput.vue";
import TEXT_FIELD from "@/js/resource/text-field";
import localStorageService from "@/js/storage/LocalStorageService";
export default {
  name: "BookAddressPage",
  components: { InputAccount },
  created() {
    this.handleLoadData();
    this.getDataProvince();
  },
  mounted() {
    document.addEventListener("click", (e) => {
      if (!e.target.closest(".button__edit-address")) {
        this.isShowActionEditAddress = this.lstAddress.map(() => false);
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
        this.lstErrorMessage.province = null;
        await this.getDataDistrict();
      }
    },

    /**
     * Theo dõi biến districtSelected
     * Author: LQHUY(04/04/2024)
     */
    districtSelected: async function (newValue) {
      if (newValue !== null) {
        this.lstErrorMessage.district = null;
        await this.getDataWard();
      }
    },

    /**
     * Theo dõi biến wardSelected
     * Author: LQHUY(04/04/2024)
     */
    wardSelected: function (newValue) {
      if (newValue !== null) {
        this.lstErrorMessage.ward = null;
      }
    },
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemEncodeFromLocalStorage("userInfo")
        ? localStorageService.getItemEncodeFromLocalStorage("userInfo")
        : {};
    },
    textFields: function () {
      return TEXT_FIELD[this.$languageCode].delieveryAddress;
    },
  },
  methods: {
    /**
     * Hàm lấy ra các bản ghi theo userId
     * Author: LQHUY(04/04/2024)
     */
    async handleLoadData() {
      try {
        //gọi API
        const res = await deliveryAddressService.getAllByUserId(
          this.userInfo.UserId
        );
        if (res.status === 200) {
          this.lstAddress = res.data;
        }
        const addressDefault = this.lstAddress.filter(
          (item) => item.DeliveryAddressDefault === true
        );
        //set địa chỉ mặc định vào local storage
        localStorageService.setItemEncodeToLocalStorage(
          "addressDefault",
          addressDefault
        );
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm ẩn hiện hành động của từng bản ghi khi click icon chỉnh sửa
     * @param {number} index
     * Author: LQHUY(08/04/2024)
     */
    onToggleShowActionItem(index, id) {
      if (this.isShowActionEditAddress[index]) {
        this.isShowActionEditAddress[index] = false;
        this.address = {};
        return;
      }
      this.isShowActionEditAddress = this.lstAddress.map(() => false);
      this.addressIdSelected = id;
      this.isShowActionEditAddress[index] = true;
    },

    /**
     * Hàm lấy dữ liệu tỉnh, thành phố
     * Author: LQHUY(04/04/2024)
     */
    async getDataProvince() {
      try {
        //gọi api lấy dữ liệu tỉnh thành phố
        const res = await this.$axios.get(`${this.urlApiAddress}`);
        //gán giá trị cho provinceData
        this.provinceData = res.data.results;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy dữ liệu quận, huyện
     * Author: LQHUY(04/04/2024)
     */
    async getDataDistrict() {
      try {
        //gọi api lấy dữ liệu quận huyện
        const res = await this.$axios.get(
          `${this.urlApiAddress}/district/${this.provinceSelected.province_id}`
        );

        //gán giá trị cho districtData
        this.districtData = res.data.results;

        //kiểm tra xem thông tin cập nhật có quận huyện hay không
        if (this.address.District) {
          this.districtSelected = this.districtData.filter(
            (item) => item.district_name === this.address.District
          )[0];
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy dữ liệu xã, phường
     * Author: LQHUY(04/04/2024)
     */
    async getDataWard() {
      try {
        //gọi api lấy xã phường
        const res = await this.$axios.get(
          `${this.urlApiAddress}/ward/${this.districtSelected.district_id}`
        );
        //gán lại giá trị cho wardData
        this.wardData = res.data.results;

        //kiểm tra xem thông tin cập nhật có xã phường hay không
        if (this.address.Ward) {
          this.wardSelected = this.wardData.filter(
            (item) => item.ward_name === this.address.Ward
          )[0];
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiên refresh lại form
     * Author: LQHUY(04/04/2024)
     */
    handleRefreshForm() {
      this.address = {};
      this.lstErrorMessage = {};
      this.provinceSelected = {};
      this.districtSelected = {};
      this.wardSelected = {};
    },

    /**
     * Thực hiên mở form thêm mới
     * Author: LQHUY(04/04/2024)
     */
    onShowFormAdd() {
      this.handleRefreshForm();
      this.formMode = this.$Enum.FormMode.Add;
      this.numberPage = 2;
      console.log(this.lstErrorMessage);
    },

    /**
     *  Thực hiện mở form sửa
     * Author: LQHUY(04/04/2024)
     */
    async onShowFormEdit() {
      this.formMode = this.$Enum.FormMode.Edit;
      this.numberPage = 2;
      this.lstErrorMessage = {};
      await this.getInfoAddress();
    },

    /**
     *  Thực hiện lấy thông tin bản ghi được chọn để sửa
     * Author: LQHUY(04/04/2024)
     */
    async getInfoAddress() {
      try {
        const res = await deliveryAddressService.getById(
          this.addressIdSelected
        );
        if (res.status === 200) {
          this.address = res.data;
          if (this.address.Province) {
            //lấy ra provinceSelected có tên trùng với Province
            this.provinceSelected = this.provinceData.filter(
              (item) => item.province_name === this.address.Province
            )[0];
            this.address.Province = null;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện save khi click vào btn thêm hoặc sửa
     * Author: LQHUY(04/04/2024)
     */
    handleSave() {
      this.handleValidateField();
      if (this.refListError.length > 0) {
        this.$refs[this.refListError[0]].focusInput();
        return;
      }
      this.handleSaveWithMode();
    },

    /**
     * Thực hiện save theo formMode
     * Author: LQHUY(04/04/2024)
     */
    handleSaveWithMode() {
      if (this.formMode === this.$Enum.FormMode.Edit) {
        this.handleOnEdit();
      } else {
        this.handleOnAdd();
      }
    },

    /**
     * Thực hiện validate dữ liệu
     * Author: LQHUY(04/04/2024)
     */
    handleValidateField() {
      try {
        for (let key in this.textFields) {
          let ref = this.textFields[key].ref;

          //validate dữ liệu
          this.$refs[ref].validate();
          let rules = this.textFields[key].rules;
          let nameField = this.textFields[key].name;

          if (rules.required === true) {
            if (
              this.address[nameField] === "" ||
              this.address[nameField] === null ||
              this.address[nameField] === undefined
            ) {
              this.lstErrorMessage[ref] = key;
              this.refListError.push(ref);
            } else {
              delete this.lstErrorMessage[ref];
              if (this.refListError.length > 0) {
                this.refListError = this.filter((item) => item !== ref);
              }
            }
          }
        }

        this.setError(
          this.provinceSelected.province_id,
          "province",
          "Tỉnh/thành phố"
        );
        this.setError(
          this.districtSelected.district_id,
          "district",
          "Quận/huyện"
        );
        this.setError(this.wardSelected.ward_id, "ward", "Xã/phường");
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Thực hiện set message dữ liệu không hợp lệ
     * Author: LQHUY(04/04/2024)
     */
    setError(value, field, title) {
      if (value === "" || value === null || value === undefined) {
        this.lstErrorMessage[field] = `${title} không được phép để trống`;
      } else {
        delete this.lstErrorMessage[field];
      }
    },

    /**
     * Thực hiện thêm mới một địa chỉ
     * Author: LQHUY(04/04/2024)
     */
    async handleOnAdd() {
      try {
        this.$emitter.emit("toggleShowLoading", true);

        //gán các giá trị cho address
        this.address.UserId = this.userInfo.UserId;
        this.address.DeliveryAddressName =
          `${this.address.HomeNumber}, ${this.wardSelected.ward_name}, ` +
          `${this.districtSelected.district_name}, ${this.provinceSelected.province_name}`;
        this.address.Ward = this.wardSelected.ward_name;
        this.address.District = this.districtSelected.district_name;
        this.address.Province = this.provinceSelected.province_name;

        //gọi api thêm mới
        var formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.address));
        const res = await deliveryAddressService.post(formData);
        if (res.status === 201) {
          this.$emitter.emit("toggleShowLoading", false);
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Thêm mới thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );

          //quay lại trang danh sách địa chỉ và load lại dữ liệu
          this.numberPage = 1;
          this.handleLoadData();
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Thực hiện sửa thông tin địa chỉ
     * Author: LQHUY(04/04/2024)
     */
    async handleOnEdit() {
      try {
        this.$emitter.emit("toggleShowLoading", true);

        //gán các giá trị cho address
        this.address.UserId = this.userInfo.UserId;
        this.address.DeliveryAddressName =
          `${this.address.HomeNumber}, ${this.wardSelected.ward_name}, ` +
          `${this.districtSelected.district_name}, ${this.provinceSelected.province_name}`;
        this.address.Ward = this.wardSelected.ward_name;
        this.address.District = this.districtSelected.district_name;
        this.address.Province = this.provinceSelected.province_name;

        //gọi api sửa bản ghi
        var formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.address));
        const res = await deliveryAddressService.put(
          this.addressIdSelected,
          formData
        );
        if (res.status === 200) {
          this.$emitter.emit("toggleShowLoading", false);
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Cập nhật thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );

          //quay lại trang danh sách địa chỉ và load lại dữ liệu
          this.numberPage = 1;
          this.handleLoadData();
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Thực hiện thiết lập địa chỉ mặc định khi click vào đặt làm mặc định
     * Author: LQHUY(04/04/2024)
     */
    async handleSetAddressDefault() {
      await this.getInfoAddress();
      this.address.DeliveryAddressDefault = true;
      await this.handleOnEdit();
      this.isShowActionEditAddress = true;
    },

    /**
     * Thực hiện ẩn dialog xóa
     * Author: LQHUY(04/04/2024)
     */
    onHideDialogDelete() {
      this.isShowDialogDelete = false;
      this.addressIdSelected = null;
    },

    /**
     * Thực hiện hiển thị dialog khi click xóa
     * Author: LQHUY(04/04/2024)
     */
    onDeleteItem() {
      this.isShowDialogDelete = true;
      this.messageDialog = "Bạn có chắc chắc muốn xóa địa chỉ này?";
    },

    /**
     * Thực hiện xóa bản ghi
     * @param {string} id
     * Author: LQHUY(04/04/2024)
     */
    async handleOnDelete() {
      try {
        this.$emitter.emit("toggleShowLoading", true);

        const res = await deliveryAddressService.delete(this.addressIdSelected);
        if (res.status === 200) {
          this.$emitter.emit("toggleShowLoading", false);
          this.onHideDialogDelete();
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Xóa thành công",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          //load lại dữ liệu
          this.handleLoadData();
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },
  },
  data() {
    return {
      isShowActionEditAddress: [],
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
      lstErrorMessage: {},
      lstAddress: [],

      addressIdSelected: null,

      refListError: [],
      isShowDialogDelete: false,
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
  z-index: 1;
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
