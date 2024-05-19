using BookShopOnline.Core.Dto.Vourcher;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        Task<Voucher> GetByVoucherCode(string voucherCode);

    }
}
