using AutoMapper;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class CartItemService : BaseService<CartItem, CartItemDto>, ICartItemService
    {
        public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper, IImageService imageService) : base(cartItemRepository, mapper, imageService)
        {

        }
    }
}
