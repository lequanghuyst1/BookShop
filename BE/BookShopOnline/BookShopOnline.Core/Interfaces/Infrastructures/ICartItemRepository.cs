using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<int> InsertManyAsync(List<CartItem> cartItems);
        Task<CartItem> CheckBookExistInCartItemAsync(Guid bookId);
    }
}
