using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Excel;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Model;
using BookShopOnline.Core.Model.Excel;
using BookShopOnline.Core.Model.Revenue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class OrdersController : MBaseController<Order, OrderDto>
    {
        IOrderService _orderService;
        IOrderRepository _orderRepository;
        IOrderExcelService _orderExcelService;
        public OrdersController(IOrderService orderService, IOrderRepository orderRepository, IOrderExcelService orderExcelService) : base(orderService)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _orderExcelService = orderExcelService;
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
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid userId, int? PageSize, int? PageNumber, string? ColumnName, string? Value)
        {
            var res = await _orderRepository.GetByUserId(userId, PageSize, PageNumber, ColumnName, Value);
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
        public async Task<IActionResult> CancelOrderAsync(OrderData orderData)
        {
            var res = await _orderService.CancelOrderAsync(orderData);
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
            var res = await _orderRepository.CalculateTotalSalesPerMonth(year);
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

        [HttpGet("Chart/[action]")]
        public async Task<IActionResult> CalculateTotalAmountByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate, Guid? categoryId)
        {
            var res = await _orderRepository.CalculateTotalAmountByTypeOfTime(typeOfTime, fromDate, toDate, categoryId);
            return Ok(res);
        }

        [HttpGet("Chart/[action]")]
        public async Task<IActionResult> GetRevenueByProduct(int typeOfTime, DateTime fromDate, DateTime toDate, int quantityFilter, Guid? categoryId)
        {
            var res = await _orderRepository.GetRevenueByProduct(typeOfTime, fromDate, toDate, quantityFilter, categoryId);
            return Ok(res);
        }

        [HttpGet("Chart/[action]")]
        public async Task<IActionResult> GetTotalRevenvue()
        {
            var res = await _orderRepository.GetTotalRevenue();
            return Ok(res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate, Guid? categoryId)
        {
            var res = await _orderRepository.GetByTypeOfTime(typeOfTime, fromDate, toDate, categoryId);
            return Ok(res);
        }

        /// <summary>
        /// Xuất tất cả thông tin nhân viên vào file excel
        /// </summary>
        /// <returns>
        /// - Status Code: 200 - link download file excel
        /// - StatusCode: 500 có vấn đề trên service
        /// </returns>
        /// Created by: LQHUY(06/01/2024)
        //[Authorize(Roles = ("Admin"))]
        [HttpPost("Export")]
        public async Task<IActionResult> ExportToExcel([FromBody] ExcelRequest<Order> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Danh sách đơn hàng";
            if (excelRequest.EntityIds?.Count() > 0)
            {
                var bytes = await _orderExcelService.ExportListAsync(excelRequest);
                return File(bytes, contenType, fileName);
            }
            var res = await _orderExcelService.ExportAllAsync(excelRequest);

            return File(res, contenType, fileName);
        }

        //[Authorize(Roles = ("Admin"))]
        [HttpPost("Export/[action]")]
        public IActionResult ExportRevenueByTime(ExcelRequest<Order> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Thống kê doanh thu";

            var res = _orderExcelService.ExportRevenueByTime(excelRequest);

            return File(res, contenType, fileName);
        }

        [HttpPost("Export/[action]")]
        public IActionResult ExportRevenueByProduct(ExcelRequest<RevenueProduct> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Thống kê doanh thu";

            var res = _orderExcelService.ExportRevenueByProduct(excelRequest);

            return File(res, contenType, fileName);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> FiterAsync(Filter filter)
        {
            var res = await _orderRepository.FilterAsync(filter);
            return Ok(res);
        }


    }

}
