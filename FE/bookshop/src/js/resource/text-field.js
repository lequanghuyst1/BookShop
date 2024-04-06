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
    user: {
      fullname: {
        name: "Fullname",
        label: "Họ và tên",
        ref: "refFullname",
        placeholder: "Nhập họ và tên",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      email: {
        name: "Email",
        label: "Email",
        ref: "refEmail",
        placeholder: "Nhập email",
        rules: {
          lengthMax: 100,
          required: true,
          rule: ["email"],
        },
      },
      password: {
        name: "Password",
        label: "Mật khẩu",
        ref: "refPassword",
        placeholder: "Nhập mật khẩu",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      replicaPassword: {
        name: "relicaPassword",
        label: "Nhập lại khẩu",
        ref: "refRelicaPassword",
        placeholder: "Nhập lại mật khẩu",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
    },
    userLogin: {
      email: {
        name: "Email",
        label: "Email",
        ref: "refEmail",
        placeholder: "Nhập email",
        rules: {
          lengthMax: 100,
          required: true,
          rule: ["email"],
        },
      },
      password: {
        name: "Password",
        label: "Mật khẩu",
        ref: "refPassword",
        placeholder: "Nhập mật khẩu",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
    },
    userRegister: {
      fullname: {
        name: "Fullname",
        label: "Họ và tên",
        ref: "refFullname",
        placeholder: "Nhập họ và tên",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      email: {
        name: "Email",
        label: "Email",
        ref: "refEmail",
        placeholder: "Nhập email",
        rules: {
          lengthMax: 100,
          required: true,
          rule: ["email"],
        },
      },
      password: {
        name: "Password",
        label: "Mật khẩu",
        ref: "refPassword",
        placeholder: "Nhập mật khẩu",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      replicaPassword: {
        name: "ReplicaPassword",
        label: "Nhập lại khẩu",
        ref: "refReplicaPassword",
        placeholder: "Nhập lại mật khẩu",
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
