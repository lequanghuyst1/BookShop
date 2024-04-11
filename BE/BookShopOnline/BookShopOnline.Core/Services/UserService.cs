using AutoMapper;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        IRoleRepository _roleRepository;
        IUserRepository _userRepository;
        ICartRepository _cartRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IImageService imageService, IRoleRepository roleRepository, ICartRepository cartRepository) : base(userRepository, mapper, imageService)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public override async Task<int> UpdateServiceAsync(Guid id, string dataJson, IFormFile? imageFile)
        {
            var user = JsonConvert.DeserializeObject<User>(dataJson);

            //Lấy thông tin người dùng trong Db
            var userDb = await _userRepository.GetByIdAsync(id);

            user.Password = userDb.Password;


            //Nếu thay đổi mật khẩu thì kiểm tra xem mật khẩu hiện tại có khớp với mật khẩu trong db hay không
            if (!string.IsNullOrEmpty(user.CurrentPassword))
            {
                var currentPasswordMD5 = CalculateMD5Hash(user.CurrentPassword);
                //không khớp ném ra lỗi
                if (currentPasswordMD5 != userDb.Password)
                {
                    errors.Add("CurrentPassword", new string[] { "Mật khẩu hiện tại không chính xác." });
                    throw new ValidateException("Mật khẩu hiện tại không chính xác.", errors);
                }
                //hợp lệ thì gán lại giá trị password
                else
                {
                    user.Password = CalculateMD5Hash(user.NewPassword);
                }
            }

            //Cập nhật thông tin
            var res = await _userRepository.UpdateAsync(id, user);
            return res;
        }
        private string CalculateMD5Hash(string input)
        {
            // Kiểm tra xem input có phải là null không
            //if (input == null)
            //{
            //    return input;
            //}
            // Tạo một đối tượng MD5
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi input thành một mảng byte và tính toán giá trị băm
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành một chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<int> RegisterAdminServiceAsync(UserRegister userRegister)
        {
            var role = await _roleRepository.getByRoleNameAsync("Admin");
            userRegister.RoleId = role.RoleId;
            userRegister.UserId = Guid.NewGuid();
            var user = _mapper.Map<User>(userRegister);
            await ValidateBeforeInsert(user);

            var res = await _userRepository.InsertAsync(user);
            return res;
        }

        public async Task<int> RegisterUserServiceAsync(UserRegister userRegister)
        {
            var role = await _roleRepository.getByRoleNameAsync("User");

            var newId = Guid.NewGuid();
            userRegister.RoleId = role.RoleId;
            userRegister.UserId = newId;

            var user = _mapper.Map<User>(userRegister);
            await ValidateBeforeInsert(user);

            var res = await _userRepository.InsertAsync(user);
            if (res > 0)
            {
                var cart = new Cart();
                cart.UserId = user.UserId;
                cart.CreatedBy = user.Fullname;
                await _cartRepository.InsertAsync(cart);
                return res;
            }
            return res;
        }
        public override async Task ValidateBeforeInsert(User user)
        {
            if (await _userRepository.CheckUserIsExist(user.Email))
            {
                errors.Add("Email", new string[] { "Email này đã tồn tại trong hệ thống" });
                throw new ValidateException("Email này đã tồn tại trong hệ thống", errors);
            }
        }
        public override Task ValidateBeforeUpdate(User user)
        {
            return base.ValidateBeforeUpdate(user);
        }
    }
}
