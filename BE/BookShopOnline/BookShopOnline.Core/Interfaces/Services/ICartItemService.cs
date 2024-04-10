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

    }
}
