using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VnPaysController : ControllerBase
    {
        IVnPayService _vnPayService;

        public VnPaysController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl(PaymentInformation model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return StatusCode(201, url);
        }

        [HttpGet("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecute(Request.Query);
            if(response!= null)
            {
                return Redirect("http://localhost:8080/customer/order");
            }
            return StatusCode(200, response);
        }
    }
}
