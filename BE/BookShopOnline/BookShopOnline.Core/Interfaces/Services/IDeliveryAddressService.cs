using BookShopOnline.Core.Dto.DeliveryAddress;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    /// <summary>
    /// Interface IDeliveryAddressService
    /// </summary>
    /// Created By: LQHUY(08/04/1024)
    public interface IDeliveryAddressService : IBaseService<DeliveryAddress, DeliveryAddressDto>
    {
        /// <summary>
        /// Hàm lấy ra danh sách các bản ghi theo userId
        /// </summary>
        /// <param name="userId">mã định danh user</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created By: LQHUY(08/04/2024
        Task<IEnumerable<DeliveryAddressDto>> GetByUserIdServiceAsync(Guid userId);

        Task UpdateDeleliveryAddressDefalutAsync(Guid id, DeliveryAddress deliveryAddress);

    }
}
