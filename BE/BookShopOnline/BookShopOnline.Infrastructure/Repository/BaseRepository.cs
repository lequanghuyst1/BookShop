using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
    {
        protected IDbContext _dbContext { get; set; }
        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var res = await _dbContext.GetAllAsync<TEntity>();
            return res;
        }

        public async Task<TEntity?> GetByIdAsync(Guid entityId)
        {
            var res = await _dbContext.GetByIdAsync<TEntity>(entityId);
            return res;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var res = await _dbContext.InsertAsync<TEntity>(entity);
            return res;
        }

        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var res = await _dbContext.UpdateAsync<TEntity>(id, entity);
            return res;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var res = await _dbContext.DeleteAsync<TEntity>(id);
            return res;
        }

        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var res = await _dbContext.DeleteManyAsync<TEntity>(ids);
            return res;
        }

        public async Task<bool> CheckExitEntityNameAsync(string entityName)
        {
            var res = await _dbContext.CheckExitEntityNameAsync<TEntity>(entityName);
            return res;
        }

        public async Task<string> GetNewCodeAsync()
        {
            var res = await _dbContext.GetNewCodeAsync<TEntity>();
            return res;
        }

        public async Task<PagingEntity<TEntity>> GetFilterPagingAsync(string? searchString, int pageSize, int pageNumber)
        {
            var res = await _dbContext.GetFilterPagingAsync<TEntity>(searchString, pageSize, pageNumber);
            return res;
        }
    }
}
