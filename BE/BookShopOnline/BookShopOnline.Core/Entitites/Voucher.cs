using BookShopOnline.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class Voucher : BaseEntity
    {
        public Guid VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public VoucherType VoucherType { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal VoucherCondition { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
