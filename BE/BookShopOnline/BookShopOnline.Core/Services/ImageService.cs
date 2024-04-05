using AutoMapper;
using BookShopOnline.Core.Dto.Image;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class ImageService : IImageService
    {
        IImageRepository _imageRepository;
        IMapper _mapper;
        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public Task<int> DeleteManyServiceAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteServiceAsync(Guid id)
        {
            var res = await _imageRepository.DeleteAsync(id);
            return res;
        }

        public async Task<IEnumerable<ImageDto>> GetAllAsync()
        {
            var res = await _imageRepository.GetAllAsync();
            var imagesDto = res.Select(image => MapEntityToDto(image));
            return imagesDto;
        }
        public ImageDto MapEntityToDto(Image image)
        {
            var imageDto = _mapper.Map<ImageDto>(image);
            return imageDto;
        }
        public async Task<ImageDto?> GetByEnityIdAsync(Guid id)
        {
            var res = await _imageRepository.GetByEnityIdAsync(id);
            var imageDto = _mapper.Map<ImageDto>(res);
            return imageDto;
        }

        public async Task<int> InsertServiceAsync(Image image, IFormFile? imageFile)
        {
            var res = await _imageRepository.InsertAsync(image, imageFile);
            return res;
        }

        public async Task<int> UpdateServiceAsync(Guid id, Image image, IFormFile? imageFile)
        {
            var res = await _imageRepository.UpdateAsync(id, image, imageFile);
            return res;
        }
    }
}
