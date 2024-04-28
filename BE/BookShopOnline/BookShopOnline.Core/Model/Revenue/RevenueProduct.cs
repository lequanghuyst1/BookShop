using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Model.Revenue
{
    public class RevenueProduct
    {
        public string BookName { get; set; }
        public int TotalQuantitySold { get; set; }
        public double Profit { get; set; }
    }
}
