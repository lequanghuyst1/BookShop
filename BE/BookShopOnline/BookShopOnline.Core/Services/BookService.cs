using AutoMapper;
using BookShopOnline.Core.Dto.Book;
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
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper) : base(bookRepository)
        {
            _mapper = mapper;

        }

        public override BookDto MapEntityToDto(Book book)
        {
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
    }
}
