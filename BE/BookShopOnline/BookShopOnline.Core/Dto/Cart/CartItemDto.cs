using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto
{
    /// <summary>
    /// Class cartItemDto
    /// </summary>
    /// Created By: LQHUY(10/04/2024)
    public class CartItemDto : BaseEntity
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public double? ProvisionalMoney { get; set; }
    }
}
