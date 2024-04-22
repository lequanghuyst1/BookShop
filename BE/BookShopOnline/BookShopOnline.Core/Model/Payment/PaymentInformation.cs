using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Model
{
    public class PaymentInformation
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string TransactionContent { get; set; }
        public Guid OrderId { get; set; }
    }
}
