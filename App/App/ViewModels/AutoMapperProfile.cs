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
            ReviewProfile();
        }
        protected void UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<RegisterViewModel, User>()
                .ForMember(d => d.UserPassword, o => o.MapFrom(s => s.Password));
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

        protected void ReviewProfile()
        {
            CreateMap<Review, ReviewFormModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ReviewId))
                .ForMember(d => d.BookId, o => o.MapFrom(s => s.Book.BookId))
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.UserID))
                .ReverseMap()
                .ForMember(d => d.ReviewId, o=> o.Ignore());

            CreateMap<Review, ReviewViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ReverseMap();
        }

        protected void BorrowProfile()
        {
            CreateMap<Borrow, BorrowFormModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.UserID))
                .ForMember(d => d.LibrarianUserId, o => o.MapFrom(s => s.Librarian.UserID))
                .ForMember(d => d.ReturnLibrarianUserId, o => o.MapFrom(s => s.ReturnLibrarian.UserID))
                .ForMember(d => d.BookId, o => o.MapFrom(s => s.Book.BookId))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ReverseMap()
                .ForMember(d => d.BorrowId, o => o.Ignore());
        }
    }

    
}
