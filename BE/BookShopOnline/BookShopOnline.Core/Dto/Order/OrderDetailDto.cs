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
        public int Quantity { get; set; }
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
        public string? Image { get; set; }
        public int? QuantityInStock { get; set; }

    }
}
