<template>
  <div id="m-dialog__info-voucher" class="m-dialog">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container w-50">
      <div class="m-dialog__header">
        <h3 class="m-dialog__header-title">
          {{
            this.formMode === this.$Enum.FormMode.Add
              ? "Thêm mới khuyến mãi"
              : "Sửa thông tin khuyến mãi"
          }}
        </h3>
        <div class="m-dialog__header-action">
          <div
            v-tippy="{
              content: 'Giúp',
              placement: 'bottom',
            }"
            class="m-dialog__header-help"
          >
            <i class="fa-regular fa-circle-question"></i>
          </div>
          <div
            v-tippy="{
              content: 'Đóng',
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
        <form action="" style="width: 100%">
          <div class="row">
            <div class="col-6">
              <MInput
                :ref="textFields.voucherCode.ref"
                :label="textFields.voucherCode.label"
                :rules="textFields.voucherCode.rules"
                :name="textFields.voucherCode.name"
                v-model="voucher.VoucherCode"
              ></MInput>
            </div>
            <div class="col-6">
              <MCombobox
                propValue="value"
                propText="name"
                :dataCombobox="comboboxData"
                :ref="textFields.voucherType.ref"
                :label="textFields.voucherType.label"
                :rules="textFields.voucherType.rules"
                v-model="voucher.VoucherType"
                id="cbAccountStatus"
              ></MCombobox>
            </div>
          </div>
          <div class="row">
            <div class="col-6">
              <MInput
                :ref="textFields.amountDiscount.ref"
                :label="textFields.amountDiscount.label"
                :rules="textFields.amountDiscount.rules"
                :name="textFields.amountDiscount.name"
                v-model="voucher.AmountDiscount"
              ></MInput>
            </div>
            <div class="col-6">
              <MInput
                :ref="textFields.voucherCondition.ref"
                :label="textFields.voucherCondition.label"
                :rules="textFields.voucherCondition.rules"
                :name="textFields.voucherCondition.name"
                v-model="voucher.VoucherCondition"
              ></MInput>
            </div>
          </div>
          <div class="row">
            <div class="col-6">
              <MInput
                :ref="textFields.quantity.ref"
                :label="textFields.quantity.label"
                :rules="textFields.quantity.rules"
                :name="textFields.quantity.name"
                v-model="voucher.Quantity"
              ></MInput>
            </div>
            <div class="col-6">
              <MInput
                :ref="textFields.expiredDate.ref"
                :label="textFields.expiredDate.label"
                :rules="textFields.expiredDate.rules"
                :name="textFields.expiredDate.name"
                type="date"
                v-model="voucher.ExpiredDate"
              ></MInput>
            </div>
          </div>
          <div class="row">
            <div class="col l-12">
              <MInput
                :ref="textFields.description.ref"
                :label="textFields.description.label"
                :rules="textFields.description.rules"
                :name="textFields.description.name"
                v-model="voucher.Description"
              ></MInput>
            </div>
          </div>
        </form>
      </div>
      <div class="m-dialog__footer">
        <div class="m-dialog__footer-left">
          <MButton
            v-tippy="{
              content: 'Hủy',
              placement: 'top',
            }"
            class="m-button--sub"
            @click="this.$emit('onCloseForm')"
            text="Hủy"
          ></MButton>
        </div>
        <div class="m-dialog__footer-right">
          <div class="m-dialog__group-button">
            <MButton
              v-tippy="{
                content: 'Thêm mới',
                placement: 'top',
              }"
              @click="handleSaveDataWithMode"
              :text="
                this.formMode === this.$Enum.FormMode.Add
                  ? 'Thêm mới'
                  : 'Sửa thông tin'
              "
              class="m-button"
            ></MButton>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TEXT_FIELD from "@/js/resource/text-field";
