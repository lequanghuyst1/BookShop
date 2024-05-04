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

      originalPrice: {
        name: "OriginalPrice",
        label: "Giá gốc",
        ref: "refOriginalPrice",
        placeholder: "",
        rules: {
          lengthMax: 20,
          required: false,
          rule: ["number"],
        },
      },
      discount: {
        name: "Discount",
        label: "Giảm giá(%)",
        ref: "refDiscount",
        placeholder: "",
        rules: {
          lengthMax: 20,
          required: false,
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
          required: false,
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
      numberOfPage: {
        name: "NumberOfPage",
        label: "Số trang",
        ref: "refNumberOfPage",
        placeholder: "",
        rules: {
          lengthMax: 50,
          required: false,
          rule: ["number"],
        },
      },
    },
    category: {
      categoryCode: {
        name: "CategoryCode",
        label: "Mã danh mục",
        ref: "refCategoryCode",
        placeholder: "Nhập mã danh mục",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      categoryName: {
        name: "CategoryName",
        label: "Tên danh mục",
        ref: "refCategoryName",
        placeholder: "Nhập tên danh mục",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      description: {
        name: "Description",
        label: "Mô tả",
        ref: "refDescription",
        placeholder: "Nhập mô tả",
        rules: {
          lengthMax: 255,
          required: false,
          rule: [],
        },
      },
    },
    publisher: {
      publisherCode: {
        name: "PublisherCode",
        label: "Mã nhà xuất bản",
        ref: "refPublisherCode",
        placeholder: "Nhập mã nhà xuất bản",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
      publisherName: {
        name: "PublisherName",
        label: "Tên nhà xuất bản",
        ref: "refPublisherName",
        placeholder: "Nhập tên nhà xuất bản",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },
      phoneNumber: {
        name: "PhoneNumber",
        label: "Số điện thoại",
        ref: "refPhoneNumber",
        placeholder: "Nhập số điện thoại",
        rules: {
          lengthMax: 255,
          required: false,
          rule: [],
        },
      },
      address: {
        name: "Address",
        label: "Địa chi",
        ref: "refAddress",
        placeholder: "Nhập địa chi",
        rules: {
          lengthMax: 255,
          required: false,
          rule: [],
        },
      },
      description: {
        name: "Description",
        label: "Mô tả",
        ref: "refDescription",
        placeholder: "Nhập mô tả",
        rules: {
          lengthMax: 255,
          required: false,
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
      phoneNumber: {
        name: "PhoneNumber",
        label: "Số điện thoại",
        ref: "refPhoneNumber",
        placeholder: "Nhập số điện thoại",
        rules: {
          lengthMax: 50,
          required: true,
          rule: ['phoneNumber'],
        },
      },
      address: {
        name: "Address",
        label: "Địa chỉ",
        ref: "refAddress",
        placeholder: "Nhập địa chỉ",
        rules: {
          lengthMax: 100,
          required: false,
          rule: [],
        },
      },
      accountStatus: {
        name: "AccountStatus",
        label: "Trạng thái",
        ref: "refAccountStatus",
        placeholder: "Trạngđịa chỉ",
        rules: {
          lengthMax: 100,
          required: false,
          rule: [],
        },
      },
      currentPassword: {
        name: "CurrentPassword",
        label: "Mật khẩu hiện tại",
        ref: "refCurrentPassword",
        placeholder: "Nhập mật khẩu hiện tại",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
      newPassword: {
        name: "NewPassword",
        label: "Nhập mật khẩu mới",
        ref: "refNewPassword",
        placeholder: "Nhập mật khẩu mới",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
      renewPassword: {
        name: "RenewPassword",
        label: "Nhập lại mật khẩu mới",
        ref: "refRenewPassword",
        placeholder: "Nhập lại mật khẩu mới",
        rules: {
          lengthMax: 50,
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
    /**
     * Các field Địa chỉ nhận hàng
     */
    delieveryAddress: {
      reminiscentName: {
        name: "ReminiscentName",
        label: "Tên gợi nhớ",
        ref: "refReminiscentName",
        placeholder: "Nhập tên gợi nhớ",
        rules: {
          lengthMax: 100,
          required: true,
          rule: [],
        },
      },

      phoneNumber: {
        name: "PhoneNumber",
        label: "Số điện thoại",
        ref: "refPhoneNumber",
        placeholder: "Nhập số điện thoại",
        rules: {
          lengthMax: 50,
          required: true,
          rule: ["phoneNumber"],
        },
      },
      homeNumber: {
        name: "HomeNumber",
        label: "Số nhà/đường",
        ref: "refHomeNumber",
        placeholder: "Nhập số nhà/tên đường",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
    },
    onlinePayment: {
      accountName: {
        name: "AccountName",
        label: "Tên chủ thẻ",
        ref: "refAccountName",
        placeholder: "Nhập tên chủ thẻ(không dấu)",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
      cardNumber: {
        name: "CardNumber",
        label: "Số thẻ",
        ref: "refCardNumber",
        placeholder: "Nhập số thẻ",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
      releaseDate: {
        name: "ReleaseDate",
        label: "Ngày phát hành",
        ref: "refReleaseDate",
        placeholder: "MM/YYYY",
        rules: {
          lengthMax: 50,
          required: true,
          rule: [],
        },
      },
    },
  },
};
export default TEXT_FIELD;
