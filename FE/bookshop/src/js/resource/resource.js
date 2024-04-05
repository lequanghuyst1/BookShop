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
  },
};
export default Resource;
