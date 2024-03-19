using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Category;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class CategoriesController : MBaseController<Category, CategoryDto>
    {
        public CategoriesController(ICategoryService categoryService) : base(categoryService)
        {
        }
    }
}
