using BookShopOnline.Api.Controllers.Base;
using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
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
        [HttpGet("GetByCategorySlug/{categorySlug}")]
        public async Task<IActionResult> GetByCategorySlug(string categorySlug)
        {
            var res = await _bookService.GetByCategorySlugAsync(categorySlug);
            return Ok(res);
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> FilterAsync(int pageSize, int pageNumber, int sortType)
        {
            var res = await _bookService.FilterAsync(pageSize, pageNumber, sortType);
            return Ok(res);
        }
    }

}
