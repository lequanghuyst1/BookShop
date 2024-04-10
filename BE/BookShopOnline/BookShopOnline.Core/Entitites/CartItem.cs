using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    /// <summary>
    /// Class cartItem
    /// </summary>
    /// Created By: LQHUY(10/04/2024)
    public class CartItem : BaseEntity
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }

    }
}
