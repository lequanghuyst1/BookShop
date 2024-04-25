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
        public string Address { get; set; }

        [DisplayName("Tổng tiền hàng")]

        public double TotalAmount { get; set; }
        [DisplayName("Tiền ship")]
        public double? ShippingFee { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        [DisplayName("Ngày đặt")]
        public DateTime OrderDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Note { get; set; }
    }
}
