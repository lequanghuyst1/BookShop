using BookShopOnline.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using static Dapper.SqlMapper;

namespace BookShopOnline.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        IDbContext _dbContext;

        public CartRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteAsync(string cartId)
        {
            var sql = $"DELETE FROM Cart WHERE CartId = @CartId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("CartId", cartId);
            var res = await _dbContext.Connection.ExecuteAsync(sql, dynamicParameters);
            return res;
        }

        public async Task<int> InsertAsync(Cart cart)
        {
            var procName = "Proc_Cart_Insert";
            var res = await _dbContext.Connection.ExecuteAsync(procName,cart);
            return res;
        }
    }
}
