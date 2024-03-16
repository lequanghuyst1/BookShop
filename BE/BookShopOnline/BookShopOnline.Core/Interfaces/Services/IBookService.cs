using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Thêm mới thông tin một cuốn sách
        /// </summary>
        /// <param name="book">thông tin chi tiết cuốn sách</param>
        /// <returns>số dòng thêm thành công</returns>
        /// CreatedBy: LQHUY(16/03/2024)
        Task<int> InsertServiceAsync(Book book);

        /// <summary>
        /// Sửa thông tin một cuốn sách
        /// </summary>
        /// <param name="book">thông tin chi tiết cuốn sách</param>
        /// <param name="id">mã định danh của sách</param>
        /// <returns>số dòng cập nhập thành công</returns>
        /// CreatedBy: LQHUY(16/03/2024)
        Task<int> UpdateServiceAsync(Book book, Guid id);
    }
}
