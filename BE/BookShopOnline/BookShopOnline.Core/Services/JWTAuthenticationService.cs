using AutoMapper;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class JWTAuthenticationService : IJWTAuthenticationService
    {
        IUserRepository _userRepository;
        IJWTAuthenticationRepository _jwtRepository;
        Dictionary<string, string[]> errors;
        IConfiguration _config;
        IMapper _mapper;
        ICacheRepository _cacheRepository;
        public JWTAuthenticationService(IUserRepository userRepository, IJWTAuthenticationRepository jwtRepository, IConfiguration configuration, IMapper mapper, ICacheRepository cacheRepository)
        {
            _userRepository = userRepository;
            errors = new Dictionary<string, string[]>();
            _jwtRepository = jwtRepository;
            _config = configuration;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<TokenModel> LoginAsync(UserLogin userLogin)
        {
            var user = await _userRepository.FindUserByEmailAndPassword(userLogin);
            if(user == null)
            {
                errors.Add("Account", new string[] { "Thông tin tài khoản không chính xác." });
                throw new ValidateException("Thông tin tài khoản không chính xác.", errors);
            }
            // Tạo danh sách các Clamis cho xác thực
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            //tạo token và refreshToken
            var token = _jwtRepository.CreateToken(authClaims);
            var refreshToken =  _jwtRepository.GenerateRefreshToken();

            //Lấy giá trị hết hạn cho refreshToken
            var refreshTokenValidityInDays = int.Parse(_config["Jwt:RefreshTokenValidityInDays"]);

            //cập nhật thông tin user

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            await _userRepository.UpdateAsync(user.UserId, user);

            var userDto = _mapper.Map<UserDto>(user);
            var TokenValidityInMinutes = int.Parse(_config["Jwt:TokenValidityInMinutes"]);

            _cacheRepository.AddToCache<string>(new JwtSecurityTokenHandler().WriteToken(token), user.Email, TimeSpan.FromMinutes(TokenValidityInMinutes));

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken,
                Expiration = DateTime.Now.AddMinutes(TokenValidityInMinutes),
                UserDto = userDto,
            };
        }

        public async Task LogoutAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                errors.Add("Account", new string[] { "Thông tin tài khoản không chính xác." });
                throw new ValidateException("Thông tin tài khoản không chính xác.", errors);
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userRepository.UpdateAsync(user.UserId, user);
        }

        public async Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal= _jwtRepository.GetPrincipalFromExpiredToken(accessToken);

            var email = principal.Identity.Name;

            var user = await _userRepository.GetUserByToken(refreshToken);

            var newAccessToken = _jwtRepository.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _jwtRepository.GenerateRefreshToken();

            var TokenValidityInMinutes = int.Parse(_config["Jwt:TokenValidityInMinutes"]);

            user.RefreshToken = newRefreshToken;

            await _userRepository.UpdateAsync(user.UserId, user);
            var userDto = _mapper.Map<UserDto>(user);

            _cacheRepository.AddToCache<string>(new JwtSecurityTokenHandler().WriteToken(newAccessToken), user.Email, TimeSpan.FromMinutes(TokenValidityInMinutes));
            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = user.RefreshToken,
                Expiration = DateTime.Now.AddMinutes(TokenValidityInMinutes),
                UserDto = userDto,
            };
        }


    }
}
