﻿using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Dto.Cart
{
    public class CartDto : BaseEntity
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
    }
}
