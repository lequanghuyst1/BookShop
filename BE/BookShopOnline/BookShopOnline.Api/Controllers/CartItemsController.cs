using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class CartItemsController : MBaseController<CartItem, CartItemDto>
    {
        public CartItemsController(ICartItemService cartItemService) : base(cartItemService)
        {
        }
    }
}
