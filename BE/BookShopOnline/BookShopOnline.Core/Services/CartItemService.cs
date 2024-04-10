using AutoMapper;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.UnitOfWork;
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
        IUnitOfWork _unitOfWork;
        public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper, IImageService imageService, IUnitOfWork unitOfWork) : base(cartItemRepository, mapper, imageService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItemDto>> InsertManyServiceAsync(List<CartItem> cartItems)
        {
            _unitOfWork.BeginTransaction();
            var res = await _unitOfWork.CartItems.InsertManyAsync(cartItems);
            if(res == cartItems.Count)
            {
                _unitOfWork.Commit();
            }
            var cartItemsDto = cartItems.Select(item => base.MapEntityToDto(item));
            return cartItemsDto.ToList();
        }
    }
}
