using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    /// <summary>
    /// Class ProductReview
    /// </summary>
    /// Created By: LQHUY(17/04/2024)
    public class ProductReview : BaseEntity
    {
        public Guid ProductReviewId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Fullname { get; set; }

    }
}
