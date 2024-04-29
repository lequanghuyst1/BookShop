const bookColumns = [
  {
    field: "BookCode",
    nameField: "Mã sách",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "Image",
    nameField: "Hình ảnh",
    class: "table-align-text",
    width: "120px",
    textAlign: "center",
  },
  {
    field: "BookName",
    nameField: "Tên sách",
    class: "table-align-text",
    width: "352px",
  },
  {
    field: "Author",
    nameField: "Tác giả",
    class: "table-align-text",
    width: "182px",
  },
  {
    field: "Price",
    nameField: "Giá",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",
    type: "money",
  },
  {
    field: "QuantityInStock",
    nameField: "Số lượng tồn",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",
  },
  {
    field: "QuantitySold",
    nameField: "Số lượng bán",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",
  },
  {
    field: "NumberOfPage",
    nameField: "Tổng số trang",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",

  },

  {
    field: "Size",
    nameField: "Kích cỡ",
    class: "table-align-text",
    width: "122px",
  },

  {
    field: "CategoryName",
    nameField: "Tên danh mục",
    class: "table-align-text",
    width: "132px",
  },
  {
    field: "PublisherName",
    nameField: "Tên NXB",
    class: "table-align-text",
    width: "182px",
  },
  {
    field: "PublicationDate",
    nameField: "Ngày xuất bản",
    class: "table-align-text",
    width: "132px",
    textAlign: "center",
    type: "datetime",
  },
];
export default bookColumns;
