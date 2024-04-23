﻿using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
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
        IOrderRepository _orderRepository;
        public OrdersController(IOrderService orderService, IOrderRepository orderRepository) : base(orderService)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
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

        /// <summary>
        /// Api thực hiện xác nhận tất cả các đơn hàng
        /// </summary>
        /// <returns>
        /// số dòng thực hiện thành công
        /// </returns>
        /// Created By: LQHUY(13/04/2024)
        [HttpPut("ConfirmAll")]
        public async Task<IActionResult> ConfirmAllAsync()
        {
            var res = await _orderService.ConfirmAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Api thực hiện lấy tổng doanh thu theo từng tháng trong 1 năm
        /// </summary>
        /// <param name="year">năm cần lọc</param>
        /// <returns>
        /// object - chứa tháng và doanh thu tháng đó
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        [HttpGet("CalculateTotalSalesPerMonth")]
        public async Task<IActionResult> CalculateTotalSalesPerMonth(int year)
        {
            var res =await _orderRepository.CalculateTotalSalesPerMonth(year);
            return Ok(res);
        }

        /// <summary>
        /// Api thực hiện lấy tổng số đơn hàng mới trong 24h theo điều kiện
        /// </summary>
        /// <param name="conditionField">trường điều kiện</param>
        /// <returns>
        /// tổng số đơn hàng
        /// </returns>
        /// Created By: LQHUY(23/04/2024)
        [HttpGet("TotalOrderBy24Hour")]
        public async Task<IActionResult> GetCountOrderBy24Hours(string? conditionField)
        {
            var res = await _orderRepository.GetTotalOrderByConditionIn24Hour(conditionField);
            return Ok(res);
        }
    }

}
