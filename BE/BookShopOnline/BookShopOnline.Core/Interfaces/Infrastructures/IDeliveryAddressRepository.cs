using BookShopOnline.Core.Dto.DeliveryAddress;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Interface IDeliveryAddressRepository
    /// </summary>
    /// Created By: LQHUY(08/04/1024)
    public interface IDeliveryAddressRepository : IBaseRepository<DeliveryAddress>
    {

        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo userId
        /// </summary>
        /// <param name="userId">mã định danh user</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(08/04/2024
        Task<IEnumerable<DeliveryAddress>> GetByUserIdAsync(Guid userId);

        Task RemoveDefaultAddressAsync();
        Task UpdateDeleliveryAddressDefalutAsync(Guid id);
    }
}
