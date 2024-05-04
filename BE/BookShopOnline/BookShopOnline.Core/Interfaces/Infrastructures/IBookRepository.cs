using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo categoryId
        /// </summary>
        /// <param name="id">mã định danh category</param>
        /// <returns>
        /// danh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(15/04/2024)
        Task<IEnumerable<Book>> GetByCategoryIdAsync(string id);

        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(15/04/2024)
        Task<IEnumerable<Book>> GetByCategorySlugAsync(string categorySlug);

        /// <summary>
        /// Hàm thực hiện phân trang dữ liệu và theo điều kiện lọc
        /// </summary>
        /// <param name="filter">thực thể filter</param>
        /// <returns>
        /// thực thể entityPaging
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        Task<PagingEntity<Book>> FilterAsync(Filter filter);

        /// <summary>
        /// Hàm lấy ra danh sách các tác giả của sách theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các tác giả
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        Task<List<string>> GetAuthorByCategorySlugAsync(string? categorySlug);

        /// <summary>
        /// Hàm lấy ra danh sách các nhà xuất bản của sách theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các nhà xuất bản
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        Task<List<string>> GetPublisherByCategorySlugAsync(string? categorySlug);
        //Task<List<string>> GetCategoryBySearchStringAsync(string? searchString);

    }
}
