using AutoMapper;
using BookShopOnline.Core.Dto.Publisher;
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
    public class PublisherService : BaseService<Publisher, PublisherDto>, IPublisherService
    {
        readonly IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, IMapper mapper, IImageService imageService) : base(publisherRepository, mapper, imageService)
        {
            _mapper = mapper;
        }

        //public override PublisherDto MapEntityToDto(Publisher publisher)
        //{
        //    var pulisherDto = _mapper.Map<PublisherDto>(publisher);
        //    return pulisherDto;
        //}
    }
}
