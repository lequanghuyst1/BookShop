const voucherColumns = [
  {
    field: "VoucherCode",
    nameField: "Mã khuyễn mãi",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "VoucherType",
    nameField: "Kiểu voucher",
    class: "table-align-text",
    width: "112px",
    type: "enum",
    enumType: "VOUCHER_TYPE"
  },
  {
    field: "AmountDiscount",
    nameField: "Số tiền giảm",
    class: "table-align-text",
    width: "112px",
    textAlign: "right",
    type: "money",
  },
  {
    field: "VoucherCondition",
    nameField: "Điều kiện sử dụng",
    class: "table-align-text",
    width: "112px",
    textAlign: "right",
    type: "money",
  },
  {
    field: "Quantity",
    nameField: "Số lượng",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "ExpiredDate",
    nameField: "Ngày hết hạn",
    class: "table-align-text",
    width: "112px",
    textAlign: "center",
    type: "datetime",
  },
  {
    field: "Description",
    nameField: "Mô tả",
    class: "table-align-text",
    width: "112px",
  },
];

export default voucherColumns;
