using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IBookRepository
    {
        /// <summary>
        /// Lấy ra tất cả sách trong database
        /// </summary>
        /// <returns>
        /// Danh sách các bản ghi
        /// </returns>
        /// CreatedBy: LQHUY(16/03/2024)
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// Lấy ra thông tin chi tiết 1 cuốn sách theo ID(mã định danh)
        /// </summary>
        /// <param name="id">Mã định danh của sách</param>
        /// <returns>
        /// Thông tin chi tiết cuốn sách
        /// </returns>
        /// CreatedBy: LQHUY(16/03/2024)
        Task<Book> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy ra danh sách các cuốn sách trong database theo mã định danh danh mục
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Danh sách các bản ghi
        /// </returns>
        /// CreatedBy: LQHUY(16/03/2024)
        Task<IEnumerable<Book>> GetByCategoryIdAsync(string id);

    }
}
