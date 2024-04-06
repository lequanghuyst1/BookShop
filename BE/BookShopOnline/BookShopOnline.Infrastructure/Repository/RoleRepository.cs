using BookShopOnline.Core.Dto;
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
    public class RoleRepository : IRoleRepository
    {
        IDbContext _dbContext;
        public RoleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role?> getByRoleNameAsync(string roleName)
        {
            var procName = "Proc_Role_GetByRoleName";
            var res = await _dbContext.Connection.QuerySingleOrDefaultAsync<Role>(procName, new { RoleName = roleName });
            return res;
        }
    }
}
