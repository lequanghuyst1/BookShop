using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Excel
{
    public interface IOrderExcelService : IBaseExcelService<OrderDto>
    {
        byte[] ExportRevenue(List<Order> orders);
    }
}
