using BookShopOnline.Core.Dto.Book;
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
        public BookService(IBookRepository bookRepository) : base(bookRepository)
        {
        }

        public override BookDto MapEntityToDto(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
