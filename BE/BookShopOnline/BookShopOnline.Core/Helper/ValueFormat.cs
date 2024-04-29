using BookShopOnline.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Helper
{
    public static class ValueFormat
    {
        public static string GetOrderStatusFormat(OrderStatus orderSatus)
        {
            switch (orderSatus)
            {
                case OrderStatus.WAIT_FOR_CONFIRMATION:
                    return "Chờ xác nhận";
                case OrderStatus.CONFIRMED:
                    return "Đã xác nhận";
                case OrderStatus.PROCESSING:
                    return "Đang xử lý";
                case OrderStatus.COMPLETE:
                    return "Hoàn thành";
                case OrderStatus.CANCELLED:
                    return "Đã hủy";
                default:
                    return "";
            }
        }
        public static string GetDeliveryStatusFormat(DeliveryStatus deliveryStatus)
        {
            switch (deliveryStatus)
            {
                case DeliveryStatus.NOT_DELIVERY:
                    return "Chưa giao hàng";
                case DeliveryStatus.BEING_TRANSPORTED:
                    return "Đang vận chuyển";
                case DeliveryStatus.DELIVERIED:
                    return "Đã giao";
                case DeliveryStatus.CANCELLED:
                    return "Đã hủy";
                default:
                    return "";
            }
        }

        public static string GetPaymentStatusFormat(PaymentStatus paymentStatus)
        {
            switch (paymentStatus)
            {
                case PaymentStatus.UNPAID:
                    return "Chưa thanh toán";
                case PaymentStatus.WAIT_FOR_HANDLE:
                    return "Chờ xử lý";
                case PaymentStatus.PAID:
                    return "Đã thanh toán";
                default:
                    return "";
            }
        }

        public static string GetDeliveryMethodFormat(DeliveryMethod deliveryMethod)
        {
            switch (deliveryMethod)
            {
                case DeliveryMethod.LOCAL_DELIVERY:
                    return "Giao hàng tận nơi";
                default:
                    return "";
            }
        }
        public static string GetPaymentMethodFormat(PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.COD:
                    return "COD";
                case PaymentMethod.VNPAY:
                    return "Thanh toán bằng VNPAY";
                default:
                    return "";
            }
        }
    }
}
