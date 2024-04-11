using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services.Base
{
    public interface IBaseReadOnlyService<TDto> 
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<string> GetNewCodeAsync();
        Task<PagingEntity<TDto>> GetFilterPagingAsync(string? searchString, int pageSize, int PageNumber);
        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo list id
        /// </summary>
        /// <param name="ids">list id</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(11/04/2024)
        Task<IEnumerable<TDto>> GetByIdsAsync(List<Guid> ids);
    }
}
