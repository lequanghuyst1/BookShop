using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Model
{
    public class Filter
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? SearchString { get; set; }
        public string? Slug { get; set; }
        public SortColumn? SortColumn { get; set; }
        public List<FilterInput>? FilterInput { get; set; }
        public List<RangeColumn>? RangeColumn { get; set; }
    }
}
