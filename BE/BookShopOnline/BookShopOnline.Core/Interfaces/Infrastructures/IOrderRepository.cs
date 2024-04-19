using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        /// <summary>
        /// Lấy ra danh sách các bản ghi theo userId
        /// </summary>
        /// <param name="userId">mã định danh người dùng</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(12/02/2024)
        Task<IEnumerable<Order>> GetByUserId(Guid userId);

        /// <summary>
        /// Hàm thực hiện xác nhận tất cả các đơn hàng chưa được xác nhận
        /// </summary>
        /// <returns>
        /// số dòng được cập nhật
        /// </returns>
        /// Created By: LQHUY(19/02/2024)
        Task<int> ConfirmAllAsync();
    }
}
