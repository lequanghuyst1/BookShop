using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    /// <summary>
    /// Class BaseEntity
    /// </summary>
    /// CreatedBy: LQHUY(16/03/2024)
    public class Book : BaseEntity
    {
        public Guid BookId { get; set; }
        [Required(ErrorMessage ="Mã sách không được để trống.")]
        public string BookCode { get; set; }
        [Required(ErrorMessage = "Tên sách không được để trống.")]
        public string BookName { get; set; }

      
        [Range(0, int.MaxValue, ErrorMessage = "Giá sách nhập vào phải là số.")]
        public double? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá sách nhập vào phải là số.")]

        public double? OriginalPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá sách nhập vào phải là số.")]

        public int? Discount { get; set; }

        [Required(ErrorMessage = "Tác giả không được để trống")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Số lượng nhập không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng nhập vào phải là số.")]
        public int? QuantityImported { get; set; }
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? Size { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Tổng số trang nhập vào phải là số.")]
        public int? NumberOfPage { get; set; }
        public string ImagePath { get; set; }
        public double? Heavy { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public string? CategoryName { get; set; }
        public string? PublisherName { get; set; }
        public int? QuantityInStock { get; set; }
        public int? QuantitySold { get; set; }
        public string? Slug { get; set; }
        public string? BookSlug { get; set; }
        public string? CategorySlug { get; set; }
        public string? PublisherSlug { get; set; }

    }
}
