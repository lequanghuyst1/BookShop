using AutoMapper;
using BookShopOnline.Core.Dto.DeliveryAddress;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    /// <summary>
    /// Class DeliveryAddressService
    /// </summary>
    /// Created By: LQHUY(08/04/1024)
    public class DeliveryAddressService : BaseService<DeliveryAddress, DeliveryAddressDto>, IDeliveryAddressService
    {
        IDeliveryAddressRepository _deliveryAddressRepository;
        #region Constructor
        public DeliveryAddressService(IDeliveryAddressRepository deliveryAddressRepository, IMapper mapper, IImageService imageService) : base(deliveryAddressRepository, mapper, imageService)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
        }

        public async Task<IEnumerable<DeliveryAddressDto>> GetByUserIdServiceAsync(Guid userId)
        {
            var res = await _deliveryAddressRepository.GetByUserIdAsync(userId);
            var resDto = res.Select(item => base.MapEntityToDto(item));
            return resDto;
        }

        public override async Task<int> InsertServiceAsync(string dataJson, IFormFile? imageFile)
        {
            var newId = Guid.NewGuid();
            var deliveryAddress = JsonConvert.DeserializeObject<DeliveryAddress>(dataJson);
            await ValidateBeforeInsert(deliveryAddress);
            deliveryAddress?.GetType()?.GetProperty("DeliveryAddressId")?.SetValue(deliveryAddress, newId);

            if (deliveryAddress.DeliveryAddressDefault == true)
            {
                await _deliveryAddressRepository.RemoveDefaultAddressAsync(deliveryAddress.UserId);
            }
            var result = await _baseRepository.InsertAsync(deliveryAddress);
            return result;

        }

        public async Task UpdateDeleliveryAddressDefalutAsync(Guid id, DeliveryAddress deliveryAddress)
        {
            await _deliveryAddressRepository.UpdateDeleliveryAddressDefalutAsync(id, deliveryAddress);
        }

        public override async Task<int> UpdateServiceAsync(Guid id, string dataJson, IFormFile? imageFile)
        {
            var deliveryAddress = JsonConvert.DeserializeObject<DeliveryAddress>(dataJson);
            await ValidateBeforeInsert(deliveryAddress);

            if (deliveryAddress.DeliveryAddressDefault == true)
            {
                await _deliveryAddressRepository.RemoveDefaultAddressAsync(deliveryAddress.UserId);
            }
            var result = await _baseRepository.UpdateAsync(id,deliveryAddress);
            return result; ;
        }
        #endregion
    }
}
