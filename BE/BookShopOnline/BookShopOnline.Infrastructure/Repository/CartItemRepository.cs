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
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {

        public CartItemRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> InsertManyAsync(List<CartItem> cartItems)
        {
            var procName = "Proc_CartItem_InsertMany";
            var res = await _dbContext.Connection.ExecuteAsync(procName, cartItems, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
