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

        /// <summary>
        /// Hàm lấy ra danh sách chi tiết đơn hàng theo orderId
        /// </summary>
        /// <param name="orderId">mã định danh đơn hàng</param>
        /// <returns>danh sách chi tiết đơn hàng</returns>
        /// Created By: LQHUY(12/04/2024)
        Task<IEnumerable<OrderDetailDto>> GetByOrderIdAsync(Guid orderId);
    }
}
