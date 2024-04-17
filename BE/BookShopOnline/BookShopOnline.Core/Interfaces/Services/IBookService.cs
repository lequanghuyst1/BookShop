using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IBookService : IBaseService<Book, BookDto>
    {
        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        Task<IEnumerable<BookDto>> GetByCategorySlugAsync(string categorySlug);

        /// <summary>
        /// Hàm thực hiện phân trang dữ liệu và lọc theo điều kiện
        /// </summary>
        /// <param name="filter">dữ liệu phân trang và các điều kiện</param>
        /// <returns>
        /// danh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        Task<PagingEntity<BookDto>> FilterAsync(Filter filter);

    }
}
