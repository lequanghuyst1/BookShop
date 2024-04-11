using AutoMapper;
using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Dto.Category;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Dto.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopOnline.Core.Dto.Image;
using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Dto.User;
using BookShopOnline.Core.Dto.DeliveryAddress;
using BookShopOnline.Core.Dto.Cart;
using BookShopOnline.Core.Dto.Order;

namespace BookShopOnline.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Book, BookDto>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<Image, ImageDto>();
            CreateMap<UserRegister, User>();
            CreateMap<User, UserDto>();
            CreateMap<DeliveryAddress, DeliveryAddressDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
        }
    }
}
