﻿using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Order;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class OrderDetailsController : MBaseController<OrderDetail, OrderDetailDto>
    {
        public OrderDetailsController(IOrderDetailService orderDetailService) : base(orderDetailService)
        {
        }
    }
}
