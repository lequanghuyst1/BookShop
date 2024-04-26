using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User, UserDto>
    {
        /// <summary>
        /// Tạo mới một tài khoản cho user
        /// </summary>
        /// <param name="userRegister">thông tin người dùng</param>
        /// <returns>
        /// 1 - thêm thành công
        /// 0 - thêm thất bại
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<int> RegisterUserServiceAsync(UserRegister userRegister);

        /// <summary>
        /// Tạo mới một tài khoản cho admin
        /// </summary>
        /// <param name="userRegister">thông tin người dùng</param>
        /// <returns>
        /// 1 - thêm thành công
        /// 0 - thêm thất bại
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        Task<int> RegisterAdminServiceAsync(UserRegister userRegister);

        Task<PagingEntity<UserDto>> GetFilterPagingByRoleName(UserFilter userFilter);

    }
}
