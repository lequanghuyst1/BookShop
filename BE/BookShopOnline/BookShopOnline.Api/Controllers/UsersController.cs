using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class UsersController : MBaseController<User, UserDto>
    {
        IUserService _userService;
        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Api thực hiện tạo tài khoản cho admin
        /// </summary>
        /// <param name="userRegister">thông tin người dùng</param>
        /// <returns>
        /// Status 201 - thành công
        /// res - số dòng được thêm
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(UserRegister userRegister)
        {
            var res = await _userService.RegisterAdminServiceAsync(userRegister);
            return StatusCode(201, res);
        }

        /// <summary>
        /// Api thực hiện tạo tài khoản cho user
        /// </summary>
        /// <param name="userRegister">thông tin người dùng</param>
        /// <returns>
        /// Status 201 - thành công
        /// res - số dòng được thêm
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync(UserRegister userRegister)
        {
            var res = await _userService.RegisterUserServiceAsync(userRegister);
            return StatusCode(201,res);
        }
    }
}
