using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Vourcher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class VouchersController : MBaseController<Voucher, VoucherDto>
    {
        IVoucherService _voucherService;
        public VouchersController(IVoucherService voucherService) : base(voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet("ApplyVoucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode, decimal totalProductCost)
        {
            var res = await _voucherService.ApplyVoucher(voucherCode, totalProductCost);
            return Ok(res);
        }
    }
}
