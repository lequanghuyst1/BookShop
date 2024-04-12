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
    public class OrderDetailsController : MBaseController<OrderDetail, OrderDetailDto>
    {
        IOrderDetailService _orderDetailService;
        public OrderDetailsController(IOrderDetailService orderDetailService) : base(orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("GetByOrderId/{id}")]
        public async Task<IActionResult> GetByOrderIdAsync(Guid id)
        {
            var res = await _orderDetailService.GetByOrderIdAsync(id);
            return Ok(res);
        }
    }
}
