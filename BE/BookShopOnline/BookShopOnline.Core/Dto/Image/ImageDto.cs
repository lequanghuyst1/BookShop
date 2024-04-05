using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Image
{
    public class ImageDto : BaseEntity
    {
        public Guid ImageId { get; set; }
        public string ImagePath { get; set; }
        public Guid? BookId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? PublisherId { get; set; }
    }
}
