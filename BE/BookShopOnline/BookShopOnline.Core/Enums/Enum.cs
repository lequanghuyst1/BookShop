using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Enums
{
    public enum VoucherType
    {
        COUPONS = 0,
        DELIVERY = 1,
    }
    /// <summary>
    /// Tình trạng đơn hàng
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Chờ xác nhận
        /// </summary>
        WAIT_FOR_CONFIRMATION = 0,
        /// <summary>
        /// Đã xác nhận
        /// </summary>
        CONFIRMED = 1,
        /// <summary>
        /// Đang xử lý
        /// </summary>
        PROCESSING = 2,
        /// <summary>
        /// Hoàn thành
        /// </summary>
        COMPLETE = 3,
        /// <summary>
        /// Đã hủy
        /// </summary>
        CANCELLED = 4,
      
    }

    /// <summary>
    /// Tình trạng giao hàng
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// Chưa giao hàng
        /// </summary>
        NOT_DELIVERY = 0,
        /// Đang giao hàng
        /// </summary>
        BEING_TRANSPORTED = 1,
        /// <summary>
        /// Đã lấy hàng
        /// </summary>
        DELIVERIED = 2,
        /// <summary>
        /// Hủy
        /// </summary>
        CANCELLED = 3,

    }

    /// <summary>
    /// Hình thức giao hàng
    /// </summary>
    public enum DeliveryMethod
    {
        /// <summary>
        /// giao hàng tận nơi
        /// </summary>
        LOCAL_DELIVERY = 0,
    }

    /// <summary>
    /// Phương thức thanh toán
    /// </summary>
    public enum PaymentMethod
    {
        COD = 0,
        ZALOPAY_WALLTET = 1,
        VNPAY = 2,
        MONO_WALLET = 3,
        SHOPPEPAY_WALLET = 4,
    }

    /// <summary>
    /// Tình trạng thanh toán
    /// </summary>
    public enum PaymentStatus
    {
        UNPAID = 0,
        WAIT_FOR_HANDLE = 1,
        PAID = 2,
    }
    public enum SortType
    {
        /// <summary>
        /// Sắp xếp tăng dần
        /// </summary>
        ASC = 1,
        /// <summary>
        /// Sắp xếp giảm dần
        /// </summary>
        DESC = 2,
        /// <summary>
        /// Không sắp xếp
        /// </summary>
        NULL = 0,
    }
    public enum AccountStatus {
        /// <summary>
        /// Ngừng hoạt động
        /// </summary>
        IN_ACTIVE = 0,
        /// <summary>
        /// Đang hoạt động
        /// </summary>
        ACTIVCE = 1,
    }
    public enum Gender
    {
        MALE = 0,
        FEMALE = 1,
        OTHER = 2,
    }

}
