﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Entitites
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public int Email { get; set; }
        public string? Address { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? QuantityAmountPurched { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
