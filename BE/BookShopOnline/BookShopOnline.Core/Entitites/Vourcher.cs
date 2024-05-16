using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class Vourcher : BaseEntity
    {
        public Guid VourcherId { get; set; }
        public string VourcherCode { get; set; }
        public int VourcherType { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal VourcherCondition { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
