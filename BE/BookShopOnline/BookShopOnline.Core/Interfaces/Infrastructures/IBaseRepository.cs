using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Entitites;
using Microsoft.AspNetCore.Http;
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
        Task<PagingEntity<TEntity>> GetFilterPagingAsync(string? searchString, int pageSize, int pageNumber);
        //Task<int> InsertAsync(string dataJson, IFormFile? imageFile);
        Task<int> InsertAsync(TEntity entity);

        //Task<int> UpdateAsync(string dataJson, IFormFile? imageFile);
        Task<int> UpdateAsync(Guid id, TEntity entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteManyAsync(List<Guid> ids);
        Task<bool> CheckExitEntityNameAsync(string entityName);
        Task<string> GetNewCodeAsync();
    }
}
