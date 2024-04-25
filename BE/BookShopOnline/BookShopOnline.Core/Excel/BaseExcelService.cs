using AutoMapper;
using BookShopOnline.Core.Interfaces.Excel;
using BookShopOnline.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Excel
{
    public abstract class BaseExcelService<TEntity, TDto> : ExcelService<TDto>, IBaseExcelService<TEntity>
    {
         IBaseRepository<TEntity> _baseRepository;
        protected IMapper _mapper;

        public BaseExcelService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> ExportAllAsync()
        {
            var data = await _baseRepository.GetAllAsync();
            var dataEnities = data.Select(item => MapEntityToDto(item));
            var columns = GetColumns();
            var bytes = ExportExcelAsync(dataEnities, columns);
            return bytes;
        }

        public async Task<byte[]> ExportListAsync(List<Guid> ids)
        {
            var data = await _baseRepository.GetByIdsAsync(ids);
            var columns = GetColumns();
            var dataEnities = data.Select(item => MapEntityToDto(item));
            var bytes = ExportExcelAsync(dataEnities, columns);
            return bytes;
        }

        public TDto MapEntityToDto(TEntity entity)
        {
            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }
        public abstract List<String> GetColumns();
    }
}
