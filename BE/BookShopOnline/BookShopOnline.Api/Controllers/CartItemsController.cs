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
        ICartItemService _cartItemService;
        public CartItemsController(ICartItemService cartItemService) : base(cartItemService)
        {
            _cartItemService = cartItemService;
        }
        [HttpGet("GetByCartId/{cartId}")]
        public async Task<IActionResult> GetByCartIdAsync(string cartId)
        {
            var res = await _cartItemService.GetByCartIdServiceAsync(cartId);
            return Ok(res);
        }

    }
}
