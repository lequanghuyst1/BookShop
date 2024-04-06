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
        Task<bool> CheckUserIsExist(User user);
    }
    
}
