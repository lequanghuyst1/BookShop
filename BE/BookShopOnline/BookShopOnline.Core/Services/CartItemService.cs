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

        public async Task<IEnumerable<CartItemDto>> GetByCartIdServiceAsync(Guid cartId)
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

            //lấy ra giỏ hàng của người dùng theo cartId
            var cartItems = await _unitOfWork.CartItems.GetByCartIdAsync(cartItem.CartId);

            //kiểm tra xem sản phẩm được thêm đã có trong giỏ hàng của tài khoản đó hay chưa
            var cartItemExit = cartItems.FirstOrDefault(item => item.BookId == cartItem.BookId);
            var book = await _unitOfWork.Book.GetByIdAsync(cartItem.BookId);

            //Nếu có thì cập nhật lại số lượng
            if (cartItemExit != null)
            {
                cartItem.Quantity = cartItem.Quantity + cartItemExit.Quantity;
                if(cartItem.Quantity > book?.QuantityInStock)
                {
                    cartItem.Quantity = book?.QuantityInStock;
                }
                var result = await _unitOfWork.CartItems.UpdateAsync(cartItemExit.CartItemId, cartItem);
                if (result > 0)
                {
                    _unitOfWork.Commit();
                    return result;
                }
                _unitOfWork.Rollback();
                return 0;
            }

            //Chưa có thì thêm mới
            cartItem.CartItemId = Guid.NewGuid();
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

            //kiểm tra xem sản phẩm được thêm đã có trong giỏ hàng của tài khoản đó hay chưa
            var cartItemExit = await _unitOfWork.CartItems.GetByIdAsync(cartItem.CartItemId);
            var book = await _unitOfWork.Book.GetByIdAsync(cartItem.BookId);

            //Nếu có thì cập nhật
            if (cartItemExit != null)
            {
                if (cartItem.Quantity > book?.QuantityInStock)
                {
                    cartItem.Quantity = book?.QuantityInStock;
                }
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
