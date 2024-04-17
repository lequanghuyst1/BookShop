using AutoMapper;
using BookShopOnline.Core.Dto.ProductReview;
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
    public class ProductReviewService : BaseService<ProductReview, ProductReviewDto>, IProductReviewService
    {
        IProductReviewRepository _repository;
        public ProductReviewService(IProductReviewRepository productReviewRepository, IMapper mapper, IImageService imageService) : base(productReviewRepository, mapper, imageService)
        {
            _repository = productReviewRepository;
        }

        public async Task<IEnumerable<ProductReviewDto>> GetByBookIdAsync(Guid bookId)
        {
            var res = await _repository.GetByBookIdAsync(bookId);
            var resDto = res.Select(item => base.MapEntityToDto(item));
            return resDto;
        }
    }
}