import voucherService from "@/utils/VoucherService";
import { mapGetters } from "vuex";
import { mapActions } from "vuex";
export default {
  name: "voucherDetail",
  props: {
    voucherIdSelected: {
      type: String,
    },
    formMode: {
      type: Number,
    },
  },
  emits: ["loadData", "onCloseForm"],
  created() {
    this.checkModeForm();
  },
  mounted() {
    this.$refs[this.textFields.voucherCode.ref].setFocus();
  },
  beforeUnmount() {
    this.setGlobalValidateDefault();
  },
  computed: {
    ...mapGetters(["globalErrorMsg"]),
    textFields() {
      return TEXT_FIELD[this.$languageCode].voucher;
    },
  },
  methods: {
    ...mapActions(["setGlobalValidateDefault"]),
    /**
     * Thực hiện kiểm tra giá trị formMode
     * @author LQHUY(13/04/2024)
     */
    checkModeForm() {
      if (this.formMode == this.$Enum.FormMode.Edit) {
        this.getVoucherDetail();
      }
    },

    /**
     * Hàm thực hiện save dữ liệu theo formMode khi click btn Thêm mới hoặc sửa
     * @author LQHUY(13/04/2024)
     */
    handleSaveDataWithMode() {
      this.handleValidateField();
      try {
        if (this.globalErrorMsg.length > 0) {
          const ref = `ref${this.globalErrorMsg[0].name}`;
          this.$refs[ref].setFocus();
          return;
        }
        if (this.formMode === this.$Enum.FormMode.Edit) {
          this.editVoucher();
        } else {
          this.addNewVoucher();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Hàm thực hiện validate dữ liệu
     * @author LQHUY(13/04/2024)
     */
    handleValidateField() {
      try {
        this.setGlobalValidateDefault();
        for (let key in this.textFields) {
          let ref = this.textFields[key].ref;
          this.$refs[ref].validate();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Hàm thực hiện gọi API thêm mới một voucher
     * @author LQHUY(13/04/2024)
     */
    async addNewVoucher() {
      try {
        var formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.voucher));
        const res = await voucherService.post(formData);
        switch (res.status) {
          case 201:
            this.successResponse("Thêm mới thành công");
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện gọi API sửa thông tin voucher theo id
     * @author LQHUY(13/04/2024)
     */
    async editVoucher() {
      try {
        var formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.voucher));
        //gọi api thực hiện sửa
        const res = await voucherService.put(this.voucherIdSelected, formData);
        switch (res.status) {
          case 200:
            this.successResponse("Sửa thành công");
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);

        console.log(error);
      }
    },

    /**
     * Hàm thực hiện gọi API lấy ra thông tin chi tiết voucher theo id
     * @author LQHUY(13/04/2024)
     */
    async getVoucherDetail() {
      try {
        const res = await voucherService.getById(this.$props.voucherIdSelected);
        switch (res.status) {
          case 200:
            this.voucher = res.data;
            this.voucher.ExpiredDate = this.$helper.formatDate(
              this.voucher.ExpiredDate,
              true
            );
            this.$emitter.emit("toggleShowLoading", false);
            break;
          default:
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
        this.$emitter.emit("toggleShowLoading", false);
        console.log(error);
      }
    },

    /**
     * Hàm xử lí khi thêm hoặc sửa thông tin thành công
     * @param {string} message
     * Author: LQHUY
     */
    successResponse(message) {
      try {
        this.$emitter.emit("toggleShowForm");
        this.$emitter.emit(
          "onShowToastMessage",
          this.$Resource[this.$languageCode].ToastMessage.Type.Success,
          message,
          this.$Resource[this.$languageCode].ToastMessage.Status.Success
        );
        this.$emit("onCloseForm");
        this.$emit("loadData");
      } catch (error) {
        console.error(error);
      }
    },
  },
  data() {
    return {
      voucher: {},
      comboboxData: [
        { value: 0, name: "Voucher giảm giá" },
        { value: 1, name: "Voucher vận chuyển" },
        { value: 2, name: "Voucher v" },
      ],
    };
  },
};
</script>
<style scoped></style>
