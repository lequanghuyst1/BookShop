using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetByIdsAsync(List<Guid> ids)
        {
            var res = await _baseService.GetByIdsAsync(ids);
            return Ok(res);
        }
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var res = await _baseService.GetNewCodeAsync();
            return Ok(res);
        }
        [HttpGet("FilterPaging")]
        public async Task<IActionResult> GetFilterPagingAsync(string? searchString, int pageSize, int pageNumber)
        {
            var res = await _baseService.GetFilterPagingAsync(searchString, pageSize, pageNumber);
            return Ok(res);
        }

        /// <summary>
        /// Api lấy 1 bản ghi theo slug của nó
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        /// Created By: LQHUY(13/04/2024)
        [HttpGet("GetBySlug/{slug}")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            var res = await _baseService.GetBySlugAsync(slug);
            return Ok(res);
        }
    }
}
