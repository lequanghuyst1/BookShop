using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IJWTAuthenticationService
    {
        Task<TokenModel> LoginAsync(UserLogin userLogin);
        Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel);
        Task LogoutAsync(string email);
    }
}
