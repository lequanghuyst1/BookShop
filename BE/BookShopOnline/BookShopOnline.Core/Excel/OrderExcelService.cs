using AutoMapper;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Excel;
using BookShopOnline.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Excel
{
    public class OrderExcelService : BaseExcelService<Order, OrderDto>, IOrderExcelService
    {

        public OrderExcelService(IOrderRepository orderRepository, IMapper mapper) : base(orderRepository, mapper)
        {

        }

        public byte[] ExportRevenue(List<Order> orders)
        {
            var ordersDto = orders.Select(item => MapEntityToDto(item));
            var columns = GetColumns();
            var res = ExportExcelAsync(ordersDto.ToList(), columns);
            return res;
        }

        public override List<string> GetColumns()
        {
            var columns = new List<string>();
            columns.Add("OrderCode");
            columns.Add("Fullname");
            columns.Add("OrderDate");
            columns.Add("PhoneNumber");
            columns.Add("ShippingFee");
            columns.Add("Tiền hàng");
            columns.Add("TotalAmount");
            return columns;

        }


    }
}
