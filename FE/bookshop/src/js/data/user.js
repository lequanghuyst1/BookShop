const userColumns = [
  {
    field: "Fullname",
    nameField: "Họ và tên",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "Email",
    nameField: "Email",
    class: "table-align-text",
    width: "152px",
  },
  {
    field: "PhoneNumber",
    nameField: "Số điện thoại",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "Gender",
    nameField: "Giới tính",
    class: "table-align-text",
    width: "82px",
    type: "enum",
    enumType: "GENDER",
  },
  {
    field: "CreatedDate",
    nameField: "Ngày tham gia",
    class: "table-align-text",
    width: "112px",
    type: "datetime",
    textAlign: "center",
  },
  {
    field: "AccountStatus",
    nameField: "Trạng thái",
    class: "table-align-text",
    width: "82px",
    color: "#a0dbb3",
    type: "enum",
    enumType: "ACCOUNT_STATUS",
  },
];
export default userColumns;
