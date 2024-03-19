using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TEntity, TDto> : ControllerBase 
    {
        protected IBaseService<TEntity, TDto> _baseService;

        public BaseReadOnlyController(IBaseService<TEntity, TDto> baseService)
        {
            _baseService = baseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _baseService.GetAllAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var res = await _baseService.GetByIdAsync(id);
            return Ok(res);
        }
    }
}
