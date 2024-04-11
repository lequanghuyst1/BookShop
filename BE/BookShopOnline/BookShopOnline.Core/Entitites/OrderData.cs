using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class OrderData
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public Order Order { get; set; }
        public List<Guid> CartItemIds { get; set; }
    }
}
