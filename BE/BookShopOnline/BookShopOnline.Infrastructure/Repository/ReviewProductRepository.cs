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
    public class ReviewProductRepository : BaseRepository<ReviewProduct>, IReviewProductRepository
    {
        public ReviewProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ReviewProduct>> GetByBookIdAsync(Guid bookId)
        {
            var sqlCommand = "Select * from view_reviewproduct where BookId = @BookId";
            var res = await _dbContext.Connection.QueryAsync<ReviewProduct>(sqlCommand, new { BookId = bookId });
            return res;
        }
    }
}
