using AutoMapper;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.UnitOfWork;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailDto>, IOrderDetailService
    {
        IUnitOfWork _unitOfWork;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper, IImageService imageService,  IUnitOfWork unitOfWork) : base(orderDetailRepository, mapper, imageService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetByOrderIdAsync(Guid orderId)
        {
            var orderDetails = await _unitOfWork.OrderDetail.GetByOrderIdAsync(orderId);
            var orderDetailsDto = orderDetails.Select(item => base.MapEntityToDto(item));
            return orderDetailsDto;
        }

        public Task<OrderDetailDto> InsertManyServiceAsync(List<OrderDetail> orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}
