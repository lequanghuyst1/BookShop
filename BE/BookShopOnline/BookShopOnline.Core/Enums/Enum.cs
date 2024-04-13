using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Enums
{
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
}
