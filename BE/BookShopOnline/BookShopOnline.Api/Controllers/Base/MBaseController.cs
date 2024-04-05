using BookShopOnline.Core.Entitites;
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
        public async Task<IActionResult> InsertAsync([FromForm] string dataJson,  IFormFile? imageFile)
        {
            var res = await _baseService.InsertServiceAsync(dataJson, imageFile);
            return StatusCode(201, res);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] string dataJson, IFormFile? imageFile)
        {
            var res = await _baseService.UpdateServiceAsync(id, dataJson, imageFile);
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
