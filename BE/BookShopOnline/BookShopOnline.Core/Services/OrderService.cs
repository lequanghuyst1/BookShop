using AutoMapper;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Enums;
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

        public async Task<Order> CheckoutServiceAsync(OrderData orderData)
        {
            _unitOfWork.BeginTransaction();
            var order = orderData.Order;
            var ordersDetail = orderData.OrderDetails;
            var cartItemIds = orderData.CartItemIds;

            var orderId = Guid.NewGuid();
            order.OrderId = orderId;
            order.OrderCode = await GetOrderCode();
            order.OrderStatus = (OrderStatus)order.OrderStatus;
            order.DeliveryStatus = (DeliveryStatus)order.DeliveryStatus;
            order.DeliveryMethod = (DeliveryMethod)order.DeliveryMethod;
            order.PaymentMethod = (PaymentMethod)order.PaymentMethod;
            order.PaymentStatus = (PaymentStatus)order.PaymentStatus;

            var res = await _unitOfWork.Order.InsertAsync(order);
            var count = 0;
            if (res > 0)
            {
                //cập nhật lại số lượng voucher
                if(orderData.Voucher?.VoucherCode != null)
                {
                    var voucherInDb = await _unitOfWork.Voucher.GetByVoucherCode(orderData.Voucher.VoucherCode);
                    voucherInDb.Quantity -= 1;
                    await _unitOfWork.Voucher.UpdateAsync( voucherInDb.VoucherId, voucherInDb);
                }
                //var result = await _unitOfWork.OrderDetail.InsertManyAsync(ordersDetail);
                foreach (var item in ordersDetail)
                {
                    item.OrderId = orderId;
                    var result = await _unitOfWork.OrderDetail.InsertAsync(item);
                    var book = await _unitOfWork.Book.GetByIdAsync(item.BookId);
                    book.QuantityInStock -= item.Quantity;
                    if(book.QuantitySold == null)
                    {
                        book.QuantitySold = 0;
                    }
                    book.QuantitySold += item.Quantity;
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
                        return order;
                    }
                    _unitOfWork.Rollback();
                }
            }

            return order;

        }

       

        public async Task<int> CancelOrderAsync(OrderData orderData)
        {
            var order = orderData.Order;
            var ordersDetail = orderData.OrderDetails;
            order.OrderStatus = OrderStatus.CANCELLED;
            order.CancellationDate = DateTime.Now;
            _unitOfWork.BeginTransaction();
            var res = await _unitOfWork.Order.UpdateAsync(order.OrderId, order);
            if(res > 0)
            {
                //cập nhật lại số lượng tồn tại trong kho và số lương bán của từng sản phẩm
                foreach(var item in ordersDetail)
                {
                    var book = await _unitOfWork.Book.GetByIdAsync(item.BookId);
                    book.QuantityInStock += item.Quantity;
                    book.QuantitySold -= item.Quantity;
                    var resUpdate = await _unitOfWork.Book.UpdateAsync(book.BookId, book);
                }
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

        public async Task<int> ConfirmAllAsync()
        {
            _unitOfWork.BeginTransaction();
            var res = await _unitOfWork.Order.ConfirmAllAsync();
            if (res > 0)
            {
                _unitOfWork.Commit();
                return res;
            }
            else return res;
        }
    }
}
