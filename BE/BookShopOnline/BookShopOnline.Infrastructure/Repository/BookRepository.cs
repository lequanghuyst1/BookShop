using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using System.Transactions;

namespace BookShopOnline.Infrastructure.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Book>> GetByCategoryIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetByCategorySlugAsync(string categorySlug)
        {
            var sqlCommand = $"Select * From view_Book where CategorySlug = @Slug";
            var res = await _dbContext.Connection.QueryAsync<Book>(sqlCommand, new { Slug = categorySlug });
            return res;
        }
    }
}
