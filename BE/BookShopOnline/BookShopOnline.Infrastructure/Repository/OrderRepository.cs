using BookShopOnline.Core.Dto.Order;
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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> ConfirmAllAsync()
        {
            var procName = "Proc_Order_ConfirmAll";
            var res = await _dbContext.Connection.ExecuteAsync(procName,transaction: _dbContext.Transaction);
            return res;
        }

        public async Task<IEnumerable<Order>> GetByUserId(Guid userId)
        {
            var procName = "Proc_Order_GetByUserId";
            var res = await _dbContext.Connection.QueryAsync<Order>(procName, new { UserId = userId }, _dbContext.Transaction);
            return res;
        }
    }
}
  