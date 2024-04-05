using BookShopOnline.Core.Dto.Image;
using BookShopOnline.Core.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image?> GetByEnityIdAsync(Guid entityId);
        Task<int> InsertAsync(Image image, IFormFile? imageFile);
        Task<int> UpdateAsync(Guid id,Image image, IFormFile? imageFile);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
