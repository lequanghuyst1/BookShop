﻿using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services.Base
{
    public abstract class BaseService<TEntity, TDto> : BaseReadOnlyService<TEntity, TDto>, IBaseService<TEntity, TDto>
    {
        public BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {

        }

        public async Task<int> DeleteManyServiceAsync(List<Guid> ids)
        {

            var res = await _baseRepository.DeleteManyAsync(ids);
            return res;
        }

        public async Task<int> DeleteServiceAsync(Guid id)
        {
            var res = await _baseRepository.DeleteAsync(id);
            return res;
        }

        public async Task<int> InsertHaveImageServiceAsync(IFormFile? image, string dataJson)
        {
            var res = await _baseRepository.InsertHaveImageAsync(image, dataJson);
            return res;
        }

        public async Task<int> InsertServiceAsync(TEntity entity)
        {
            await ValidateBeforeInsert(entity);
            var res = await _baseRepository.InsertAsync(entity);
            return res;
        }

        public async Task<int> UpdateHaveImageServiceAsync(IFormFile? imageFile, string dataJson)
        {
            var res = await _baseRepository.UpdateHaveImageAsync(imageFile, dataJson);
            return res;
        }

        public async Task<int> UpdateServiceAsync(TEntity entity, Guid id)
        {
            var res = await _baseRepository.UpdateAsync(entity, id);
            return res;
        }

        public virtual async Task ValidateBeforeInsert(TEntity entity)
        {
            await Task.CompletedTask;
        }
    }
}
