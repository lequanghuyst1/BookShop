using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services.Base;
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

        public async Task<int> InsertServiceAsync(TEntity entity)
        {
            var res = await _baseRepository.InsertAsync(entity);
            return res;
        }

        public async Task<int> UpdateServiceAsync(TEntity entity, Guid id)
        {
            var res = await _baseRepository.UpdateAsync(entity,id);
            return res;
        }
    }
}
