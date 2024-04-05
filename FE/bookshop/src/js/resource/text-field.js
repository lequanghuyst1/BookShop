const TEXT_FIELD = {
  VN: {
    book: {
      bookCode: {
        name: "BookCode",
        label: "Mã sách",
        ref: "refBookCode",
        placeholder: "NV-000001",
        rules: {
          lengthMax: 20,
          required: true,
          rule: [],
        },
      },
      bookName: {
        name: "BookName",
        label: "Tên Sách",
        ref: "refBookName",
        placeholder: "",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      author: {
        name: "Author",
        label: "Tên tác giả",
        ref: "refAuthor",
        placeholder: "",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      price: {
        name: "Price",
        label: "Giá",
        ref: "refPrice",
        placeholder: "",
        rules: {
          lengthMax: 20,
          required: true,
          rule: ["number"],
        },
      },
      quantityImported: {
        name: "QuantityImported",
        label: "Số lượng nhập",
        ref: "refQuantityImported",
        placeholder: "",
        rules: {
          lengthMax: 20,
          required: true,
          rule: ["number"],
        },
      },
      categoryId: {
        name: "CategoryId",
        label: "Danh mục",
        ref: "refCategoryId",
        placeholder: "",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      publisherId: {
        name: "PublisherId",
        label: "Nhà xuất bản",
        ref: "refPublisherId",
        placeholder: "",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
    },
  },
};
export default TEXT_FIELD;
