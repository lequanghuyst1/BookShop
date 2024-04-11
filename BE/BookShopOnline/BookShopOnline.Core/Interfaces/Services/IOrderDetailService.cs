using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IOrderDetailService : IBaseService<OrderDetail, OrderDetailDto>
    {
        Task<OrderDetailDto> InsertManyServiceAsync(List<OrderDetail> orderDetails);

    }
}
