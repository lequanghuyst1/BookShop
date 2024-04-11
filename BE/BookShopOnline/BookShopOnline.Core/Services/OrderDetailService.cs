using AutoMapper;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
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
        public OrderDetailService(IOrderDetailRepository orderDetailRepository , IMapper mapper, IImageService imageService) : base(orderDetailRepository, mapper, imageService)
        {
        }

        public Task<OrderDetailDto> InsertManyServiceAsync(List<OrderDetail> orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}
