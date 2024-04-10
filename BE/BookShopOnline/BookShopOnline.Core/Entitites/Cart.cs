using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class Cart : BaseEntity
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
    }
}
