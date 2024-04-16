using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> GetByCategoryIdAsync(string id);
        Task<IEnumerable<Book>> GetByCategorySlugAsync(string categorySlug);
        Task<PagingEntity<Book>> FilterAsync(int pageSize,int pageNumber, int sortType);


    }
}
