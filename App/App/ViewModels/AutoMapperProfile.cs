using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
            UserProfile();
            BookProfile();
            LibraryProfile();
        }
        protected void UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }

        protected void BookProfile()
        {
            CreateMap<Book, BookListItemViewModel>();
            CreateMap<Book, BookViewModel>();
        }

        protected void LibraryProfile()
        {
            CreateMap<Library, LibraryListItemModel>()
                .ForMember(d => d.LocationName, o => o.MapFrom(s => s.Location.Name))
                .ForMember(d => d.LocationStreet, o => o.MapFrom(s => s.Location.Street));
        }
    }

    
}
