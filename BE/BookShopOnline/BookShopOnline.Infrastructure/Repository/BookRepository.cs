using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        readonly string ConnectionString;
        public IDbConnection Connection { get; set; }
        public BookRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
            Connection = new MySqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var sqlCommand = "Select * From Book";
            var res = await Connection.QueryAsync<Book>(sqlCommand);
            return res;
        }

        public Task<Book> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetByCategoryIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
