using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Model.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Excel
{
    public interface IOrderExcelService : IBaseExcelService<OrderDto>
    {
        byte[] ExportRevenueByTime(List<Order> orders);
        byte[] ExportRevenueByProduct(List<RevenueProduct> data);
    }
}
