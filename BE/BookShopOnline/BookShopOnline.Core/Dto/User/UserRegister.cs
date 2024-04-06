using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto
{
    public class UserRegister
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
