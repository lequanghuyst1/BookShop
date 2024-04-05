using BookShopOnline.Core.Dto.Image;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IImageService 
    {
        Task<IEnumerable<ImageDto>> GetAllAsync();
        Task<ImageDto?> GetByEnityIdAsync(Guid id);
        Task<int> InsertServiceAsync(Image image, IFormFile? imageFile);
        Task<int> UpdateServiceAsync(Guid id, Image image, IFormFile? imageFile);
        Task<int> DeleteServiceAsync(Guid id);
        Task<int> DeleteManyServiceAsync(List<Guid> ids);
    }
}
