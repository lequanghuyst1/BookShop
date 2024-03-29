﻿using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Book
{
    public class BookDto : BaseEntity
    {
        public Guid BookId { get; set; }
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string? Description { get; set; }
        public string Author { get; set; }
        public double? Price { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? Size { get; set; }
        public int? NumberOfPage { get; set; }
        public double? Heavy { get; set; }
        public string? Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public string? CategoryName { get; set; }
        public string? PublisherName { get; set; }
        public int QuantityInStock { get; set; }

    }
}
