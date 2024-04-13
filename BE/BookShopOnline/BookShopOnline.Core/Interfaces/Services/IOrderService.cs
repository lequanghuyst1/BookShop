
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
    public interface IOrderService : IBaseService<Order, OrderDto>
    {
        /// <summary>
        /// Hàm thực hiện thanh toán
        /// </summary>
        /// <param name="orderData">thực thể OrderData</param>
        /// <returns>
        /// 1 - thanh toán thành công 
        /// 0 - thanh toán thất bại</returns>
        /// Created By: LQHUY(12/02/2024)
        Task<int> CheckoutServiceAsync(OrderData orderData);
        /// <summary>
        /// Lấy ra danh sách các bản ghi theo userId
        /// </summary>
        /// <param name="userId">mã định danh người dùng</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(12/02/2024)
        Task<IEnumerable<OrderDto>> GetByUserId(Guid userId);

        /// <summary>
        /// Hàm hủy đơn hàng
        /// </summary>
        /// <param name="order">thông tin đơn hàng</param>
        /// <returns>
        /// 1 - hủy thành công
        /// </returns>
        /// Created By: LQHUY(13/02/2024)
        Task<int> CancelOrderAsync(Order order);
    }
}
