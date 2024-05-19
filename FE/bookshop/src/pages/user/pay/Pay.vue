<template>
  <the-header></the-header>
  <div v-if="isShowPaymentSuccess === false" class="pay-content">
    <div class="container">
      <div class="row">
        <div class="col-7">
          <div class="delivery-address">
            <div class="info-order-title mb-3">
              <h3>Địa chỉ giao hàng</h3>
            </div>
            <div
              v-for="item in addressDelivery"
              :key="item.DeliveryAddressId"
              v-show="this.addressDelivery.length > 0"
              class="select-address"
            >
              <label style="margin-top: 20px" class="warrap__input-radio">
                <input
                  type="radio"
                  v-model="selectedDeliveryAddress"
                  :value="item"
                  @click="this.isShowFormAddress = false"
                  name="address"
                />
                <span class="checkmark"></span>
              </label>
              <div class="address-item">
                <div class="item__image">
                  <img
                    src="https://cellphones.com.vn/smember/_nuxt/img/office-building%203.3224147.png"
                    alt="cps-image"
                  />
                </div>
                <div class="item__content">
                  <div class="content__title d-flex gap-3">
                    <p class="title__name">
                      {{ item?.ReminiscentName }}
                    </p>
                    <p v-if="item.DeliveryAddressDefault" class="title__type">
                      Mặc định
                    </p>
                  </div>
                  <div class="content__address">
                    {{ item?.DeliveryAddressName }}
                  </div>
                  <div class="content__phone">
                    {{ item?.PhoneNumber }}
                  </div>
                </div>
                <!-- <div class="button__edit-address">
                  <i
                    v-tippy="{ content: 'Chỉnh sửa', placement: 'top' }"
                    class="fa-solid fa-pen-to-square"
                    @click="onToggleShowActionItem()"
                  ></i>
                </div> -->
              </div>
            </div>
            <div class="address-different">
              <label class="warrap__input-radio">
                <input
                  @click="()=>{
                    this.isShowFormAddress = true;
                    this.address = {}
                  }"
                  type="radio"
                  name="address"
                />
                <span class="checkmark"></span>
              </label>
              <label
                style="font-size: 14px; margin-left: 16px; margin-top: 18px"
                for=""
                >Giao hàng đến địa chỉ khác</label
              >
            </div>
            <div
              v-if="
                !this.selectedDeliveryAddress?.ReminiscentName ||
                isShowFormAddress
              "
              class="block-address-form"
            >
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
                      :class="{
                        'm-textfield-error': lstErrorMessage.province,
                      }"
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
                    <span class="m-error-message">{{
                      lstErrorMessage.province
                    }}</span>
                  </div>
                </div>

                <div class="row group-input">
                  <div class="input-label">
                    <label for="cbDistrict" class=""
                      >Chọn quận/huyện<span class="field-required"
                        >*</span
                      ></label
                    >
                  </div>
                  <div class="col-8">
                    <select
                      id="cbDistrict"
                      class="m-textfield comboxbox p-0 ps-2 w-100"
                      v-model="districtSelected"
                      :class="{
                        'm-textfield-error': lstErrorMessage.district,
                      }"
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
                    <span class="m-error-message">{{
                      lstErrorMessage.district
                    }}</span>
                  </div>
                </div>

                <div class="row group-input">
                  <div class="input-label">
                    <label for="id" class=""
                      >Chọn xã/phường<span class="field-required"
                        >*</span
                      ></label
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
                    <span class="m-error-message">{{
                      lstErrorMessage.ward
                    }}</span>
                  </div>
                </div>

                <InputAccount
                  :label="textFields.homeNumber.label"
                  :placeholder="textFields.homeNumber.placeholder"
                  :ref="textFields.homeNumber.ref"
                  :rules="textFields.homeNumber.rules"
                  v-model="address.HomeNumber"
                ></InputAccount>
                <div
                  style="margin-top: 16px; margin-bottom: 16px"
                  @click="handleOnAdd"
                  class="btn-add-address"
                >
                  <button>Lưu địa chỉ</button>
                </div>
              </form>
            </div>
          </div>

          <!-- Start: Phương thức vận chuyển -->
          <div class="delivery-method">
            <div class="info-order-title">
              <h3>Phương thức vận chuyển</h3>
            </div>
            <div class="d-flex gap-3 mt-2 mb-2">
              <label class="warrap__input-radio">
                <input
                  type="radio"
                  name="delivery"
                  :value="this.$Enum.DELIVERY_METHOD.LOCAL_DELIVERY"
                  v-model="order.DeliveryMethod"
                />
                <span class="checkmark"></span>
              </label>
              <p style="flex: 1; font-weight: bold; font-size: 14px">
                Giao hàng tận nơi:
                {{ this.$helper.formatMoney(this.order.ShippingFee) }} đ
              </p>
            </div>
          </div>
          <!-- End: Phương thức vận chuyển -->

          <!-- Start: Phương thức thanh toán -->
          <div class="payment-method">
            <div class="info-order-title" style="color: #333">
              <h3>Phương thức thanh toán</h3>
            </div>
            <div class="payment-method-list">
              <div
                v-for="(item, index) in paymentMethods"
                :key="index"
                class="payment-method-item"
              >
                <div class="method-pay">
                  <label class="warrap__input-radio">
                    <input
                      :value="item.value"
                      v-model="order.PaymentMethod"
                      type="radio"
                      name="payment"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <div
                    :style="{
                      background:
                        'url(' + item.image + ') no-repeat center center',
                      width: '40px',
                      height: '24px',
                    }"
                  ></div>
                  <p class="methot-name">{{ item.paymentName }}</p>
                  <a v-show="item.description" class="method-detail" href=""
                    >Chi tiết</a
                  >
                </div>
                <p class="event-payment">
                  {{ item.description }}
                </p>
              </div>
            </div>
          </div>
          <!-- End: Phương thức thanh toán -->

          <!-- Start: Thông tin khác -->
          <div class="info-different">
            <div class="info-order-title">
              <h3>Thông tin khác</h3>
            </div>
            <div class="item order-note">
              <input id="note" v-model="isShowNote" type="checkbox" />
              <label for="note">Ghi chú</label>
              <div
                v-if="isShowNote"
                @click="this.$refs['refNote'].focus()"
                class="note-content mt-3 mb-3"
              >
                <div class="input-group">
                  <input
                    ref="refNote"
                    v-model="order.Note"
                    class="textfiled"
                    type="text"
                  />
                  <div class="label">Ghi chú</div>
                </div>
              </div>
            </div>
            <div class="item export-invoice">
              <input id="note" type="checkbox" />
              <label for="note">Xuất hóa đơn GTGT</label>
            </div>
          </div>
          <!-- End: Thông tin khác -->
        </div>

        <div class="col-5">
          <div class="info-order">
            <div class="info-order-title">
              <h3>Chi tiết đơn hàng</h3>
            </div>
            <div class="order-summary">
              <div
                v-for="item in orderDetailData"
                :key="item.CartItemId"
                class="product"
              >
                <div class="product__image" style="overflow: hidden">
                  <img
                    :src="'https://localhost:7015/' + item.ImagePath"
                    alt=""
                  />
                </div>
                <div class="product__desc">
                  <div
                    v-tippy="{ content: item.BookName, placement: 'top' }"
                    class="product__name"
                  >
                    {{ item.BookName }}
                  </div>
                  <div class="product__price">
                    <p class="product__price--show">
                      {{ this.$helper.formatMoney(item.Price) }}đ
                    </p>
                    <p class="product__price--through">
                      {{ this.$helper.formatMoney(item.OriginalPrice) }}
                      đ
                    </p>
                  </div>
                  <div class="product__quantity">
                    Số lượng: {{ item.Quantity }}
                  </div>
                </div>
                <div class="product__total-amount">
                  {{ this.$helper.formatMoney(item.Quantity * item.Price) }} đ
                </div>
              </div>
            </div>
          </div>

          <div class="discount-code">
            <div class="info-order-title">
              <h3>Mã khuyến mãi, quà tặng</h3>
            </div>
            <form v-on:submit.prevent class="mt-3" action="">
              <div class="discount-code-input">
                <input
                  class="input-discount"
                  type="text"
                  placeholder="Mã giảm giá"
                  v-model="voucherCode"
                />
                <button
                  class="btn-discount-code"
                  :style="{
                    'background-color': voucherCode ? '#338dbc' : '#c8c8c8',
                  }"
                  @click="handleApplyVoucher"
                >
                  Sử dụng
                </button>
              </div>
              <span class="m-error-message" style="font-size: 14px">{{
                errMessageVoucher
              }}</span>
            </form>
          </div>
          <!-- <div class="member-shop">
            <div class="info-order-title">
              <h3>Thành viên</h3>
            </div>
            <div class="member-shop-content">
              <div class="checkout-member">
                <span>Số F-Point hiện có: </span>
                <span class="fpoint">0</span>
              </div>
              <div class="checkout-input-checkbox">
                <input type="checkbox" />
                <label>Sử dụng F-point để thanh toán</label>
              </div>
              <div class="checkout-member">
                <span>Số lần freeship: </span>
                <span class="fpoint">0 lần</span>
              </div>
              <div class="checkout-input-checkbox">
                <input type="checkbox" />
                <label>Sử dụng fresship</label>
              </div>
            </div>
          </div> -->
          <div class="total-order">
            <div class="info-order-title">
              <h3>Thanh toán</h3>
            </div>
            <div class="total-line mt-3 total-sub-total">
              <p class="total-name">Thành tiền</p>
              <p class="total-price">
                {{ this.$helper.formatMoney(this.order.TotalProductCost) }} đ
              </p>
            </div>
            <div class="total-line total-delivery">
              <p class="total-name">Phí vận chuyển</p>
              <p class="total-price">
                {{ this.$helper.formatMoney(this.order.ShippingFee) }}đ
              </p>
            </div>
            <div
              v-if="
                this.voucher.VoucherType === this.$Enum.VOUCHER_TYPE.COUPONS
              "
              class="total-line total-delivery mb-3"
            >
              <p class="total-name">Mã giảm giá</p>
              <p class="total-price">
                - {{ this.$helper.formatMoney(this.order.DiscountCoupons) }}đ
              </p>
            </div>
            <div
              v-if="
                this.voucher.VoucherType === this.$Enum.VOUCHER_TYPE.DELIVERY
              "
              class="total-line total-delivery mb-3"
            >
              <p class="total-name">Mã vận chuyển</p>
              <p class="total-price">
                - {{ this.$helper.formatMoney(this.order.DiscountDelivery) }}đ
              </p>
            </div>
            <div class="total-line total-amount">
              <p class="total-name">Tổng số tiền</p>
              <p class="total-price">
                {{ this.$helper.formatMoney(this.order.TotalAmount) }} đ
              </p>
            </div>
            <div
              style="margin-top: 16px; margin-bottom: 16px"
              @click="handleCheckout"
              class="btn-add-address"
            >
              <button style="width: 100%">
                {{
                  this.order.PaymentMethod === this.$Enum.PAYMENT_METHOD.COD
                    ? "Đặt hàng"
                    : "Xác nhận thanh toán"
                }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <PaymentSuccess
    title="Đặt hàng"
    content="Đơn hàng được đặt thành công"
    v-if="isShowPaymentSuccess"
  ></PaymentSuccess>
</template>
<script>
import TheHeader from "@/components/user/layout/TheHeader.vue";
import TEXT_FIELD from "@/js/resource/text-field";
import localStorageService from "@/js/storage/LocalStorageService";
import InputAccount from "../account/GroupInput.vue";
import deliveryAddressService from "@/utils/DeliveryAddressService";
import PAYMENT_METHOD from "@/js/resource/payment-method";
import cartItemService from "@/utils/CartItemService";
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import orderService from "@/utils/OrderService";
import vnPayService from "@/utils/VnPayService";
import PaymentSuccess from "./PaymentSuccess.vue";
import voucherService from "@/utils/VoucherService";
export default {
  name: "PayUserPage",
  components: { TheHeader, InputAccount, PaymentSuccess },
  mounted() {
    this.getDataProvince();
    this.getAddressDelivery();
    this.getOrderDetailsData();
    document.title = "Thanh toán";
  },
  data() {
    return {
      address: {},

      voucherCode: "",
      voucher: {},
      errMessageVoucher: "",

      urlApiAddress: "https://vapi.vnappmob.com/api/province",

      /**giá trị của thành phố, huyện, xã được chọn */
      districtSelected: {},
      provinceSelected: {},
      wardSelected: {},

      /**dữ liệu của thành phố, huyện, xã */
      districtData: [],
      provinceData: [],
      wardData: [],

      lstErrorMessage: {},
      refListError: [],

      addressDelivery: [],
      selectedDeliveryAddress: {},

      isShowNote: false,
      isShowFormAddress: false,
      orderDetailData: [],
      totalAmountCart: 0,
      order: {
        OrderStatus: this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION,
        PaymentMethod: this.$Enum.PAYMENT_METHOD.COD,
        DeliveryStatus: this.$Enum.DELIVERY_STATUS.NOT_DELIVERY,
        DeliveryMethod: this.$Enum.DELIVERY_METHOD.LOCAL_DELIVERY,
        ShippingFee: 19000,
      },
      isShowPaymentSuccess: false,
    };
  },
  computed: {
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo")
        ? localStorageService.getItemFromLocalStorage("userInfo")
        : {};
    },
    textFields: function () {
      return TEXT_FIELD[this.$languageCode].delieveryAddress;
    },
    paymentMethods: function () {
      return PAYMENT_METHOD[this.$languageCode];
    },
    orderDetailIds: function () {
      return localStorageService.getItemFromLocalStorage("itemSelected");
    },
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
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
  methods: {
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
              this.refListError = this.refListError.filter(
                (item) => item !== ref
              );
            }
          }
        }

        this.setError(
          this.provinceSelected.province_id,
          "refProvince",
          "province",
          "Tỉnh/thành phố"
        );
        this.setError(
          this.districtSelected.district_id,
          "refDistrict",

          "district",
          "Quận/huyện"
        );
        this.setError(
          this.wardSelected.ward_id,
          "refWard",
          "ward",
          "Xã/phường"
        );
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Thực hiện set message dữ liệu không hợp lệ
     * Author: LQHUY(04/04/2024)
     */
    setError(value, ref, field, title) {
      if (value === "" || value === null || value === undefined) {
        this.lstErrorMessage[field] = `${title} không được phép để trống`;
        this.refListError.push(ref);
      } else {
        delete this.lstErrorMessage[field];
        this.refListError = this.refListError.filter((item) => item !== ref);
      }
    },

    /**
     * Thực hiện thêm mới một địa chỉ
     * Author: LQHUY(04/04/2024)
     */
    async handleOnAdd() {
      this.handleValidateField();
      if (this.refListError.length > 0) {
        this.$refs[this.refListError[0]].focusInput();
        return;
      }
      this.addNewAdresss();
    },

    async addNewAdresss() {
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

          this.getAddressDelivery();
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Hàm thực hiện thanh toán khi click btn Thanh toán
     * @author LQHUY(12/04/2024)
     */
    async handleCheckout() {
      try {
        //thiết lập thông tin đơn hàng
        if (
          this.selectedDeliveryAddress?.ReminiscentName &&
          !this.isShowFormAddress
        ) {
          this.order.Fullname = this.selectedDeliveryAddress.ReminiscentName;
          this.order.PhoneNumber = this.selectedDeliveryAddress.PhoneNumber;
          this.order.Address = this.selectedDeliveryAddress.DeliveryAddressName;
        } else {
          this.order.Fullname = this.address.ReminiscentName;
          this.order.PhoneNumber = this.address.PhoneNumber;
          this.order.Address =
            `${this.address.HomeNumber}, ${this.wardSelected.ward_name}, ` +
            `${this.districtSelected.district_name}, ${this.provinceSelected.province_name}`;
        }
        this.order.UserId = this.userInfo.UserId;
        this.order.OrderCode = "";
        this.order.PaymentStatus =
          this.order.PaymentMethod === this.$Enum.PAYMENT_METHOD.COD
            ? this.$Enum.PAYMENT_STATUS.UNPAID
            : this.$Enum.PAYMENT_STATUS.WAIT_FOR_HANDLE;

        //thiết lập các thông tin để truyền khi gọi Api checkout
        const orderInfo = {
          OrderDetails: this.orderDetailData,
          Order: this.order,
          CartItemIds: this.orderDetailIds,
        };
        if (this.voucher.VoucherCode) {
          orderInfo.Voucher = this.voucher;
        }

        //gọi api thanh toán
        const res = await orderService.checkout(orderInfo);
        if (res.status === 201) {
          //gọi lấy lại giá trị giỏ hàng và gán vào local
          const cartData = await cartItemService.getByCartId(
            this.userInfo.CartId
          );
          cartLocalStorageService.setCartToLocalStorage(cartData.data);
          this.$emitter.emit("getQuantityOfCart");
          localStorageService.setItemToLocalStorage("itemSelected", []);

          if (this.order.PaymentMethod === this.$Enum.PAYMENT_METHOD.VNPAY) {
            const paymentInfo = {
              OrderType: "electronic",
              Amount: this.order.TotalAmount,
              TransactionContent: "Thanh toán tiền sách",
              OrderId: res.data.OrderId,
            };
            const resUrl = await vnPayService.CreatePaymentUrl(paymentInfo);
            if (resUrl.status === 201) {
              const urlPayment = resUrl.data;
              //di chuyển đến trang thanh toán online
              location.href = urlPayment;
            }
          } else {
            this.isShowPaymentSuccess = true;
          }
        }
      } catch (error) {
        console.log(error);
        this.$emitter.emit("toggleShowLoading", false);
      }
    },

    /**
     * Thực hiện tính toán tổng số tiền trong giỏ hàng
     * @author LQHUY(09/04/2024)
     */
    calculatorTotalAmountCart() {
      const totalAmount = this.orderDetailData.reduce(
        (accumulator, item) => accumulator + item.Price * item.Quantity,
        0
      );
      const totalQuantity = this.orderDetailData.reduce(
        (accumulator, item) => accumulator + item.Quantity,
        0
      );
      const totalOrder = totalAmount + this.order.ShippingFee;
      this.order.TotalQuantity = totalQuantity;
      this.order.TotalProductCost = totalAmount;
      this.order.TotalAmount = totalOrder;
    },

    async getOrderDetailsData() {
      try {
        const res = await cartItemService.getByIds(this.orderDetailIds);
        if (res.status === 200) {
          this.orderDetailData = res.data;
          this.calculatorTotalAmountCart();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm lấy dữ liệu địa chỉ mặc định của người dùng
     * Author: LQHUY(04/04/2024)
     */
    async getAddressDelivery() {
      try {
        const res = await deliveryAddressService.getAllByUserId(
          this.userInfo.UserId
        );

        if (res.data.length === 0) {
          this.addressDelivery = [];
          return;
        }
        if (res.data.length > 0) {
          this.addressDelivery = res.data;
        }
        const addressDeliveryDefault = res.data.filter(
          (item) => item.DeliveryAddressDefault === true
        );
        this.selectedDeliveryAddress = addressDeliveryDefault[0];
      } catch (error) {
        console.log(error);
      }
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

        if (this.districtSelected.district_id) {
          return;
        }
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

        if (this.wardSelected.ward_id) {
          return;
        }

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
    async handleApplyVoucher() {
      try {
        if (this.voucherCode) {
          const params = {
            voucherCode: this.voucherCode,
            totalProductCost: this.order.TotalProductCost,
          };
          const res = await voucherService.applyVoucher({ params });
          if (res.status === 200) {
            this.errMessageVoucher = "";
            this.voucher = res.data;
            if (this.voucher.VoucherType === this.$Enum.VOUCHER_TYPE.DELIVERY) {
              if (this.voucher.AmountDiscount >= this.order.ShippingFee) {
                this.order.DiscountDelivery = this.order.ShippingFee;
                this.order.TotalAmount -= this.order.DiscountDelivery;
              } else {
                this.order.DiscountDelivery = this.voucher.AmountDiscount;
                this.order.TotalAmount -= this.order.DiscountDelivery;
              }
            } else if (
              this.voucher.VoucherType === this.$Enum.VOUCHER_TYPE.COUPONS
            ) {
              this.order.DiscountCoupons = this.voucher.AmountDiscount;
              this.order.TotalAmount -= this.order.DiscountCoupons;
            }
          }
        }
      } catch (error) {
        if (error.response.status === 400) {
          this.errMessageVoucher = Object.values(
            error.response.data.errors
          ).join("");
        }
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
@import url(./pay.css);
.payment-method {
  padding: 16px;
  background-color: #fff;
  border-radius: 8px;
  margin-top: 32px !important;
  margin-bottom: 32px;
}
.item {
  margin: 8px 0;
}
.item label {
  margin-left: 12px;
  font-size: 14px;
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
/* input radio */
.warrap__input-radio {
  display: inline-block;
  position: relative;
  cursor: pointer;
  user-select: none;
  padding-left: 20px;
}
.warrap__input-radio + .warrap__input-radio {
  margin-left: 24px;
}
.warrap__input-radio input {
  position: absolute;
  opacity: 0;
  cursor: pointer;
}

.checkmark {
  position: absolute;
  top: 0;
  left: 0;
  width: 20px;
  height: 20px;
  border: solid 2px #ccc;
  border-radius: 50%;
}
.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}
.warrap__input-radio .checkmark:after {
  top: 50%;
  left: 50%;
  width: 8px;
  height: 8px;
  border-radius: 50%;
  background-color: #c92127;
  transform: translate(-50%, -50%) rotate(45deg);
}
.warrap__input-radio input:checked ~ .checkmark {
  background: #fff;
  border: solid 2px #c92127;
}
.warrap__input-radio input:checked ~ .checkmark:after {
  display: block;
}
</style>
