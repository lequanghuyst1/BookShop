using AutoMapper;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services.Base
{
    public abstract class BaseService<TEntity, TDto> : BaseReadOnlyService<TEntity, TDto>, IBaseService<TEntity, TDto>
    {
        IImageService _imageService;
        protected Dictionary<string, string[]> errors;
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IImageService imageService) : base(baseRepository, mapper)
        {
            _imageService = imageService;
            errors = new Dictionary<string, string[]>();

        }

        public async Task<int> DeleteManyServiceAsync(List<Guid> ids)
        {

            var res = await _baseRepository.DeleteManyAsync(ids);
            return res;
        }

        public async Task<int> DeleteServiceAsync(Guid id)
        {
            
                var result = await _baseRepository.DeleteAsync(id);
                return result;
          
        }

        public async Task<int> InsertServiceAsync(string dataJson, IFormFile? imageFile)
        {
            var newId = Guid.NewGuid();
            var entity = JsonConvert.DeserializeObject<TEntity>(dataJson);
            var tableName = typeof(TEntity).Name;
            await ValidateBeforeInsert(entity);
            entity?.GetType()?.GetProperty($"{tableName}Id")?.SetValue(entity, newId);

            if (imageFile != null && imageFile.Length > 0)
            {
                var res = await _baseRepository.InsertAsync(entity);
                if (res > 0)
                {
                    var image = new Image();
                    image?.GetType()?.GetProperty($"{tableName}Id")?.SetValue(image, newId);
                    await _imageService.InsertServiceAsync(image, imageFile);
                }
                return res;

            }
            var result = await _baseRepository.InsertAsync(entity);
            return result;
        }

        public async Task<int> UpdateServiceAsync(Guid id, string dataJson, IFormFile? imageFile)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(dataJson);
            var tableName = typeof(TEntity).Name;
            await ValidateBeforeInsert(entity);
            entity?.GetType()?.GetProperty($"{tableName}Id")?.SetValue(entity, id);

            if (imageFile != null && imageFile.Length > 0)
            {
                var res = await _baseRepository.UpdateAsync(id, entity);
                if (res > 0)
                {
                    var image = new Image();
                    await _imageService.UpdateServiceAsync(id, image, imageFile);
                }
                return res;

            }
            var result = await _baseRepository.UpdateAsync(id, entity);
            return result;
        }

        public virtual async Task ValidateBeforeInsert(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateBeforeUpdate(TEntity entity)
        {
            await Task.CompletedTask;
        }
    }
}
