using App.Models;
using AutoMapper;

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
            LocationProfile();
            RoleProfile();
        }
        #region UserProfile
        protected void UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<RegisterViewModel, User>()
                .ForMember(d => d.UserPassword, o => o.MapFrom(s => s.Password));
        }
        #endregion

        #region BookProfile
        protected void BookProfile()
        {
            CreateMap<Book, BookListItemViewModel>();
            CreateMap<Book, BookViewModel>()
                .ForMember(d => d.AvgRating, o => o.Ignore());
            CreateMap<Book, BookFormModel>()
                .ForMember(d => d.LibraryId, o => o.MapFrom(s => s.Library.LibraryId))
                .ReverseMap()
                .ForMember(d => d.BookId, o => o.Ignore());
        }
        #endregion

        #region LibraryProfile
        protected void LibraryProfile()
        {
            CreateMap<Library, LibraryListItemModel>()
                .ForMember(d => d.LocationName, o => o.MapFrom(s => s.Location.Name))
                .ForMember(d => d.LocationStreet, o => o.MapFrom(s => s.Location.Street));
            CreateMap<Library, LibraryFormModel>()
                .ForMember(d => d.LocationId, o => o.MapFrom(s => s.Location.LocationId))
                .ReverseMap()
                .ForMember(d => d.LibraryId, o => o.Ignore());
        }
        #endregion

        #region ReviewProfile
        protected void ReviewProfile()
        {
            CreateMap<Review, ReviewFormModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ReviewId))
                .ReverseMap()
                .ForMember(d => d.ReviewId, o => o.Ignore());

            CreateMap<Review, ReviewViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ReverseMap();
        }
        #endregion

        #region BorrowProfile
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
        #endregion

        #region LocationProfile
        protected void LocationProfile()
        {
            CreateMap<Location, LocationViewModel>();
            CreateMap<Location, LocationFormModel>()
                .ReverseMap()
                .ForMember(d => d.LocationId, o => o.Ignore());
        }
        #endregion

        #region RoleProfile
        protected void RoleProfile()
        {
            CreateMap<Role, RoleFormModel>()
                .ForMember(d => d.LibraryId, o => o.MapFrom(s => s.Library.LibraryId))
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.UserID))
                .ForMember(d => d.UserRole, o => o.MapFrom(s => s.UserRole))
                .ReverseMap()
                .ForMember(d => d.RoleId, o => o.Ignore());
        }
        #endregion
    }


}
