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

        /// <summary>
        /// Hàm lấy ra tổng số tiền của hóa đơn theo từng tháng của 1 năm
        /// </summary>
        /// <param name="year">năm cần lấy</param>
        /// <returns>
        /// object - chứa tháng và tổng tiền tháng đó
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        Task<IEnumerable<object>> CalculateTotalSalesPerMonth(int year);

        /// <summary>
        /// Hàm lấy ra tổng đơn hàng mới theo điều kiện trong vòng 24h
        /// </summary>
        /// <param name="filedCondition">trường cần tìm kiếm</param>
        /// <returns>
        /// tổng số đơn
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        Task<int> GetTotalOrderByConditionIn24Hour(string? filedCondition);
    }
}
