using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Paging
{
    public class PagingEntity<TEntity>
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }    
        public List<TEntity> Data { get; set; }
    }
}
