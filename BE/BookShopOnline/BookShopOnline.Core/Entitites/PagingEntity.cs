using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class PagingEntity<T>
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
