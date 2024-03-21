using AutoMapper;
using BookShopOnline.Core.Dto.Category;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
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
        ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public override CategoryDto MapEntityToDto(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;

        }
        public async override Task ValidateBeforeInsert(Category category)
        {
            if (await _categoryRepository.CheckExitEntityNameAsync(category.CategoryName))
            {
                Dictionary<string, string[]>? errors = new Dictionary<string, string[]>();
                errors.Add("categoryName", new string[] { $"Danh mục {category.CategoryName} đã tồn tại trong hệ thống!" });
                throw new ValidateException(System.Net.HttpStatusCode.BadRequest, $"Danh mục{category.CategoryName} đã tồn tại trong hệ thống", errors);
            }
        }
    }
}
