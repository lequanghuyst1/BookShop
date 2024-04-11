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
    public class OrderService : BaseService<Order, OrderDto>, IOrderService
    {
        IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IImageService imageService, IUnitOfWork unitOfWork) : base(orderRepository, mapper, imageService)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<OrderDto> InsertManyServiceAsync(List<Order> orders)
        {
            throw new NotImplementedException();
        }
    }
}
