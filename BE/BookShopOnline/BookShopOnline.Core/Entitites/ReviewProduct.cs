using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    /// <summary>
    /// Class ReviewProduct
    /// </summary>
    /// Created By: LQHUY(17/04/2024)
    public class ReviewProduct : BaseEntity
    {
        public Guid ReviewProductId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public Boolean? IsEdit { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Fullname { get; set; }

    }
}
