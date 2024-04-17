using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopOnline.Api.Controllers
{
    [ApiController]
    public class BooksController : MBaseController<Book, BookDto>
    {
        IBookRepository _bookRepository;
        IBookService _bookService;
        public BooksController(IBookRepository bookRepository, IBookService bookService) : base(bookService)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
        }
        /// <summary>
        /// Api thực hiện lấy ra danh sách các bản ghi theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        [HttpGet("GetByCategorySlug/{categorySlug}")]
        public async Task<IActionResult> GetByCategorySlug(string categorySlug)
        {
            var res = await _bookService.GetByCategorySlugAsync(categorySlug);
            return Ok(res);
        }

        /// <summary>
        /// Api thực hiện phân trang dữ liệu theo điều kiện lọc
        /// </summary>
        /// <param name="filter">các điều kiện lọc và phân trang</param>
        /// <returns>
        /// dánh sách các bản ghi
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        [HttpPost("Filter")]
        public async Task<IActionResult> FilterAsync(Filter filter)
        {
            var res = await _bookService.FilterAsync(filter);
            return Ok(res);
        }

        /// <summary>
        /// Api thực hiện lấy ra danh sách các tác giả theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các nhà cung cấp
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        [HttpGet("GetAuthor")]
        public async Task<IActionResult> GetAuthorAsync(string categorySlug)
        {
            var res = await _bookRepository.GetAuthorByCategorySlugAsync(categorySlug);
            return Ok(res);
        }

        /// <summary>
        /// Api thực hiện lấy ra danh sách các nhà cung cấp theo categorySlug
        /// </summary>
        /// <param name="categorySlug">categorySlug</param>
        /// <returns>
        /// danh sách các nhà cung cấp
        /// </returns>
        /// Created By: LQHUY(16/04/2024)
        [HttpGet("GetPublisher")]
        public async Task<IActionResult> GetPublisherAsync(string categorySlug)
        {
            var res = await _bookRepository.GetPublisherByCategorySlugAsync(categorySlug);
            return Ok(res);
        }

    }

}
