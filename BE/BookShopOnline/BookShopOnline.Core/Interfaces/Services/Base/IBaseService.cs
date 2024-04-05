using BookShopOnline.Core.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services.Base
{
    public interface IBaseService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
        Task<int> InsertServiceAsync(string dataJson, IFormFile? imageFile);
        Task<int> UpdateServiceAsync(Guid id, string dataJson, IFormFile? imageFile);
        Task<int> DeleteServiceAsync(Guid id);
        Task<int> DeleteManyServiceAsync(List<Guid> ids);
    }
}
