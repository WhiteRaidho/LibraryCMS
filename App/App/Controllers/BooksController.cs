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

        public BooksController(IMapper mapper, LibrariesService librariesService, BooksService booksService, ReviewsService reviewsService)
        {
            Mapper = mapper;
            Libraries = librariesService;
            Books = booksService;
            Reviews = reviewsService;
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
            var result = Mapper.Map<IEnumerable<BookListItemViewModel>>(books).ToList();
            return (ActionResult<IEnumerable<BookListItemViewModel>>)result;
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
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookFormModel>> Fetch(int bookId)
        {
            var entity = Books.GetBook(bookId);

            if (entity == null)
                return NotFound();

            return Mapper.Map<BookFormModel>(entity);
        }

        [HttpPost("books")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]BookFormModel model)
        {
            var entity = Mapper.Map<Book>(model);
            var library = Libraries.GetLibrary(model.LibraryId);

            entity.Library = library;

            if (library == null)
                return NotFound();

            Libraries.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { bookId = entity.BookId }, Mapper.Map<BookFormModel>(entity));
        }


        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]BookFormModel model, int bookId)
        {
            var entity = Books.GetBook(bookId);
            var library = Libraries.GetLibrary(model.LibraryId);

            entity.Library = library;

            if (library == null)
                return NotFound();

            if (entity == null)
                return NotFound();

            Books.Update(entity, model);

            return Accepted();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int bookId)
        {
            var entity = Books.GetBook(bookId);

            if (entity == null)
                return NotFound();

            Books.Remove(entity);

            return Accepted();
        }
    }
}
