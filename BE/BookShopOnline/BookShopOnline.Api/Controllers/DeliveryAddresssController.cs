using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.DeliveryAddress;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    /// <summary>
    /// DeliveryAddresssController
    /// </summary>
    /// Created By: LQHUY(08/04/1024)
    [ApiController]
    public class DeliveryAddresssController : MBaseController<DeliveryAddress, DeliveryAddressDto>
    {
        IDeliveryAddressService _deliveryAddressService;
        public DeliveryAddresssController(IDeliveryAddressService deliveryAddressService) : base(deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(Guid userId)
        {
            var res = await _deliveryAddressService.GetByUserIdServiceAsync(userId);
            return Ok(res);
        }
    }
}
