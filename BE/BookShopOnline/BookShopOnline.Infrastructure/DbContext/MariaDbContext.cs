using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Resources;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json;
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
            try
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection");
                Connection = new MySqlConnection(ConnectionString);
            }
            catch (MySqlException)
            {
                Dictionary<string, string[]>? errors = new Dictionary<string, string[]>();
                errors.Add("Database", new string[] { ResourceVN.ConnectDbException });
                throw new ConnectDbException( ResourceVN.ConnectDbException, errors);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        {
            var tableName = typeof(TEntity).Name.ToLower();
            var sqlCommand = $"Select * From view_{tableName}";
            var res = await Connection.QueryAsync<TEntity>(sqlCommand);
            return res;
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(Guid entityId)
        {
            var tableName = typeof(TEntity).Name;
            var sqlCommand = $"Select * From {tableName} where {tableName}Id = @EntityId";
            var res = await Connection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, new { EntityId = entityId });
            return res;
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var procName = $"Proc_{tableName}_Insert";
            var res = await Connection.ExecuteAsync(procName, entity);
            return res;
        }

        public async Task<int> UpdateAsync<TEntity>(Guid id, TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var procName = $"Proc_{tableName}_Update";
            entity?.GetType()?.GetProperty($"{tableName}Id")?.SetValue(entity, id);
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

        public async Task<bool> CheckExitEntityNameAsync<TEntity>(string entityName)
        {
            var tableName = typeof(TEntity).Name;
            var sqlCommand = $"Select * From {tableName} where {tableName}Name = @EntityName";
            var res = await Connection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, new { EntityName = entityName });
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public async Task<string> GetNewCodeAsync<TEntity>()
        {
            var tableName = typeof(TEntity).Name;
            var sql = $"SELECT {tableName}Code FROM {tableName}";
            var listEntityCode = (await Connection.QueryAsync<string>(sql)).ToList();
            var listNumberCode = new List<int>();
            var prefixNewCode = GetPrefixNewCode(tableName);
            for (var i = 0; i < listEntityCode.Count; i++)
            {
                listNumberCode.Add(int.Parse(listEntityCode[i].Replace($"{prefixNewCode}-", "")));
            }
            listNumberCode.Sort();
            var newCode = $"{prefixNewCode}-{listNumberCode[listNumberCode.Count - 1] + 1}";
            return newCode;
        }
        public string GetPrefixNewCode(string tableName)
        {
            switch (tableName)
            {
                case "Category":
                    return "DM";
                case "Book":
                    return "MS";
                case "Publisher":
                    return "NXB";
                default:
                    return "";
            }
        }

        

      
        public async Task<PagingEntity<TEntity>> GetFilterPagingAsync<TEntity>(string? searchString, int pageSize, int pageNumber)
        {
            var tableName = typeof(TEntity).Name;
            var sql = $"Proc_{tableName}_FilterPaging";

            PagingEntity<TEntity> pagingEntity = new PagingEntity<TEntity>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchString", searchString);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await Connection.QueryAsync<TEntity>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }
    }
}
