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
                throw new ConnectDbException(System.Net.HttpStatusCode.InternalServerError, ResourceVN.ConnectDbException, errors);
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

        public async Task<int> InsertHaveImageAsync<TEntity>(IFormFile? image, string dataJson)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(dataJson);
            if (image?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await image.CopyToAsync(stream);
                }
                var imagePath = $"/images/{image.FileName}";
                entity?.GetType()?.GetProperty("Image")?.SetValue(entity, imagePath);
            }
            else
            {
                entity?.GetType()?.GetProperty("Image")?.SetValue(entity, null);
            }
            var tableName = typeof(TEntity).Name;
            var procName = $"Proc_{tableName}_Insert";
            var res = await Connection.ExecuteAsync(procName, entity);
            return res;
        }

        public async Task<int> UpdateHaveImageAsync<TEntity>(IFormFile? image, string dataJson)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(dataJson);
            if (image?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await image.CopyToAsync(stream);
                }
                var imagePath = $"/images/{image.FileName}";
                entity?.GetType()?.GetProperty("Image")?.SetValue(entity, imagePath);
            }
            else
            {
                entity?.GetType()?.GetProperty("Image")?.SetValue(entity, null);
            }

            var tableName = typeof(TEntity).Name;
            var procName = $"Proc_{tableName}_Update";
            var res = await Connection.ExecuteAsync(procName, entity);
            return res;
        }
    }
}
