using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IJWTAuthenticationRepository
    {
        /// <summary>
        /// Lấy ra chuỗi refresh token
        /// </summary>
        /// <returns>
        /// chuỗi refresh token
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        string GenerateRefreshToken();

        /// <summary>
        /// Tạo chuỗi token jwt
        /// </summary>
        /// <param name="authClaims">Danh sách chứa thông tin claim</param>
        /// <returns>
        /// chuỗi jwt token
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        JwtSecurityToken CreateToken(List<Claim> authClaims);

        /// <summary>
        /// Hàm trả về 1 đối tượng kiểu ClaimsPrincipal
        /// </summary>
        /// <param name="accessToken">Chuỗi jwt token</param>
        /// <returns>
        /// chuỗi ClaimsPrincipal
        /// </returns>
        /// Created By: LQHUY(06/04/2024)
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
