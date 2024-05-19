using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Voucher?> GetByVoucherCode(string voucherCode)
        {
            var sqlCommand = "select * from view_voucher where VoucherCode = @VoucherCode";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Voucher>(sqlCommand, new { VoucherCode  = voucherCode }, _dbContext.Transaction
                );
            return res;
        }
    }
}
