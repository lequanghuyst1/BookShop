using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Order
{
    public class OrderDetailDto : BaseEntity
    {
        public Guid OrderDetailId { get; set; }
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
