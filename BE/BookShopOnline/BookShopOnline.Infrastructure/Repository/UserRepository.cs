using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckUserIsExist(User user)
        {
            var procName = "Proc_User_CheckUserIsExist";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(procName, new { Email = user.Email });
            if (res != null)
            {
                return true;
            }
            return false;
        }
    }
}
