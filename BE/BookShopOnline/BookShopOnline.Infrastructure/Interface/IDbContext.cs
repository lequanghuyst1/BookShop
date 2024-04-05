using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookShopOnline.Infrastructure.Interface
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>();
        Task<TEntity?> GetByIdAsync<TEntity>(Guid entityId);
        Task<PagingEntity<TEntity>> GetFilterPagingAsync<TEntity>(string? searchString, int pageSize, int pageNumber);
        //Task<int> InsertAsync<TEntity>(string dataJson, IFormFile? image);
        Task<int> InsertAsync<TEntity>(TEntity entity);
        //Task<int> UpdateAsync<TEntity>(string dataJson, IFormFile? image);
        Task<int> UpdateAsync<TEntity>(Guid id, TEntity entity);
        Task<int> DeleteAsync<TEntity>(Guid id);
        Task<int> DeleteManyAsync<TEntity>(List<Guid> ids);
        Task<bool> CheckExitEntityNameAsync<TEntity>(string entityName);
        Task<string> GetNewCodeAsync<TEntity>();


    }
}
