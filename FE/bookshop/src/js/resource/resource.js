const ToastMessage = {
  Type: {
    Success: "success",
    Error: "error",
    Warning: "warning",
    Info: "info",
  },
  Status: {
    Success: "Thành công!",
    Error: "Lỗi!",
    Warning: "Cảnh báo!",
    Info: "Thông tin!",
  },
};
const TEXT = {
  FileNameExcel: "Thống kê doanh thu",
}
const Dialog = {
  Type: {
    Question: "question",
    Warning: "warning",
    Info: "info",
  },
};

const Resource = {
  Language: {
    VN: "VN",
    EN: "EN",
  },
  VN: {
    ToastMessage,
    Dialog,
    ErrorMessage(msg) {
      return `${msg} không được để trống`;
    },
    ConfirmDeleteAll(className) {
      return `Bạn có chắc chắn xóa những ${className} đã chọn này?`;
    },
    WanrningMessage: "Cảnh báo",
    TEXT,
    VALIDATE_STATUS: {
      loginFail: "Tên đăng nhập hoặc mật khẩu không đúng",
      email: "Email không đúng định dạng",
      number: "Trường dữ liệu phải là số.",
      invalidData: "Dữ liệu nhập không tồn tại",
      phoneNumber: "Số điên thoại không hợp lệ",
      phoneDeck: "Số điện thoại bàn không hợp lệ",
      identityNumber: "Số CMND/CCCD không hợp lệ",
      required: function (label) {
        return ` ${label} không được để trống`;
      },
      dateOfBirth: "Năm sinh không được lớn hơn năm hiện tại",
      maxlength: function (label, number) {
        return `Độ dài tối đa của  ${label} là ${number} kí tự`;
      },
      datetime: function (label) {
        return ` ${label} không hợp lệ`;
      },
    },

    ENUM_ORDER_STATUS: {
      waitForConfirmation: "Chờ xác nhận",
      confirmed: "Đã xác nhận",
      processing: "Đang xử lý",
      complete: "Hoàn thành",
      cancelled: "Bị hủy",
    },
    ENUM_DELIVERY_STATUS: {
      notDelivery: "Chưa giao hàng",
      beingTranspoted: "Đang giao hàng",
      deliveried: "Đã giao",
      cancelled: "Hủy",
    },
    ENUM_PAYMENT_STATUS: {
      unpaid: "Chưa thanh toán",
      paid: "Đã thanh toán",
      waitForHandle: "Đang xử lý",
    },
    ENUM_PAYMENT_METHOD: {
      cod: "Thanh toán khi nhận hàng",
      vnPay: "Thanh toán bằng VNPAY",
    },
    ENUM_ACCOUNT_STATUS: {
      inActive: "Ngừng hoạt động",
      active: "Đang hoạt động"
    },
    ENUM_GENDER: {
      male: "Nam",
      feMale: "Nữ",
      other: "Khác"
    },
    ENUM_VOUCHER_TYPE : {
      delivery: "Voucher giảm giá",
      shipping: "Voucher vận chuyển"
    }
  },
};
export default Resource;
