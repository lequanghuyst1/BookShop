using AutoMapper;
using BookShopOnline.Core.Dto.Vourcher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class VourcherService : BaseService<Vourcher, VourcherDto>, IVourcherService
    {
        public VourcherService(IVourcherRepository vourcherRepository, IMapper mapper, IImageService imageService) : base(vourcherRepository, mapper, imageService)
        {
        }
    }
}
