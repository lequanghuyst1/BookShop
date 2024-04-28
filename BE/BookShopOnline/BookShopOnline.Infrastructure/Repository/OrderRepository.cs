using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Model;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Order>> GetByUserId(Guid userId)
        {
            var procName = "Proc_Order_GetByUserId";
            var res = await _dbContext.Connection.QueryAsync<Order>(procName, new { UserId = userId }, _dbContext.Transaction);
            return res;
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
            var paramLimit = " LIMIT @pageSize OFFSET @start_index";

            parameters.Add("@pageSize", filter.PageSize);
            parameters.Add("@start_index", (filter.PageNumber - 1) * filter.PageSize);

            sqlCommand += paramOrderBy;
            sqlCommand += paramLimit;

            // lấy ra chuỗi sql k có limit
            int limitIndex = sqlCommand.IndexOf("LIMIT");
            string sqlComandWithoutLimit = sqlCommand.Substring(0, limitIndex); // Cắt từ vị trí 0 đến vị trí của "LIMIT"

            var res = await _dbContext.Connection.QueryAsync<OrderDto>(sqlCommand, parameters);
            var resNoLimit = await _dbContext.Connection.QueryAsync<OrderDto>(sqlComandWithoutLimit, parameters);

            var pagingResult = new PagingEntity<OrderDto>();
            pagingResult.Data = res;
            //gán tổng cố bản ghi
            pagingResult.TotalRecord = resNoLimit.Count();
            //gán tổng số trang
            pagingResult.TotalPage = (int)Math.Ceiling((double)pagingResult.TotalRecord / filter.PageSize);
            return pagingResult;
        }
    }
}
