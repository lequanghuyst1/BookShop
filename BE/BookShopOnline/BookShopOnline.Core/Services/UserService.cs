using AutoMapper;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        IRoleRepository _roleRepository;
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IImageService imageService, IRoleRepository roleRepository) : base(userRepository, mapper, imageService)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
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
            userRegister.RoleId = role.RoleId;
            userRegister.UserId = Guid.NewGuid();
            var user = _mapper.Map<User>(userRegister);

            await ValidateBeforeInsert(user);

            var res = await _userRepository.InsertAsync(user);
            return res;
        }
        public override async Task ValidateBeforeInsert(User user)
        {
            if (await _userRepository.CheckUserIsExist(user))
            {
                errors.Add("Email", new string[] { "Email này đã tồn tại trong hệ thống" });
                throw new ValidateException("Email này đã tồn tại trong hệ thống", errors);
            }
        }
    }
}
