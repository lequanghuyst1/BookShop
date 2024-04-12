using AutoMapper;
using BookShopOnline.Core.Dto.Book;
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
    public class BookService : BaseService<Book, BookDto>, IBookService
    {

        IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository, IMapper mapper, IImageService imageService) : base(bookRepository, mapper, imageService)
        {
            _bookRepository = bookRepository;
        }

        public override async Task ValidateBeforeInsert(Book book)
        {
            if(await _bookRepository.CheckDuplicateCodeAsync(book.BookCode))
            {
                errors.Add("BookCode", new string[] { $"Mã sách <{book.BookCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException($"Mã sách <{book.BookCode}> đã tồn tại trong hệ thống.", errors);
            }
            if(book.BookName == "" || book.BookName == null)
            {
                errors.Add("BookName", new string[] { "Tên sách không được để trống" });
                throw new ValidateException("Tên sách không được để trống", errors);
            }
            if (book.Author == "" || book.Author == null)
            {
                errors.Add("Author", new string[] { "Tên tác giả không được để trống" });
                throw new ValidateException("Tên tác giả không được để trống", errors);
            }

        }
    }
}
