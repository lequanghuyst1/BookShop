const orderColumns = [
  {
    field: "OrderCode",
    nameField: "Mã đơn hàng",
    width: "112px",
  },

  {
    field: "OrderDate",
    nameField: "Ngày tạo",
    width: "112px",
    type: "datetime",
  },
  {
    field: "Fullname",
    nameField: "Khách hàng",
    width: "112px",
  },
  {
    field: "OrderStatus",
    nameField: "Trạng thái",
    width: "152px",
    type: "enum",
    enumType: "ORDER_STATUS"
  },
  {
    field: "PaymentStatus",
    nameField: "Thanh toán",
    width: "152px",
    type: "enum",
    enumType: "PAYMENT_STATUS",


  },
  {
    field: "DeliveryStatus",
    nameField: "Giao hàng",
    width: "132px",
    type: "enum",
    enumType: "DELIVERY_STATUS"
  },
  {
    field: "TotalAmount",
    nameField: "Tổng tiền",
    width: "112px",
    textAlign: "right",
    type: "money",
  },
  
];
export default orderColumns;
