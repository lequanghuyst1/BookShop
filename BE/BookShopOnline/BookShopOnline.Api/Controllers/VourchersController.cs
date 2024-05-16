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
    public class VourchersController : MBaseController<Vourcher, VourcherDto>
    {
        public VourchersController(IVourcherService vourcherService) : base(vourcherService)
        {
        }
    }
}
