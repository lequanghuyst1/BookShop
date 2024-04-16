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

        public async Task<PagingEntity<Book>> FilterAsync(int pageSize, int pageNumber, int sortType)
        {
            var sql = $"Proc_Book_Filter";

            PagingEntity<Book> pagingEntity = new PagingEntity<Book>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@sortType", sortType);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.Connection.QueryAsync<Book>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
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
