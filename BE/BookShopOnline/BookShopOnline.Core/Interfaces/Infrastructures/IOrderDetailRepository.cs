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
        /// <summary>
        /// Hmà lấy ra danh sách chi tiết đơn hàng theo orderId
        /// </summary>
        /// <param name="orderId">mã định danh đơn hàng</param>
        /// <returns>danh sách chi tiết đơn hàng</returns>
        /// Created By: LQHUY(12/04/2024)
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId);
    }
}
