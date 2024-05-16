using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class VourcherRepository : BaseRepository<Vourcher>, IVourcherRepository
    {
        public VourcherRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
