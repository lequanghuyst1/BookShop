using AutoMapper;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
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

        public async Task<int> CheckoutServiceAsync(OrderData orderData)
        {
            _unitOfWork.BeginTransaction();
            var order = orderData.Order;
            var ordersDetail = orderData.OrderDetails;
            var cartItemIds = orderData.CartItemIds;
            var orderId = Guid.NewGuid();
            order.OrderId = orderId;
            order.OrderCode = await GetOrderCode();
            var res = await _unitOfWork.Order.InsertAsync(order);
            var count = 0;
            if (res > 0)
            {
                //var result = await _unitOfWork.OrderDetail.InsertManyAsync(ordersDetail);
                foreach (var item in ordersDetail)
                {
                    item.OrderId = orderId;
                    var result = await _unitOfWork.OrderDetail.InsertAsync(item);
                    var book = await _unitOfWork.Book.GetByIdAsync(item.BookId);
                    book.QuantityInStock = book.QuantityInStock - item.Quantity;
                    if(book.QuantitySold == null)
                    {
                        book.QuantitySold = 0;
                    }
                    book.QuantitySold = book.QuantitySold + item.Quantity;
                    await _unitOfWork.Book.UpdateAsync(book.BookId, book);
                    if (result > 0)
                    {
                        count = count + result;
                    }
                }
                if (count == ordersDetail.Count)
                {
                    var resultDelete = await _unitOfWork.CartItems.DeleteManyAsync(cartItemIds);
                    if (resultDelete > 0)
                    {
                        _unitOfWork.Commit();
                        return res;
                    }
                    _unitOfWork.Rollback();
                }
            }
            return 0;

        }

        public async Task<IEnumerable<OrderDto>> GetByUserId(Guid userId)
        {
            _unitOfWork.BeginTransaction();
            var orders = await _unitOfWork.Order.GetByUserId(userId);
            var ordersDto = orders.Select(item => base.MapEntityToDto(item));
            return ordersDto;
        }

        public async Task<int> CancelOrderAsync(Order order)
        {
            order.Status = "Đã hủy";
            order.CancellationDate = DateTime.Now;
            _unitOfWork.BeginTransaction();
            var res = await _unitOfWork.Order.UpdateAsync(order.OrderId, order);
            if(res > 0)
            {
                _unitOfWork.Commit();
                return res;
            }
            else
            {
                errors.Add("Order", new string[] { "Hủy đơn hàng không thành công" });
                throw new ValidateException(Resources.ResourceVN.Exception_Validate_Default, errors);
            }
            
        }

        private async Task<string> GetOrderCode()
        {
            Random rand = new Random();
            var orderCodeNew = "";
            do
            {
                orderCodeNew = rand.Next(100000000, 1000000000).ToString();
            }
            while (await _unitOfWork.Order.CheckDuplicateCodeAsync(orderCodeNew));
            return orderCodeNew;
        }
    }
}
