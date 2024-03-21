using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services.Base
{
    public interface IBaseReadOnlyService<TDto> 
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<string> GetNewCodeAsync();
    }
}
