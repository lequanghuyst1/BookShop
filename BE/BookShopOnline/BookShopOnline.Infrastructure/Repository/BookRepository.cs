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
using BookShopOnline.Core.Model;
using BookShopOnline.Core.Enums;

namespace BookShopOnline.Infrastructure.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagingEntity<Book>> FilterAsync(Filter filter)
        {
            //var sql = $"Proc_Book_Filter";
            var priceRangesConditions = new List<string>();
            var filterConditions = new List<string>();
            var publisherConditions = new List<string>();
            var categorySlugConditions = new List<string>();
            var parameters = new DynamicParameters();
            var rangeColumn = filter.RangeColumn;
            var filterInput = filter.FilterInput;
            PagingEntity<Book> pagingEntity = new PagingEntity<Book>();

            //tạo câu truy vấn
            var query = "";
            //kiểm tra xem có lấy danh sách sản phầm theo dang mục không
            if (string.IsNullOrEmpty(filter.Slug))
            {
                query = @"Select * from view_book where 1 = 1 ";

            }
            else
            {
                query = @"Select * from view_book where CategorySlug = @CategorySlug ";
                parameters.Add("CategorySlug", filter.Slug);
            }

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query += "AND BookName LIKE CONCAT ('%',@searchString,'%')";
                parameters.Add("searchString", filter.SearchString);
            }

            //kiểm tra xem có lọc theo giá tiền hay k
            if (rangeColumn != null && rangeColumn.Count > 0)
            {
                for (int i = 0; i < rangeColumn.Count; i++)
                {
                    string paramName1 = $"minPrice{i}";
                    string paramName2 = $"maxPrice{i}";
                    priceRangesConditions.Add($"({rangeColumn[i].ColumnName} BETWEEN @{paramName1} AND @{paramName2})");
                    parameters.Add(paramName1, rangeColumn[i].MinValue);
                    parameters.Add(paramName2, rangeColumn[i].MaxValue);
                }
                var priceRangesString = string.Join(" OR ", priceRangesConditions);
                query += $"AND ({priceRangesString})";
            }

            //kiểm tra xem có lọc theo giá trị không
            if (filterInput != null && filterInput.Count > 0)
            {
                for (int i = 0; i < filterInput.Count; i++)
                {
                    if (filterInput[i].ColumnName == "Author")
                    {
                        var paramInput = $"value{i}";
                        filterConditions.Add($"{filterInput[i].ColumnName} = @{paramInput}");
                        parameters.Add(paramInput, filterInput[i].Value);
                    }
                    if (filterInput[i].ColumnName == "PublisherName")
                    {
                        var paramInput = $"value{i}";
                        publisherConditions.Add($"{filterInput[i].ColumnName} = @{paramInput}");
                        parameters.Add(paramInput, filterInput[i].Value);
                    }
                    if (filterInput[i].ColumnName == "CategoryName")
                    {
                        var paramInput = $"value{i}";
                        categorySlugConditions.Add($"{filterInput[i].ColumnName} = @{paramInput}");
                        parameters.Add(paramInput, filterInput[i].Value);
                    }

                }
                string filterConditionString = string.Join(" OR ", filterConditions);
                string publisherConditionsString = string.Join(" OR ", publisherConditions);
                string categorySlugConditonsString = string.Join(" OR ", categorySlugConditions);

                if (!string.IsNullOrEmpty(filterConditionString))
                {
                    query += $" AND ({filterConditionString}) ";
                }
                if (!string.IsNullOrEmpty(publisherConditionsString))
                {
                    query += $" AND ({publisherConditionsString}) ";
                }
                if (!string.IsNullOrEmpty(categorySlugConditonsString))
                {
                    query += $" AND ({categorySlugConditonsString}) ";
                }
            }


            var paramOrderBy = "";

            //Kiểm tra xem có lọc theo cột nào hay không (Bên FE đã để mặc định là sản phẩm mới nhất)
            if (filter.SortColumn != null)
            {
                string type = ((SortType)filter.SortColumn.Type == SortType.DESC) ? "DESC" : "ASC";
                query += $" ORDER BY {filter.SortColumn.ColumnName} {type}";
            }
            query += paramOrderBy;
            query += " LIMIT @pageSize OFFSET @start_index";

            parameters.Add("@pageSize", filter.PageSize);
            parameters.Add("@start_index", (filter.PageNumber - 1) * filter.PageSize);

            // lấy ra chuỗi sql k có limit
            int limitIndex = query.IndexOf("LIMIT");
            string sqlWithoutLimit = query.Substring(0, limitIndex); // Cắt từ vị trí 0 đến vị trí của "LIMIT"

            //thực hiện truy vấn lấy ra các bản ghi theo điều kiện
            var res = await _dbContext.Connection.QueryAsync<Book>(query, parameters);
            //thực hiện truy vấn theo điều kiện không có limit
            var resNoLimit = await _dbContext.Connection.QueryAsync<Book>(sqlWithoutLimit, parameters);

            pagingEntity.Data = res;
            //gán tổng cố bản ghi
            pagingEntity.TotalRecord = resNoLimit.Count();
            //gán tổng số trang

            if (filter.PageSize.HasValue && filter.PageSize.Value != 0)
            {
                pagingEntity.TotalPage = (int)Math.Ceiling((double)pagingEntity.TotalRecord / filter.PageSize.Value);
            }
            else
            {
                pagingEntity.TotalPage = 1;

            }

            return pagingEntity;
        }

        public async Task<List<string>> GetAuthorByCategorySlugAsync(string? categorySlug)
        {
            var sqlCommand = "Select DISTINCT Author From view_book where @CategorySlug IS NULL OR CategorySlug = @CategorySlug";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CategorySlug", categorySlug);
            var res = await _dbContext.Connection.QueryAsync<string>(sqlCommand, parameters);
            return res.ToList();
        }
        public async Task<List<string>> GetPublisherByCategorySlugAsync(string? categorySlug)
        {

            var sqlCommand = "Select DISTINCT PublisherName From view_book where @CategorySlug IS NULL OR CategorySlug = @CategorySlug";
            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CategorySlug", categorySlug);
            var res = await _dbContext.Connection.QueryAsync<string>(sqlCommand, parameters);
            return res.ToList();
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
