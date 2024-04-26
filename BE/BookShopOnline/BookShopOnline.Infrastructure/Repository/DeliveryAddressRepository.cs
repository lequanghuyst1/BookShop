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
    /// <summary>
    /// Class DeliveryAddressRepository
    /// </summary>
    /// Created By: LQHUY(08/04/1024)
    public class DeliveryAddressRepository : BaseRepository<DeliveryAddress>, IDeliveryAddressRepository
    {
        #region Constructor
        public DeliveryAddressRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<DeliveryAddress>> GetByUserIdAsync(Guid userId)
        {
            var procName = "Proc_DeliveryAddress_GetByUserId";
            var res = await _dbContext.Connection.QueryAsync<DeliveryAddress>(procName, new { UserId = userId });
            return res;
        }

        public async Task RemoveDefaultAddressAsync(Guid userId)
        {
            var procName = "Proc_DeliveryAddress_RemoveDefault";
            await _dbContext.Connection.ExecuteAsync(procName, new { UserId = userId });
        }

        public async Task UpdateDeleliveryAddressDefalutAsync(Guid id, DeliveryAddress deliveryAddress)
        {
            var procName = "Proc_DeliveryAddress_SetDefault";
            await _dbContext.Connection.ExecuteAsync(procName, deliveryAddress);
        }
        #endregion
    }
}
