﻿using BookShopOnline.Core.Interfaces.Services.Base;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(TEntity entity, Guid id)
        {
            var res = await _baseService.UpdateServiceAsync(entity, id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var res = await _baseService.DeleteServiceAsync(id);
            return Ok(res);
        }
    }
}
