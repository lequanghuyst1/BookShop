using AutoMapper;
using BookShopOnline.Core.Dto.Book;
using BookShopOnline.Core.Dto.Category;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Dto.Publisher;
using BookShopOnline.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Book, BookDto>();
            CreateMap<Publisher, PublisherDto>();
        }
    }
}
