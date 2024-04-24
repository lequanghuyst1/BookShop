using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using System;
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

        public async Task<object> CalculateTotalAmountByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate)
        {
            var procName = "Proc_Order_CalculateTotalAmountByTypeOfTime";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("typeOfTime", typeOfTime);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            var res = await _dbContext.Connection.QueryAsync<object>(procName, parameters);
            return res;
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
            var res = await _dbContext.Connection.ExecuteAsync(procName,transaction: _dbContext.Transaction);
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

        public async Task<IEnumerable<OrderDto>> GetByTypeOfTime(int typeOfTime, DateTime fromDate, DateTime toDate)
        {
            var procName = "Proc_Order_GetByTimeType";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("typeOfTime", typeOfTime);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            var res = await _dbContext.Connection.QueryAsync<OrderDto>(procName, parameters);
            return res;
        }
    }
}
  