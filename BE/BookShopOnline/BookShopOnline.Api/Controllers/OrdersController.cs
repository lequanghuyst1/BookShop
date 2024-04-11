using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class OrdersController : MBaseController<Order, OrderDto>
    {
        IOrderService _orderService;
        public OrdersController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("Checkout")]
        public async Task<IActionResult> CheckoutAsync(OrderData orderData)
        {
            var res = await _orderService.CheckoutServiceAsync(orderData);
            return StatusCode(201, res);
        }
    }
}
