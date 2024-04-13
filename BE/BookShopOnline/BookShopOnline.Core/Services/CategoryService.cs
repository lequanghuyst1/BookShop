using AutoMapper;
using BookShopOnline.Core.Dto.Category;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Resources;
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
        ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IImageService imageService) : base(categoryRepository, mapper, imageService)
        {
            _categoryRepository = categoryRepository;
        }

        public async override Task ValidateBeforeInsert(Category category)
        {
            //categoryCode không hợp lệ ném ra exception
            if (await _categoryRepository.CheckExitEntityNameAsync(category.CategoryName))
            {
                errors.Add("CategoryName", new string[] { $"Danh mục {category.CategoryName} đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }

        public override async Task ValidateBeforeUpdate(Category category)
        {
            //categoryCode không hợp lệ ném ra exception
            if(!await _categoryRepository.CheckEntityCodeUpdateAsync(category.CategoryCode, category.CategoryId)) {
                errors.Add("CategoryCode", new string[] { $"Mã danh mục <{category.CategoryCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }

            //categoryName không hợp lệ ném ra exception
            if (!await _categoryRepository.CheckEntityNameUpdateAsync(category.CategoryName, category.CategoryId))
            {
                errors.Add("CategoryName", new string[] { $"Danh mục <{category.CategoryName}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }
    }
}
