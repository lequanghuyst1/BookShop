using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Model;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using Microsoft.AspNetCore.Http.Features;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookShopOnline.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<object>> CalculateTotalSalesPerMonth(int year)
        {
            var procName = "Proc_Order_CalculateTotalSalesPerMonth";
            var res = await _dbContext.Connection.QueryAsync<object>(procName, new { Year = year });
            return res;
        }

        public async Task<int> ConfirmAllAsync()
        {
            var procName = "Proc_Order_ConfirmAll";
            var res = await _dbContext.Connection.ExecuteAsync(procName, transaction: _dbContext.Transaction);
            return res;
        }

        public async Task<PagingEntity<OrderDto>> GetByUserId(Guid userId, int? PageSize, int? PageNumber, string? ColumnName, string? Value)
        {
            PagingEntity<OrderDto> pagingEntity = new PagingEntity<OrderDto>();
            DynamicParameters parameters = new DynamicParameters();
            var sqlCommand = "";
            if (string.IsNullOrEmpty(ColumnName))
            {
                sqlCommand = $"SELECT * FROM view_order vo WHERE vo.UserId = @UserId ORDER BY vo.CreatedDate DESC";
            }
            else
            {
                sqlCommand = $"SELECT * FROM view_order vo WHERE vo.UserId = @UserId AND ({ColumnName} = @value) ORDER BY vo.CreatedDate DESC";
                parameters.Add("@value", Value);
            }

            parameters.Add("@UserId", userId);
            //parameters.Add("@searchString", searchString);

            if (PageSize != null && PageNumber != null)
            {
                var paramLimit = " LIMIT @pageSize OFFSET @start_index";
                sqlCommand += paramLimit;
                parameters.Add("@pageSize", PageSize);
                parameters.Add("@start_index", (PageNumber - 1) * PageSize);

                // lấy ra chuỗi sql k có limit
                int limitIndex = sqlCommand.IndexOf("LIMIT");
                var sqlComandWithoutLimit = sqlCommand.Substring(0, limitIndex); // Cắt từ vị trí 0 đến vị trí của "LIMIT"
                var resNoLimit = await _dbContext.Connection.QueryAsync<OrderDto>(sqlComandWithoutLimit, parameters);
                //gán tổng số bản ghi
                pagingEntity.TotalRecord = resNoLimit.Count();
                //gán tổng số trang
                pagingEntity.TotalPage = (int)Math.Ceiling((double)pagingEntity.TotalRecord / PageSize.Value);
            }
            var res = await _dbContext.Connection.QueryAsync<OrderDto>(sqlCommand, parameters);
            //gán giá trị các bản ghi
            pagingEntity.Data = res;
            if (PageSize == null && PageNumber == null)
            {
                pagingEntity.TotalRecord = res.Count();
                pagingEntity.TotalPage = 1;
            }

            return pagingEntity;
        }

        public async Task<double> GetTotalRevenue()
        {
            var procName = "Proc_Order_GetTotalRevenvue";
            var res = await _dbContext.Connection.ExecuteScalarAsync<double>(procName);
            return res;
        }

        public async Task<int> GetTotalOrderByConditionIn24Hour(string? fieldCondition)
        {
            string sqlCommand = @"SELECT COUNT(*) FROM view_order WHERE OrderDate >= NOW() - INTERVAL 24 HOUR";
            DynamicParameters parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(fieldCondition))
            {
                string conditionString = $"{fieldCondition} = @Condition";
                parameters.Add("Condition", Core.Enums.OrderStatus.WAIT_FOR_CONFIRMATION);
                sqlCommand += $" AND {conditionString}";
            }

            var result = await _dbContext.Connection.ExecuteScalarAsync<int>(sqlCommand, parameters);
            return result;
        }

        public async Task<IEnumerable<OrderDto>> GetByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate, Guid? categoryId)
        {
            var procName = "Proc_Order_GetByTimeType";


            var now = DateTime.Now;


            fromDate = fromDate.AddDays(1);
            toDate = toDate.AddDays(1);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("typeOfTime", typeOfTime);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            parameters.Add("categoryId", categoryId);
            var res = await _dbContext.Connection.QueryAsync<OrderDto>(procName, parameters);
            return res;
        }
        public async Task<object> CalculateTotalAmountByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate, Guid? categoryId)
        {
            var procName = "Proc_Order_CalculateTotalAmountByTypeOfTime";

            var now = DateTime.Now;

            fromDate = fromDate.AddDays(1);
            toDate = toDate.AddDays(1);


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("typeOfTime", typeOfTime);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            parameters.Add("categoryId", categoryId);

            var res = await _dbContext.Connection.QueryAsync<object>(procName, parameters);
            return res;
        }

        public async Task<IEnumerable<object>> GetRevenueByProduct(int typeOfTime, DateTime fromDate, DateTime toDate, int quantityFilter, Guid? categoryId)
        {
            var procName = "Proc_Order_CalculateRevenueByProduct";

            var now = DateTime.Now;


            fromDate = fromDate.AddDays(1);
            toDate = toDate.AddDays(1);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("typeOfTime", typeOfTime);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            parameters.Add("quantityFilter", quantityFilter);
            parameters.Add("categoryId", categoryId);

            var res = await _dbContext.Connection.QueryAsync<object>(procName, parameters);
            return res;
        }

        public async Task<PagingEntity<OrderDto>> FilterAsync(Filter filter)
        {
            var filterInput = filter.FilterInput;
            var pagingResult = new PagingEntity<OrderDto>();

            DynamicParameters parameters = new DynamicParameters();
            var sqlCommand = @"SELECT *
                  FROM view_order vo
                  WHERE (@searchString IS NULL
                  OR vo.Fullname LIKE CONCAT('%', @searchString, '%')
                  OR vo.OrderCode LIKE CONCAT('%', @searchString, '%'))";

            parameters.Add("@searchString", filter.SearchString);
            var filterConditions = new List<string>();
            int i = 0;
            if (filterInput != null && filterInput.Count() > 0)
            {
                foreach (var item in filterInput)
                {
                    var paramInput = $"value{i}";
                    filterConditions.Add($"{item.ColumnName} = @{paramInput}");
                    parameters.Add(paramInput, item.Value);
                    i++;
                }
                string filterConditionString = string.Join(" AND ", filterConditions);

                sqlCommand += $"AND {filterConditionString}";
            }

            var paramOrderBy = " ORDER BY vo.CreatedDate DESC";

            sqlCommand += paramOrderBy;

            if (filter.PageSize != null && filter.PageNumber != null)
            {
                var paramLimit = " LIMIT @pageSize OFFSET @start_index";
                sqlCommand += paramLimit;
                parameters.Add("@pageSize", filter.PageSize);
                parameters.Add("@start_index", (filter.PageNumber - 1) * filter.PageSize);

                // lấy ra chuỗi sql k có limit
                int limitIndex = sqlCommand.IndexOf("LIMIT");
                var sqlComandWithoutLimit = sqlCommand.Substring(0, limitIndex); // Cắt từ vị trí 0 đến vị trí của "LIMIT"
                var resNoLimit = await _dbContext.Connection.QueryAsync<OrderDto>(sqlComandWithoutLimit, parameters);
                //gán tổng số bản ghi
                pagingResult.TotalRecord = resNoLimit.Count();
                //gán tổng số trang
                pagingResult.TotalPage = (int)Math.Ceiling((double)pagingResult.TotalRecord / filter.PageSize.Value);
            }

            var res = await _dbContext.Connection.QueryAsync<OrderDto>(sqlCommand, parameters);

            pagingResult.Data = res;
            if (filter.PageSize == null && filter.PageNumber == null)
            {
                pagingResult.TotalRecord = res.Count();
                pagingResult.TotalPage = 1;

            }
            return pagingResult;
        }
    }
}
