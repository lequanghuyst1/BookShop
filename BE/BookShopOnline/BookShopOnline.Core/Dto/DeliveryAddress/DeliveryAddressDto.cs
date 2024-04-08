using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.DeliveryAddress
{
    /// <summary>
    /// Tạo class DeliveryAddressDto
    /// </summary>
    /// Created By: LQHUY(08/04/2024) 
    public class DeliveryAddressDto : BaseEntity
    {
        /// <summary>
        /// Mã định danh địa chỉ nhận hàng
        /// </summary>
        public Guid DeliveryAddressId { get; set; }

        /// <summary>
        /// Mã định dang người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Phường/xã
        /// </summary>
        public string? Ward { get; set; }

        /// <summary>
        /// Quận/huyện
        /// </summary>
        public string? District { get; set; }

        /// <summary>
        /// Tỉnh/thành phố
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// Số nhà/ tên đường
        /// </summary>
        public string? HomeNumber { get; set; }

        /// <summary>
        /// Địa chỉ nhận hàng
        /// </summary>
        public string? DeliveryAddressName { get; set; }

        /// <summary>
        /// Địa chỉ mặc định (1 - mặc định, 0 - không mặc định)
        /// </summary>
        public Boolean? DeliveryAddressDefault { get; set; }

        /// <summary>
        /// Họ và tên người nhận hàng
        /// </summary>
        public string ReminiscentName { get; set; }

        /// <summary>
        /// Số điện thoại người nhận hàng
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
