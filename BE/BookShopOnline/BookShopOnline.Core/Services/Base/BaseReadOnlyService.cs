using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services.Base
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
        protected IBaseRepository<TEntity> _baseRepository;

        public BaseReadOnlyService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities  = await _baseRepository.GetAllAsync();
            var res = entities.Select(entity => MapEntityToDto(entity));
            return res;
        }

        public async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            var res = MapEntityToDto(entity);
            return res;
        }

        public async Task<string> GetNewCodeAsync()
        {
            var res = await _baseRepository.GetNewCodeAsync();
            return res;
        }

        public abstract TDto MapEntityToDto(TEntity entity);
    }
}
