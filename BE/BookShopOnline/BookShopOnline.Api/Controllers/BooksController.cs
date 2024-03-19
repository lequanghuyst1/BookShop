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

        public BooksController(IBookRepository bookRepository, IBookService bookService) : base(bookService)
        {
            _bookRepository = bookRepository;
        }

    }

}
