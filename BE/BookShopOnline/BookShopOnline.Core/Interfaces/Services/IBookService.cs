using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Services
{
    public interface IBookService : IBaseService<Book, BookDto>
    {
        Task<IEnumerable<BookDto>> GetByCategorySlugAsync(string categorySlug);
    }
}
