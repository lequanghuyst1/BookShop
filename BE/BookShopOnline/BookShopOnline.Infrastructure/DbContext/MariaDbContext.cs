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

namespace BookShopOnline.Infrastructure.DbContext
{
    public class MariaDbContext : IDbContext
    {
        readonly string ConnectionString;
        public IDbConnection Connection { get; set; }
        public MariaDbContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
            Connection = new MySqlConnection(ConnectionString);
        }
      
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        {
            var tableName = typeof(TEntity).Name;
            var sqlCommand = $"Select * From {tableName}";
            var res = await Connection.QueryAsync<TEntity>(sqlCommand);
            return res;
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(Guid entityId)
        {
            var tableName = typeof(TEntity).Name;
            var sqlCommand = $"Select * From {tableName} where {tableName}Id = @EntityId";
            var res = await Connection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, new { EntityId = entityId});
            return res;
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var procName = $"Proc_{tableName}_Insert";
            var res = await Connection.ExecuteAsync(procName, entity);
            return res;
        }

        public async Task<int> UpdateAsync<TEntity>(TEntity entity, Guid entityId)
        {
            var tableName = typeof(TEntity).Name;
            entity?.GetType()?.GetProperty($"{tableName}Id")?.SetValue(entity, entityId);
            var procName = $"Proc_{tableName}_Update";
            var res = await Connection.ExecuteAsync(procName, entity);
            return res;
        }

        public async Task<int> DeleteAsync<TEntity>(Guid id)
        {
            var tableName = typeof(TEntity).Name;
            var sql = $"DELETE FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{tableName}Id", id);
            var res = await Connection.ExecuteAsync(sql, dynamicParameters);
            return res;
        }

        public async Task<int> DeleteManyAsync<TEntity>(List<Guid> ids)
        {
            var tableName = typeof(TEntity).Name;
            var sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @Ids";
            var res = await Connection.ExecuteAsync(sql, new { Ids = ids });
            return res;
        }
    }
}
