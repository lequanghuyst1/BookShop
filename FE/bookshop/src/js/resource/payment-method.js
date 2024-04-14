import Enum from "./enum";
const PAYMENT_METHOD = {
  VN: [
    {
      paymentName: "Ví ZaloPay",
      description:
        'Nhập mã "ZLPFHS": Giảm 10K cho ĐH 300K(Áp dụng cho khách đã từng thanh toán bằng ZaloPay )',
      image:
        "	https://cdn0.fahasa.com/skin/frontend/base/default/images/payment_icon/ico_zalopayapp.svg?q=105574",
      value: Enum.PAYMENT_METHOD.ZALOPAY_WALLTET,
    },
    {
      paymentName: "VNPAY",
      description:
        'Nhập mã "VNPAYONLINE": Giảm 10k cho ĐH từ 50K và giảm 20K cho ĐH từ 200K - Nhập mã tại VNPAY',
      image:
        "https://cdn0.fahasa.com/skin/frontend/base/default/images/payment_icon/ico_vnpay.svg?q=105574",
      value: Enum.PAYMENT_METHOD.VNPAY,
    },
    {
      paymentName: "Ví ShopeePay",
      description:
        'Nhập mã "SPPFHST4": Giảm 10K cho ĐH 200.000Đ - Nhập mã tại SHOPEEPAY - Số lượng có hạn',
      image:
        "https://cdn0.fahasa.com/skin/frontend/base/default/images/payment_icon/ico_airpay.svg?q=105574",
      value: Enum.PAYMENT_METHOD.SHOPPEPAY_WALLET,
    },
    {
      paymentName: "Ví Mono",
      description: null,
      image:
        "https://cdn0.fahasa.com/skin/frontend/base/default/images/payment_icon/ico_momopay.svg?q=105574",
      value: Enum.PAYMENT_METHOD.MONO_WALLET,
    },
    {
      paymentName: "Thanh toán bằng tiền mặt",
      description: null,
      image:
        "https://cdn0.fahasa.com/skin/frontend/base/default/images/payment_icon/ico_cashondelivery.svg?q=105574",
      value: Enum.PAYMENT_METHOD.CASH_PAYMENT,
    },
  ],
};
export default PAYMENT_METHOD;
