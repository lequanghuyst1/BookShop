using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        Task<int> InsertManyAsync(List<OrderDetail> orderDetails);

    }
}
