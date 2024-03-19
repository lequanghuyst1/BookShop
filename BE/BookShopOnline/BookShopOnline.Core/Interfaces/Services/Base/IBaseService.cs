﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services.Base
{
    public interface IBaseService<TEntity, TDto> : IBaseReadOnlyService<TDto> 
    {
        Task<int> InsertServiceAsync(TEntity entity);
        Task<int> UpdateServiceAsync(TEntity entity, Guid id);
        Task<int> DeleteServiceAsync(Guid id);
        Task<int> DeleteManyServiceAsync(List<Guid> ids);
    }
}
