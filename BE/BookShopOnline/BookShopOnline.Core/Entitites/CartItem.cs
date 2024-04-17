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
        public double? ProvisionalMoney { get; set; }
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public double? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public string Author { get; set; }
        public int QuantityImported { get; set; }
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? Size { get; set; }
        public int? NumberOfPage { get; set; }
        public double? Heavy { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public string? CategoryName { get; set; }
        public string? PublisherName { get; set; }
        public int? QuantityInStock { get; set; }
        public double UnitPrice { get; set; }

    }
}
