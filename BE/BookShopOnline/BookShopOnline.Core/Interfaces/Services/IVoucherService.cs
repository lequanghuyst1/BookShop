using BookShopOnline.Core.Dto.Vourcher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IVoucherService : IBaseService<Voucher, VoucherDto>
    {
        Task<VoucherDto> ApplyVoucher(string voucherCode, decimal totalProductCost);
    }
}
