using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers.Base
{
    [ApiController]
    public class MBaseController<TEntity, TDto> : BaseReadOnlyController<TEntity, TDto>
    {
        public MBaseController(IBaseService<TEntity, TDto> baseService) : base(baseService)
        {

        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(TEntity entity)
        {
            var res = await _baseService.InsertServiceAsync(entity);
            return StatusCode(201, res);
        }

        [HttpPost("HaveImage")]
        public async Task<IActionResult> InsertHaveImageAsync([FromForm] string dataJson, IFormFile? imageFile)
        {
            var res = await _baseService.InsertHaveImageServiceAsync(imageFile, dataJson);
            return StatusCode(201, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(TEntity entity, Guid id)
        {
            var res = await _baseService.UpdateServiceAsync(entity, id);
            return Ok(res);
        }

        [HttpPut("HaveImage")]
        public async Task<IActionResult> UpdateHaveImageAsync([FromForm] string dataJson, IFormFile? imageFile)
        {
            var res = await _baseService.UpdateHaveImageServiceAsync(imageFile, dataJson);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var res = await _baseService.DeleteServiceAsync(id);
            return Ok(res);
        }
        [HttpDelete("DeleteMany")]
        public async Task<IActionResult> DeleteManyAsync([FromBody] List<Guid> ids)
        {
            var res = await _baseService.DeleteManyServiceAsync(ids);
            return Ok(res);
        }
    }
}
