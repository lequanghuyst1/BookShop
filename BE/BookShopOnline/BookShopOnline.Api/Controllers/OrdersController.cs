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

        /// <summary>
        /// Api thực hiện lấy ra các bản ghi theo userId
        /// </summary>
        /// <param name="id">mã đingj danh người dùng</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(12/04/2024)
        [HttpGet("GetByUserId/{id}")]
        public async Task<IActionResult> CheckoutAsync(Guid id)
        {
            var res = await _orderService.GetByUserId(id);
            return StatusCode(200, res);
        }

        /// <summary>
        /// Api thực hiện hủy đơn hàng
        /// </summary>
        /// <param name="order">thông tin đơn hàng</param>
        /// <returns>
        /// 1 - hủy thành công
        /// </returns>
        /// Created By: LQHUY(13/04/2024)
        [HttpPut("CancelOrder")]
        public async Task<IActionResult> CancelOrderAsync(Order order)
        {
            var res = await _orderService.CancelOrderAsync(order);
            return Ok(res);
        }
    }

}
