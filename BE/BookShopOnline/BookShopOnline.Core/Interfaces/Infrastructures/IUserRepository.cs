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

        Task<User?> FindByEmailAsync(string email);

        Task<User?> GetUserByToken(string token);

    }

}
