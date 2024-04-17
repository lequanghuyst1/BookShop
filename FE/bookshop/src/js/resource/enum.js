const Enum = {
  Gender: {
    Male: 0,
    Female: 1,
    Other: 2,
  },
  FormMode: {
    Add: 0,
    Edit: 1,
    Clone: 2,
  },
  FormAccount: {
    Login: 0,
    Register: 1,
  },
  /**Tình trạng đơn hàng */
  ORDER_STATUS: {
    WAIT_FOR_CONFIRMATION: 0,
    CONFIRMED: 1,
    SHIPPING: 2,
    DELIVERED: 3,
    CANCELLED: 4,
  },
  /**Tình trạng giao hàng */
  DELIVERY_STATUS: {
    NOT_DELIVERY: 0,
    WAITTING_FOR_DELIVERY: 1,
    RECEIVED_THE_GOODS: 2,
  },
  /**Hình thức giao hàng */
  DELIVERY_METHOD: {
    OCD: 0,
  },

  /**Tình trạng thanh toán */
  PAYMENT_STATUS: {
    UNPAID: 0,
    PAID: 1,
  },

  /**Hình thức thanh toán */
  PAYMENT_METHOD: {
    CASH_PAYMENT: 0,
    ZALOPAY_WALLTET: 1,
    VNPAY: 2,
    MONO_WALLET: 3,
    SHOPPEPAY_WALLET: 4,
  },
  SORT_TYPE: {
    NULL: 0,
    ASC: 1, 
    DESC: 2,
  }
};

export default Enum;
