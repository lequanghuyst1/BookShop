using AutoMapper;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.UnitOfWork;
using BookShopOnline.Core.Services.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

        public async Task<IEnumerable<CartItemDto>> GetByCartIdServiceAsync(string cartId)
        {
            _unitOfWork.BeginTransaction();
            var cartsItem = await _unitOfWork.CartItems.GetByCartIdAsync(cartId);
            var cartsItemDto = cartsItem.Select(item => base.MapEntityToDto(item));
            return cartsItemDto;
        }

        public async Task<List<CartItemDto>> InsertManyServiceAsync(List<CartItem> cartItems)
        {
            _unitOfWork.BeginTransaction();
            var res = await _unitOfWork.CartItems.InsertManyAsync(cartItems);
            if (res == cartItems.Count)
            {
                _unitOfWork.Commit();
            }
            var cartItemsDto = cartItems.Select(item => base.MapEntityToDto(item));
            return cartItemsDto.ToList();
        }

        public override async Task<int> InsertServiceAsync(string dataJson, IFormFile? imageFile)
        {
            _unitOfWork.BeginTransaction();
            var cartItem = JsonConvert.DeserializeObject<CartItem>(dataJson);
            var cartItemExit = await _unitOfWork.CartItems.CheckBookExistInCartItemAsync(cartItem.BookId);
            //kiểm tra xem sản phẩm được thêm đã có trong giỏ hàng hay chưa

            //Nếu có thì cập nhật lại số lượng
            if (cartItemExit != null)
            {
                cartItem.Quantity = cartItem.Quantity + cartItemExit.Quantity;
                var result = await _unitOfWork.CartItems.UpdateAsync(cartItem.CartId, cartItem);
                if (result > 0)
                {
                    _unitOfWork.Commit();
                    return result;
                }
                _unitOfWork.Rollback();
                return 0;
            }
            cartItem.CartItemId = Guid.NewGuid();
            //Chưa có thì thêm mới
            var res = await _unitOfWork.CartItems.InsertAsync(cartItem);
            if (res > 0)
            {
                _unitOfWork.Commit();
                return res;
            }

            _unitOfWork.Rollback();
            return res;
        }

        public override async Task<int> UpdateServiceAsync(Guid id, string dataJson, IFormFile? imageFile)
        {
            _unitOfWork.BeginTransaction();
            var cartItem = JsonConvert.DeserializeObject<CartItem>(dataJson);
            //kiểm tra xem sản phẩm được thêm đã có trong giỏ hàng hay chưa
            var cartItemExit = await _unitOfWork.CartItems.CheckBookExistInCartItemAsync(cartItem.BookId);

            //Nếu có thì cập nhật
            if (cartItemExit != null)
            {
                var result = await _unitOfWork.CartItems.UpdateAsync(id, cartItem);
                if (result > 0)
                {
                    _unitOfWork.Commit();
                    return result;
                }
                _unitOfWork.Rollback();
            }

            _unitOfWork.Rollback();
            return 0;
        }
    }
}
