using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface ICartItemService : IBaseService<CartItem, CartItemDto>
    {
        Task<List<CartItemDto>> InsertManyServiceAsync(List<CartItem> cartItems);

        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo cartId
        /// </summary>
        /// <param name="cartId">mã định danh giỏ hàng</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(11/04/2024)
        Task<IEnumerable<CartItemDto>> GetByCartIdServiceAsync(Guid cartId);

    }
}
