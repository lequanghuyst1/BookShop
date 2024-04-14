using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Enums
{
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
        /// Đang giao hàng
        /// </summary>
        SHIPPING = 2,
        /// <summary>
        /// Đã giao hàng
        /// </summary>
        DELIVERED = 3,
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
        /// Chờ lấy hàng
        /// </summary>
        WAITTING_FOR_DELIVERY = 0,
        /// <summary>
        /// Đã lấy hàng
        /// </summary>
        RECEIVED_THE_GOODS = 1,
    }

    /// <summary>
    /// Hình thức giao hàng
    /// </summary>
    public enum DeliveryMethod
    {
        OCD = 0,
    }

    /// <summary>
    /// Phương thức thanh toán
    /// </summary>
    public enum PaymentMethod
    {
        CASH_PAYMENT =  0,
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
        PAID = 1,
    }
}
