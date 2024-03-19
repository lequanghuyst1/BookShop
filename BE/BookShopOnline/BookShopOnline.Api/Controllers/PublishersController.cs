using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Publisher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class PublishersController : MBaseController<Publisher, PublisherDto>
    {
        public PublishersController(IPublisherService publisherService) : base(publisherService)
        {
        }
    }
}
