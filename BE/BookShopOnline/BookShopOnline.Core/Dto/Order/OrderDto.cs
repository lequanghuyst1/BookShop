﻿using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Order
{
    public class OrderDto : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Note { get; set; }
    }
}
