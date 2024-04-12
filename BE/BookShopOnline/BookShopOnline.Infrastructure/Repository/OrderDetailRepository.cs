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
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId)
        {
            var sqlCommand = "Select * From view_orderdetail where OrderId = @OrderId";
            var param = new DynamicParameters();
            param.Add("@OrderId", orderId);
            var res = await _dbContext.Connection.QueryAsync<OrderDetail>(sqlCommand, param,_dbContext.Transaction);
            return res;
        }

        public Task<int> InsertManyAsync(List<OrderDetail> orderDetails)
        {
            throw new NotImplementedException();

        }
    }
}
