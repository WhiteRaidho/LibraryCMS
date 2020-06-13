using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/borrows")]
    public class BorrowsController : ControllerBase
    {
        protected BorrowsService Borrows { get; }
        protected IMapper Mapper { get; }
        protected UsersService Users { get; }
        protected BooksService Books { get; }
        protected RolesService Roles { get; }

        public BorrowsController(BorrowsService borrowsService, IMapper mapper, UsersService usersService, BooksService booksService, RolesService rolesService)
        {
            Roles = rolesService;
            Books = booksService;
            Users = usersService;
            Mapper = mapper;
            Borrows = borrowsService;
        }

        [HttpGet("{borrowId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BorrowFormModel>> Fetch(int borrowId)
        {
            var entity = Borrows.GetBorrow(borrowId);

            if (entity == null)
                return NotFound();

            return Mapper.Map<BorrowFormModel>(entity);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]BorrowFormModel model)
        {
            var entity = Mapper.Map<Borrow>(model);
            var user = Users.GetUser(model.UserId);
            var librarian = Users.GetUser(User.Identity.Name);
            var book = Books.GetBook(model.BookId);

            if (book == null || user == null || librarian == null)
                return NotFound();

            if (!Roles.IsLibrarian(librarian.UserID) && !librarian.IsAdmin)
                Unauthorized();
            entity.Book = book;
            entity.Librarian = librarian;
            entity.User = user;
            entity.ReturnLibrarian = null;

            Borrows.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { borrowId = entity.BorrowId }, Mapper.Map<BorrowFormModel>(entity));
        }
    }
}
