using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Kiểm tra tài khoản đã tồn tại trong hệ thống chưa
        /// </summary>
        /// <param name="user">thông tin tài khoản</param>
        /// <returns>
        /// true - đã tồn tại
        /// false - chưa tồn tại
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<bool> CheckUserIsExist(string email);

        /// <summary>
        /// Kiểm tra thông tin người dùng có xác thực không
        /// </summary>
        /// <param name="user">thông tin tài khoản</param>
        /// <returns>
        /// true - xác thực
        /// false - không xác thực
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<User?> FindUserByEmailAndPassword(UserLogin userLogin);

        /// <summary>
        /// Lấy ra thông tin người dùng theo Email
        /// </summary>
        /// <param name="email">email đăng nh</param>
        /// <returns>
        /// thông tin người dùng
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<User?> FindByEmailAsync(string email);

        /// <summary>
        /// Lấy ra thông tin người dùng theo token
        /// </summary>
        /// <param name="token">thông tin token</param>
        /// <returns>
        /// thông tin người dùng
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<User?> GetUserByToken(string token);

        /// <summary>
        /// Hàm lấy tổng số khách hàng mới trong 24h qua
        /// </summary>
        /// <returns>
        /// tổng sổ sách hàng mới
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        Task<int> GetTotalUserNewBy24Hours();
    }

}
