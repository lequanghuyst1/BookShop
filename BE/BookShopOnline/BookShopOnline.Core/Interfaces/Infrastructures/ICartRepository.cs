using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface ICartRepository
    {
        Task<int> InsertAsync(Cart cart);
        Task<int> DeleteAsync(string cartId);
    }
}
