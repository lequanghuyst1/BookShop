using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IReviewProductRepository : IBaseRepository<ReviewProduct>
    {
        /// <summary>
        /// Hàm lấy ra danh sách các đánh giá theo bookId
        /// </summary>
        /// <param name="bookId">mã định danh của sách</param>
        /// <returns>
        /// danh sách các đánh giá của sản phẩm
        /// </returns>
        /// Created By: LQHUY(17/04/2024)
        Task<IEnumerable<ReviewProduct>> GetByBookIdAsync(Guid bookId);
    }
}
