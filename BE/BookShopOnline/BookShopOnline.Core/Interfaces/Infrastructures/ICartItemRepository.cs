using BookShopOnline.Core.Dto;
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
        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo cartId
        /// </summary>
        /// <param name="cartId">mã định danh giỏ hàng</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(11/04/2024)
        Task<IEnumerable<CartItem>> GetByCartIdAsync(Guid cartId);

    }
}
