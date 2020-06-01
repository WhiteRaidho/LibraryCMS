using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        protected BooksService Books { get;  }
        protected UsersService Users { get; }
        protected ReviewsService Reviews { get; }
        protected IMapper Mapper { get; }

        public ReviewsController(ReviewsService reviewsService, IMapper mapper, BooksService booksService, UsersService usersService)
        {
            Users = usersService;
            Books = booksService;
            Reviews = reviewsService;
            Mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] ReviewFormModel model)
        {
            var entity = Mapper.Map<Review>(model);
            
            entity.User = Users.GetUserWithBorrows(model.UserId);
            entity.Book = Books.GetBook(model.BookId);

            var isBorrowed = entity.User.Borrows.FirstOrDefault(b => b.Book.BookId == model.BookId);

            if (isBorrowed == null)
                return Forbid();

            Reviews.Create(entity);

            return CreatedAtAction(nameof(Fetch), new {id = entity.ReviewId}, Mapper.Map<ReviewFormModel>(entity));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ReviewFormModel>> Fetch (int id)
        {
            var entity = Reviews.GetReview(id);

            if (entity == null)
                return NotFound();

            return Mapper.Map<ReviewFormModel>(entity);
        }
    }
}
