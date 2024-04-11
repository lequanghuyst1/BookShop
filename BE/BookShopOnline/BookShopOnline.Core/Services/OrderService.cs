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
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IImageService imageService, IUnitOfWork unitOfWork ) : base(orderRepository, mapper, imageService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CheckoutServiceAsync(OrderData orderData)
        {
            _unitOfWork.BeginTransaction();
            var order = orderData.Order;
            var ordersDetail = orderData.OrderDetails;
            var cartItemIds = orderData.CartItemIds;
            var orderId = Guid.NewGuid();
            var res = await _unitOfWork.Order.InsertAsync(order);
            var count = 0;
            if(res > 0)
            {
                //var result = await _unitOfWork.OrderDetail.InsertManyAsync(ordersDetail);
                foreach(var item in ordersDetail)
                {
                    item.OrderId = orderId;
                    var result = await _unitOfWork.OrderDetail.InsertAsync(item);
                    count = count + result;
                }
                if(count == ordersDetail.Count)
                {
                    var resultDelete = await _unitOfWork.CartItems.DeleteManyAsync(cartItemIds);
                    if(resultDelete > 0)
                    {
                        _unitOfWork.Commit();
                        return res;
                    }
                    _unitOfWork.Rollback();
                }
            }
            return 0;

        }
    }
}
