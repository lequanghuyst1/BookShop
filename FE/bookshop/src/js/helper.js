import Enum from "./resource/enum";
import Resource from "./resource/resource";
const resource = () => {
  return Resource["VN"];
};
const helper = {
  /**
   * Hàm định dạng ngày tháng
   * @param {date} date giá trị ngày tháng
   * @param {boolean} displayOnForm xác định xem hiển thị lên table hay form
   * @returns trả về định dạng để hiển thị lên form và table
   * Author: LQHUY
   */
  formatDate(date, displayOnForm) {
    try {
      if (date) {
        //chuyển về dũ liệu date trong js
        let dob = new Date(date);
        let day = dob.getDate() < 10 ? `0${dob.getDate()}` : dob.getDate();
        let month =
          dob.getMonth() + 1 < 10
            ? `0${dob.getMonth() + 1}`
            : dob.getMonth() + 1;
        let year = dob.getFullYear();
        if (displayOnForm) {
          return `${year}-${month}-${day}`;
        } else {
          return `${day}/${month}/${year}`;
        }
      } else {
        return "";
      }
    } catch (error) {
      console.error(error);
    }
  },

  /**
   * Hàm định dạng giới tính
   * @param {int} gender giá trị ngày tháng
   * @returns trả về định dạng để hiển thị lên form và table
   * Author: LQHUY
   */
  formatGender(gender) {
    try {
      if (gender !== null || gender !== undefined) {
        switch (gender) {
          case Enum.Gender.Male:
            return resource().Gender.Male;
          case Enum.Gender.Female:
            return resource().Gender.Female;
          case Enum.Gender.Other:
            return resource().Gender.Other;
          default:
            return "";
        }
      }
    } catch (error) {
      console.error(error);
    }
  },
  /**
   * Hàm định dạng tiền
   * @param {double} money
   * @returns định dạng muốn hiển thị (VNĐ)
   * Author: LQHUY (24/11/2023)
   */
  formatMoney(money) {
    try {
      if (money !== null && money !== undefined) {
        money = new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        })
          .format(money)
          .replace("₫", "");
        return money;
      } else {
        return "";
      }
    } catch (error) {
      console.error(error);
    }
  },
  /**
   * Hàm định dạng lại giá trị là tiển từ input gửi lên API
   * @param {string} value
   * @returns giá trị đúng
   * Author: LQHUY(13/12/2023)
   */
  formatMoneySendApi(value) {
    try {
      if (value) {
        return value
          .replace(/[^\dđ]/gi, "")
          .replace(/đ/gi, "d")
          .replace(/d/gi, "");
      }
      return "";
    } catch (error) {
      console.error(error);
    }
  },

  /**
   * Hàm chuyển chuỗi tiếng Việt thành k dấu và loại bỏ các ký tự đặc biệt
   * @param {string} str
   * @returns string
   * Author: LQHUY(26/01/2024)
   */
  removeVietnameseTones(str) {
    str = str.toLowerCase();
    str = str.replace(/[áàảãạăắằẳẵặâấầẩẫậ]/g, "a");
    str = str.replace(/[éèẻẽẹêếềểễệ]/g, "e");
    str = str.replace(/[íìỉĩị]/g, "i");
    str = str.replace(/[óòỏõọôốồổỗộơớờởỡợ]/g, "o");
    str = str.replace(/[úùủũụưứừửữự]/g, "u");
    str = str.replace(/[ýỳỷỹỵ]/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
  },

  hanldeValueTypeEnum(typeEnum, value) {
    switch (typeEnum) {
      case "ORDER_STATUS":
        if (value === Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION) {
          return resource().ENUM_ORDER_STATUS.waitForConfirmation;
        } else if (value === Enum.ORDER_STATUS.CONFIRMED) {
          return resource().ENUM_ORDER_STATUS.confirmed;
        } else if (value === Enum.ORDER_STATUS.PROCESSING) {
          return resource().ENUM_ORDER_STATUS.processing;
        } else if (value === Enum.ORDER_STATUS.COMPLETE) {
          return resource().ENUM_ORDER_STATUS.complete;
        } else if (value === Enum.ORDER_STATUS.CANCELLED) {
          return resource().ENUM_ORDER_STATUS.cancelled;
        }
        break;
      case "DELIVERY_STATUS":
        if (value === Enum.DELIVERY_STATUS.NOT_DELIVERY) {
          return resource().ENUM_DELIVERY_STATUS.notDelivery;
        } else if (value === Enum.DELIVERY_STATUS.BEING_TRANSPORTED) {
          return resource().ENUM_DELIVERY_STATUS.beingTranspoted;
        } else if (value === Enum.DELIVERY_STATUS.DELIVERIED) {
          return resource().ENUM_DELIVERY_STATUS.deliveried;
        } else if (value === Enum.DELIVERY_STATUS.CANCELLED) {
          return resource().ENUM_DELIVERY_STATUS.cancelled;
        }
        break;
      case "PAYMENT_STATUS":
        if (value === Enum.PAYMENT_STATUS.UNPAID) {
          return resource().ENUM_PAYMENT_STATUS.unpaid;
        } else if (value === Enum.PAYMENT_STATUS.PAID) {
          return resource().ENUM_PAYMENT_STATUS.paid;
        } else if (value === Enum.PAYMENT_STATUS.WAIT_FOR_HANDLE) {
          return resource().ENUM_PAYMENT_STATUS.waitForHandle;
        }
        break;
      case "PAYMENT_METHOD":
        if (value === Enum.PAYMENT_METHOD.COD) {
          return resource().ENUM_PAYMENT_METHOD.cod;
        } else if (value === Enum.PAYMENT_METHOD.VNPAY) {
          return resource().ENUM_PAYMENT_METHOD.vnPay;
        }
        break;
      case "ACCOUNT_STATUS":
        if (value === Enum.ACCOUNT_STATUS.IN_ACTIVE) {
          return resource().ENUM_ACCOUNT_STATUS.inActive;
        } else if (value === Enum.ACCOUNT_STATUS.ACTIVE) {
          return resource().ENUM_ACCOUNT_STATUS.active;
        }
        break;
      case "GENDER":
        if (value === Enum.GENDER.MALE) {
          return resource().ENUM_GENDER.male;
        } else if (value === Enum.GENDER.FEMALE) {
          return resource().ENUM_GENDER.feMale;
        } else if (value === Enum.GENDER.OTHER) {
          return resource().ENUM_GENDER.other;
        }
        break;
        case "VOUCHER_TYPE": 
        if (value === Enum.VOUCHER_TYPE.COUPONS) {
          return resource().ENUM_VOUCHER_TYPE.delivery;
        } else if (value === Enum.VOUCHER_TYPE.DELIVERY) {
          return resource().ENUM_VOUCHER_TYPE.shipping;
        }
        break;
      default:
        return "";
    }
  },

  formatOrderDate(value) {
    var date = new Date(value);
    // Chuẩn hóa giờ để hiển thị đúng định dạng 12 giờ
    var hour = date.getHours(); // Nếu giờ là 0 thì chuyển thành 12
    var minute = date.getMinutes();

    // Thêm số 0 đằng trước nếu cần
    hour = hour < 10 ? "0" + hour : hour;
    minute = minute < 10 ? "0" + minute : minute;

    // Xác định AM hoặc PM

    // Tạo chuỗi định dạng mong muốn
    var formattedDate =
      date.getDate() +
      "/" +
      (date.getMonth() + 1) +
      "/" +
      date.getFullYear() +
      " " +
      hour +
      ":" +
      minute;

    return formattedDate;
  },
};

export default helper;
