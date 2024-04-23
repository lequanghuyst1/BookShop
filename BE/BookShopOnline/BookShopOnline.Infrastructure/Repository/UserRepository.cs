using BookShopOnline.Core.Dto.User;
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

        public async Task<bool> CheckUserIsExist(string email)
        {
            var procName = "Proc_User_CheckUserIsExist";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(procName, new { Email = email });
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var procName = "Proc_User_CheckUserIsExist";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(procName, new { Email = email });
            return res;
        }

        public async Task<User?> FindUserByEmailAndPassword(UserLogin userLogin)
        {
            var procName = "Proc_User_FindUserByEmailAndPassword";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(procName, userLogin);
            return res;
        }

        public async Task<int> GetTotalUserNewBy24Hours()
        {
            string sqlCommand = @"SELECT COUNT(*) FROM view_user WHERE CreatedDate >= NOW() - INTERVAL 24 HOUR AND RoleName = 'User'";
            var result = await _dbContext.Connection.ExecuteScalarAsync<int>(sqlCommand);
            return result;
        }

        public async Task<User?> GetUserByToken(string token)
        {
            var sql = "Select * from User where RefreshToken = @RefreshToken";
            var param = new DynamicParameters();
            param.Add("@RefreshToken", token);
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(sql, param);
            return res;
        }
    }
}
