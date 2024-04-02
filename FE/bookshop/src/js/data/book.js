const bookColumns = [
  {
    field: "bookCode",
    nameField: "Mã sách",
    class: "table-align-text",
    width: "112px",
  },
  {
    field: "image",
    nameField: "Hình ảnh",
    class: "table-align-text",
    width: "100px",
  },
  {
    field: "bookName",
    nameField: "Tên sách",
    class: "table-align-text",
    width: "182px",
  },
  {
    field: "author",
    nameField: "Tác giả",
    class: "table-align-text",
    width: "152px",
  },
  {
    field: "price",
    nameField: "Giá",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",
    type: "money",
  },
  {
    field: "quantityInStock",
    nameField: "Số lượng tồn",
    class: "table-align-text",
    width: "132px",
    textAlign: "right",
  },
  {
    field: "publicationDate",
    nameField: "Ngày xuất bản",
    class: "table-align-text",
    width: "132px",
    textAlign: "center",
    type: "datetime",
  },
  {
    field: "size",
    nameField: "Kích cỡ",
    class: "table-align-text",
    width: "122px",
  },
  {
    field: "numberOfPage",
    nameField: "Tổng số trang",
    class: "table-align-text",
    width: "132px",
  },
  {
    field: "categoryName",
    nameField: "Tên danh mục",
    class: "table-align-text",
    width: "132px",
  },
  {
    field: "publisherName",
    nameField: "Tên NXB",
    class: "table-align-text",
    width: "182px",
  },
];
export default bookColumns;
