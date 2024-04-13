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
        /// Hàm kiểm tra enityCode đã tồn tại trong database chưa
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

        /// <summary>
        /// Hàm kiểm tra entityCode có phù hợp để Update hay không
        /// </summary>
        /// <param name="entityCode">mã code</param>
        /// <param name="id">mã định danh enity</param>
        /// <returns>
        /// true - hợp lệ
        /// false - không hợp lệ
        /// </returns>
        Task<bool> CheckEntityCodeUpdateAsync(string entityCode, Guid id);

        /// <summary>
        /// Hàm kiểm tra entityName có phù hợp để Update hay không
        /// </summary>
        /// <param name="entityName">tên entity</param>
        /// <param name="id">mã định danh enity</param>
        /// <returns>
        /// true - hợp lệ
        /// false - không hợp lệ
        /// </returns>
        Task<bool> CheckEntityNameUpdateAsync(string entityName, Guid id);

        /// <summary>
        /// Hàm lấy ra bản ghi theo slug
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="slug"></param>
        /// <returns>thông tin bản ghi</returns>
        /// Created By: LQHUY(13/04/2024)
        Task<TEntity> GetBySlugAsync(string slug);
    }
}
