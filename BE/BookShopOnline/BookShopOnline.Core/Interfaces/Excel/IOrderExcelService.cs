using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Model.Excel;
using BookShopOnline.Core.Model.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Excel
{
    public interface IOrderExcelService : IBaseExcelService<Order>
    {
        byte[] ExportRevenueByTime(ExcelRequest<Order> excelRequest);
        byte[] ExportRevenueByProduct(ExcelRequest<RevenueProduct> excelRequest);
    }
}
