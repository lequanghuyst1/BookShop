using AutoMapper;
using BookShopOnline.Core.Dto.Publisher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Resources;
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
        IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository, IMapper mapper, IImageService imageService) : base(publisherRepository, mapper, imageService)
        {
            _mapper = mapper;
            _publisherRepository = publisherRepository; 
        }

        //public override PublisherDto MapEntityToDto(Publisher publisher)
        //{
        //    var pulisherDto = _mapper.Map<PublisherDto>(publisher);
        //    return pulisherDto;
        //}

        public async override Task ValidateBeforeInsert(Publisher publisher)
        {
            //categoryCode không hợp lệ ném ra exception
            if (await _publisherRepository.CheckDuplicateCodeAsync(publisher.PublisherCode))
            {
                errors.Add("PublisherCode", new string[] { $"Mã NXB <{publisher.PublisherCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
            //categoryCode không hợp lệ ném ra exception

            if (await _publisherRepository.CheckExitEntityNameAsync(publisher.PublisherName))
            {
                errors.Add("PublisherName", new string[] { $"Nhà xuất bản <{publisher.PublisherName}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }

        public override async Task ValidateBeforeUpdate(Publisher publisher)
        {
            //publihserCode không hợp lệ ném ra exception
            if (!await _publisherRepository.CheckEntityCodeUpdateAsync(publisher.PublisherCode, publisher.PublisherId))
            {
                errors.Add("PublisherCode", new string[] { $"Mã NXB <{publisher.PublisherCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }

            //publisherName không hợp lệ ném ra exception
            if (!await _publisherRepository.CheckEntityNameUpdateAsync(publisher.PublisherName, publisher.PublisherId))
            {
                errors.Add("PublisherName", new string[] { $"Nhà xuất bản <{publisher.PublisherName}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }
    }
}
