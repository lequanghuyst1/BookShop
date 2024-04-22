using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Library;
using BookShopOnline.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{

    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        IOrderService _orderService;
        IOrderRepository _orderRepository;
        public VnPayService(IConfiguration configuration, IOrderService orderService, IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        public string CreatePaymentUrl(PaymentInformation model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = model.OrderId.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            //phiên bản api mà merchant kết nối. Phiên bản hiện tại là : 2.1.0
            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            //Mã API sử dụng, mã cho giao dịch thanh toán là: pay
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            //Mã website của merchant trên hệ thống của VNPAY
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            //Số tiền thanh toán
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            //Là thời gian phát sinh giao dịch định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            //Ngôn ngữ giao diện hiển thị. Hiện tại hỗ trợ Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            //Thông tin mô tả nội dung thanh toán 
            pay.AddRequestData("vnp_OrderInfo", model.TransactionContent);
            //Mã danh mục hàng hóa. Mỗi hàng hóa sẽ thuộc một nhóm danh mục do VNPAY quy định.
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán.
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            //Mã tham chiếu của giao dịch tại hệ thống của merchant.
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public async Task<PaymentResponse> PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            //Nếu thanh toán thành công thì cập nhật lại trạng thái đơn hàng
            if(response.Success == true)
            {
                var orderId = new Guid(response.OrderId);
                var order = await _orderRepository.GetByIdAsync(orderId);
                order.PaymentStatus = Enums.PaymentStatus.PAID;
                order.OrderStatus = Enums.OrderStatus.WAIT_FOR_CONFIRMATION;
                await _orderRepository.UpdateAsync(orderId, order);
                return response;
            }
            return response;
        }
    }
}
