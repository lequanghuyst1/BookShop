using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.ProductReview;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class ReviewProductsController : MBaseController<ReviewProduct, ReviewProductDto>
    {
        IReviewProductService _productReviewService;

        #region Constructor
        public ReviewProductsController(IReviewProductService productReviewService) : base(productReviewService)
        {
            _productReviewService = productReviewService;
        } 
        #endregion

        /// <summary>
        /// Api thực hiện lấy danh sách các đánh giá theo bookId
        /// </summary>
        /// <param name="bookId">mã định danh của sách</param>
        /// <returns>
        /// danh sách các đánh giá
        /// </returns>
        /// Created By: LQHUY(17/04/2024)
        [HttpGet("GetByBookId")]
        public async Task<IActionResult> GetByBookIdAsync(Guid bookId)
        {
            var res = await _productReviewService.GetByBookIdAsync(bookId);
            return Ok(res);
        }
        
    }
}
