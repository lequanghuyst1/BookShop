using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class UsersController : MBaseController<User, UserDto>
    {
        IUserService _userService;
        IJWTAuthenticationService _jwtAuthenticationService;
        IUserRepository _userRepository;
        public UsersController(IUserService userService, IJWTAuthenticationService jwtAuthenticationService, IUserRepository userRepository) : base(userService)
        {
            _userService = userService;
            _jwtAuthenticationService = jwtAuthenticationService;
            _userRepository = userRepository;
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
            return StatusCode(201, res);
        }

        /// <summary>
        /// Api thực hiện đăng nhập người dùng
        /// </summary>
        /// <param name="userLogin">thông tin đăng nhập</param>
        /// <returns>
        /// Thông tin đăng nhập người dùng
        /// </returns>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserLogin userLogin)
        {
            var res = await _jwtAuthenticationService.LoginAsync(userLogin);
            return StatusCode(201, res);
        }

        /// <summary>
        /// Api thực hiện lấy token mới khi token cũ hết hạn
        /// </summary>
        /// <param name="tokenModel">thông tin token cũ</param>
        /// <returns>
        /// Thông tin đăng nhập người dùng mới
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewTokenAsync(TokenModel tokenModel)
        {
            var res = await _jwtAuthenticationService.RefreshTokenAsync(tokenModel);
            return StatusCode(201, res);
        }

        /// <summary>
        /// Api thực hiện đăng xuất
        /// </summary>
        /// <param name="email">email người dùng</param>
        /// <returns></returns>
        /// Created By: LQHUY(06/04/2024)
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] string email)
        {
            await _jwtAuthenticationService.LogoutAsync(email);
            return StatusCode(201);
        }

        /// <summary>
        /// Api thực hiện lấy tổng số khách hàng mới trong 24h quá
        /// </summary>
        /// <returns>
        /// tổng số khách hàn
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        [HttpGet("TotalNewCustomer")]
        public async Task<IActionResult> TotalNewCustomer()
        {
            var res = await _userRepository.GetTotalUserNewBy24Hours();
            return Ok(res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetFilterPagingByRoleName(UserFilter userFilter)
        {
            var res = await _userRepository.GetFilterPagingByRoleName(userFilter);
            return Ok(res);
        }
    }
}
