using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid entityId);
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity, Guid entityId);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
