using AutoMapper;
using BookShopOnline.Core.Dto.Category;
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
    public class CategoryService : BaseService<Category, CategoryDto>, ICategoryService
    {
        readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository)
        {
            _mapper = mapper;
        }
        public override CategoryDto MapEntityToDto(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;

        }
    }
}
