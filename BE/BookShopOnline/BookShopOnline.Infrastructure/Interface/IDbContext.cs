using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookShopOnline.Infrastructure.Interface
{
    public interface IDbContext
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>();
        Task<TEntity?> GetByIdAsync<TEntity>(Guid entityId);
        Task<int> InsertAsync<TEntity>(TEntity entity);
        Task<int> UpdateAsync<TEntity>(TEntity entity, Guid entityId);
        Task<int> DeleteAsync<TEntity>(Guid id);
        Task<int> DeleteManyAsync<TEntity>(List<Guid> ids);
    }
}
