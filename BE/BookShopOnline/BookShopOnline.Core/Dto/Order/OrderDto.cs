using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Order
{
    public class OrderDto : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        [DisplayName("Mã đơn hàng")]
        public string OrderCode { get; set; }

        [DisplayName("Tên khách hàng")]
        public string Fullname { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Tổng hóa đơn")]
        public double TotalAmount { get; set; }
        [DisplayName("Tiền ship")]
        public double? ShippingFee { get; set; }
        [DisplayName("Trạng thái")]
        public OrderStatus OrderStatus { get; set; }

        [DisplayName("Vận chuyển")]
        public DeliveryStatus DeliveryStatus { get; set; }

        [DisplayName("Phương thức vận chuyển")]
        public DeliveryMethod DeliveryMethod { get; set; }

        [DisplayName("Phương thức thanh toán")]
        public PaymentMethod PaymentMethod { get; set; }

        [DisplayName("Thanh toán")]
        public PaymentStatus PaymentStatus { get; set; }

        [DisplayName("Số lượng mua")]
        public int TotalQuantity { get; set; }

        [DisplayName("Tiền hàng")]
        public double TotalProductCost { get; set; }

        [DisplayName("Ngày đặt")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Ngày hủy")]
        public DateTime? CancellationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Note { get; set; }
    }
}
