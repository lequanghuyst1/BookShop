using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    /// <summary>
    /// Class Order
    /// </summary>
    /// Created By: LQHUY(11/04/2024)
    public class Order : BaseEntity
    {
        public Guid OrderId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Note { get; set; }
    }
}
