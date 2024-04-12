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

        /// <summary>
        /// Kiểm tra enityCode đã tồn tại trong database chưa
        /// </summary>
        /// <param name="entityCode">mã code</param>
        /// <returns>
        /// true - đã có trong database
        /// flase - chưa có trong database
        /// </returns>
        /// Created by: LQHUY(12/04/2024)
        Task<bool> CheckDuplicateCodeAsync(string entityCode);

        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo list id
        /// </summary>
        /// <param name="ids">list id</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(11/04/2024)
        Task<IEnumerable<TEntity>> GetByIdsAsync(List<Guid> ids);
    }
}
