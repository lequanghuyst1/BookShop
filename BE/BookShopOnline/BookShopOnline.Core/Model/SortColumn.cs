using BookShopOnline.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Model
{
    public class SortColumn
    {
        public string? ColumnName { get; set; }
        public SortType? Type { get; set; }
    }
}
