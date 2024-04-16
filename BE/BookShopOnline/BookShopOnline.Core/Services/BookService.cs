using AutoMapper;
using BookShopOnline.Core.Dto.Book;
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
    public class BookService : BaseService<Book, BookDto>, IBookService
    {

        IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository, IMapper mapper, IImageService imageService) : base(bookRepository, mapper, imageService)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PagingEntity<BookDto>> FilterAsync(int pageSize, int pageNumber, int sortType)
        {
            var res = await _bookRepository.FilterAsync(pageSize, pageNumber, sortType);
            var pagingEntityDto = new PagingEntity<BookDto>
            {
                TotalPage = res.TotalPage,
                TotalRecord = res.TotalRecord,
                Data = res.Data.Select(item => base.MapEntityToDto(item))
            };
            return pagingEntityDto;

        }

        public async Task<IEnumerable<BookDto>> GetByCategorySlugAsync(string categorySlug)
        {
            var books = await _bookRepository.GetByCategorySlugAsync(categorySlug);
            var booksDto = books.Select(item => base.MapEntityToDto(item));
            return booksDto;
        }

        public override async Task ValidateBeforeInsert(Book book)
        {
           
            //if(book.BookName == "" || book.BookName == null)
            //{
            //    errors.Add("BookName", new string[] { "Tên sách không được để trống." });
            //    throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            //}
            //if (book.Author == "" || book.Author == null)
            //{
            //    errors.Add("Author", new string[] { "Tên tác giả không được để trống." });
            //    throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            //}

            //validate nghiệp vụ
            //bookCode không hợp lệ ném ra exception
            if (await _bookRepository.CheckDuplicateCodeAsync(book.BookCode))
            {
                errors.Add("BookName", new string[] { $"Mã sách <{book.BookCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }

            //bookName không hợp lệ ném ra exception
            if (await _bookRepository.CheckExitEntityNameAsync(book.BookName))
            {
                errors.Add("BookName", new string[] { $"Tên sách <{book.BookName}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }

        public override async Task ValidateBeforeUpdate(Book book)
        {
            //bookCode không hợp lệ ném ra exception
            if (!await _bookRepository.CheckEntityCodeUpdateAsync(book.BookCode, book.BookId))
            {
                errors.Add("BookCode", new string[] { $"Mã sách <{book.BookCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }

            //bookName không hợp lệ ném ra exception
            if (!await _bookRepository.CheckEntityNameUpdateAsync(book.BookName,book.BookId))
            {
                errors.Add("BookName", new string[] { $"Tên sách <{book.BookName}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }
    }
}
