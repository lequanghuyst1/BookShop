using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class ProductReviewRepository : BaseRepository<ProductReview>, IProductReviewRepository
    {
        public ProductReviewRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ProductReview>> GetByBookIdAsync(Guid bookId)
        {
            var sqlCommand = "Select * from view_productreview where BookId = @BookId";
            var res = await _dbContext.Connection.QueryAsync<ProductReview>(sqlCommand, new { BookId = bookId });
            return res;
        }
    }
}
