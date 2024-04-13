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
        IDbTransaction Transaction { get; set; }
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


        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo list id
        /// </summary>
        /// <param name="ids">list id</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(11/04/2024)
        Task<IEnumerable<TEntity>> GetByIdsAsync<TEntity>(List<Guid> ids);

        /// <summary>
        /// Kiểm tra enityCode đã tồn tại trong database chưa
        /// </summary>
        /// <param name="entityCode">mã khách hàng</param>
        /// <returns>
        /// true - đã có trong database
        /// flase - chưa có trong database
        /// </returns>
        /// Created by: LQHUY(12/04/2024)
        Task<bool> CheckDuplicateCodeAsync<TEntity>(string entityCode);

        /// <summary>
        /// Kiểm tra entityCode có phù hợp để Update hay không
        /// </summary>
        /// <param name="entityCode">mã code</param>
        /// <param name="id">mã định danh enity</param>
        /// <returns>
        /// true - hợp lệ
        /// false - không hợp lệ
        /// </returns>
        Task<bool> CheckEntityCodeUpdateAsync<TEnity>(string entityCode, Guid id);

        /// <summary>
        /// Hàm kiểm tra entityName có phù hợp để Update hay không
        /// </summary>
        /// <param name="entityName">tên entity</param>
        /// <param name="id">mã định danh enity</param>
        /// <returns>
        /// true - hợp lệ
        /// false - không hợp lệ
        /// </returns>
        Task<bool> CheckEntityNameUpdateAsync<TEntity>(string entityName, Guid id);

        /// <summary>
        /// Hàm lấy ra bản ghi theo slug
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="slug"></param>
        /// <returns>thông tin bản ghi</returns>
        /// Created By: LQHUY(13/04/2024)
        Task<TEntity> GetBySlugAsync<TEntity>(string slug);



    }
}
