using AutoMapper;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services.Base
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
        protected IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseReadOnlyService(IBaseRepository<TEntity> baseRepository , IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities  = await _baseRepository.GetAllAsync();
            var res = entities.Select(entity => MapEntityToDto(entity));
            return res;
        }

        public async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            var res = MapEntityToDto(entity);
            return res;
        }

        public async Task<PagingEntity<TDto>> GetFilterPagingAsync(string? searchString, int pageSize, int PageNumber)
        {
            var entity = await _baseRepository.GetFilterPagingAsync(searchString, pageSize, PageNumber);
            var entitiesData = entity.Data;
            var res = new PagingEntity<TDto>()
            {
                TotalPage = entity.TotalPage,
                TotalRecord = entity.TotalRecord,
                Data = entitiesData.Select(entity => MapEntityToDto(entity)),
            };
            return res;
        }

        public async Task<string> GetNewCodeAsync()
        {
            var res = await _baseRepository.GetNewCodeAsync();
            return res;
        }

        public TDto MapEntityToDto(TEntity entity)
        {
            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }

        public async Task<IEnumerable<TDto>> GetByIdsAsync(List<Guid> ids)
        {
            var res = await _baseRepository.GetByIdsAsync(ids);
            var entitiesDto = res.Select(item => MapEntityToDto(item));
            return entitiesDto;
        }

        public async Task<TDto> GetBySlugAsync(string slug)
        {
            var entity = await _baseRepository.GetBySlugAsync(slug);
            var res = MapEntityToDto(entity);
            return res;
        }
    }
}
