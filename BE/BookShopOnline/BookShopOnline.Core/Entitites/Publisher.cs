using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class Publisher : BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherCode { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
