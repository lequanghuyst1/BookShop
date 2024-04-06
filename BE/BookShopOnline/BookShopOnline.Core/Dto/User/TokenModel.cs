using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.User
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Thời gian hết hạn token
        /// </summary>
        public DateTime? Expiration { get; set; }
        public UserDto? UserDto { get; set; }
    }
}
