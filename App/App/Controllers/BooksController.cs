using App.Extensions;
using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api")]
    public class BooksController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected LibrariesService Libraries { get; }
        protected BooksService Books { get; }
        protected ReviewsService Reviews { get; }
        protected RolesService Roles { get; }

        public BooksController(IMapper mapper, LibrariesService librariesService, BooksService booksService, ReviewsService reviewsService, RolesService rolesService)
        {
            Mapper = mapper;
            Libraries = librariesService;
            Books = booksService;
            Reviews = reviewsService;
            Roles = rolesService;
        }

        [HttpGet("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BookListItemViewModel>>> GetBooks([FromQuery]string search, [FromQuery]string author, [FromQuery] int? lib)
        {
            if (lib != null && lib > 0)
            {
                var library = Libraries.GetLibrary((int)lib);
                if (library == null) return NotFound();
            }

            var books = Books.GetBooks(search, author, lib);
            var result = Mapper.Map<IEnumerable<BookListItemViewModel>>(books).ForEach(b => b.Copies = Books.GetBookCount(b.AuthorFullName, b.Title)).ToList();
            return (ActionResult<IEnumerable<BookListItemViewModel>>)result;
        }

        [HttpGet("avalibleBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<AvalibleBookListItemViewModel>>> GetAvalibleBooks([FromQuery]string search, [FromQuery]string author)
        {
            if (!Roles.IsAdmin(User.Identity.Name) && !Roles.IsLibrarian(User.Identity.Name)) return Forbid();

            var books = Books.GetAvalibleBooks(search, author);

            if (books == null)
                return NotFound();

            var result = Mapper.Map<IEnumerable<AvalibleBookListItemViewModel>>(books).ToList();
            return (ActionResult<IEnumerable<AvalibleBookListItemViewModel>>)result;
        }

        [HttpGet("books/{authorFullName}/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookViewModel>> GetBook(string authorFullName, string bookTitle)
        {
            var book = Books.GetBook(authorFullName, bookTitle);
            if (book == null) return NotFound();
            var result = Mapper.Map<BookViewModel>(book);
            result.AvgRating = Reviews.GetRatingForBook(book);
            return (ActionResult<BookViewModel>)result;
        }

        [HttpGet("admin/books/{authorFullName}/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookFormModel>> Fetch(string authorFullName, string bookTitle)
        {
            if (!Roles.IsLibrarian(User.Identity.Name) && !Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var entity = Books.GetBook(authorFullName, bookTitle);

            if (entity == null)
                return NotFound();

            var result = Mapper.Map<BookFormModel>(entity);
            var copies = Books.GetBookCopies(authorFullName, bookTitle);
            result.BookCopies = Mapper.Map<IEnumerable<BookCopieModel>>(copies)
                .ForEach(x => x.Library = Mapper.Map<LibraryListItemModel>(copies.First(c => c.BookId == x.BookId).Library)).ToList();

            return result;
        }

        [HttpPost("admin/books")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]BookFormModel model)
        {
            if (!Roles.IsLibrarian(User.Identity.Name) && !Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var entity = Mapper.Map<Book>(model);

            foreach (BookCopieModel copie in model.BookCopies)
            {
                if (copie.BookId != 0) continue;
                var library = Libraries.GetLibrary(copie.Library.LibraryId);
                if (library == null) return NotFound();
                Book book = new Book() {
                    AuthorName = entity.AuthorName,
                    AuthorSurname = entity.AuthorSurname,
                    Description = entity.Description,
                    Library = library,
                    Title = entity.Title
                };
                Books.Create(book);
            }

            var result = Mapper.Map<BookFormModel>(entity);
            var copies = Books.GetBookCopies(entity.AuthorFullName, entity.Title);
            result.BookCopies = Mapper.Map<IEnumerable<BookCopieModel>>(copies)
                .ForEach(x => x.Library = Mapper.Map<LibraryListItemModel>(copies.First(c => c.BookId == x.BookId).Library)).ToList();

            return CreatedAtAction(nameof(Fetch), new { authorFullName = entity.AuthorFullName, bookTitle = entity.Title }, result);
        }

        [HttpPut("admin/books")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]BookFormModel model)
        {
            if (!Roles.IsLibrarian(User.Identity.Name) && !Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var entity = Mapper.Map<Book>(model);

            foreach (BookCopieModel copie in model.BookCopies)
            {
                var library = Libraries.GetLibrary(copie.Library.LibraryId);
                if (library == null) return NotFound();
                if (copie.BookId != 0)
                {
                    Book book = Books.GetBook(copie.BookId);
                    book.Title = entity.Title;
                    book.AuthorName = entity.AuthorName;
                    book.AuthorSurname = entity.AuthorSurname;
                    book.Description = entity.Description;
                    Books.Update(book);
                }
                else
                {
                    Book book = new Book()
                    {
                        AuthorName = entity.AuthorName,
                        AuthorSurname = entity.AuthorSurname,
                        Description = entity.Description,
                        Library = library,
                        Title = entity.Title
                    };
                    Books.Create(book);
                }
            }

            var result = Mapper.Map<BookFormModel>(entity);
            var copies = Books.GetBookCopies(entity.AuthorFullName, entity.Title);
            result.BookCopies = Mapper.Map<IEnumerable<BookCopieModel>>(copies)
                .ForEach(x => x.Library = Mapper.Map<LibraryListItemModel>(copies.First(c => c.BookId == x.BookId).Library)).ToList();

            return CreatedAtAction(nameof(Fetch), new { authorFullName = entity.AuthorFullName, bookTitle = entity.Title }, result);
        }

        //[HttpPut("books/{bookId}")]
        //[ProducesResponseType(StatusCodes.Status202Accepted)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult> Update([FromBody]BookFormModel model, int bookId)
        //{
        //    var entity = Books.GetBook(bookId);
        //    var library = Libraries.GetLibrary(model.LibraryId);

        //    entity.Library = library;

        //    if (library == null)
        //        return NotFound();

        //    if (entity == null)
        //        return NotFound();

        //    Books.Update

        //    Books.Update(entity, model);

        //    return Accepted();
        //}

        [HttpDelete("admin/books/{bookId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int bookId)
        {
            if (!Roles.IsLibrarian(User.Identity.Name) && !Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var entity = Books.GetBook(bookId);

            if (entity == null)
                return NotFound();

            Books.Remove(entity);

            return Accepted();
        }
    }
}
