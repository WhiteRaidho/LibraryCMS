using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BorrowListItemModel>> GetList([FromQuery]string search)
        {
            if (!Roles.IsAdmin(User.Identity.Name) && !Roles.IsLibrarian(User.Identity.Name)) return Forbid();

            var list = Borrows.GetList(search);

            if (list == null)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<BorrowListItemModel>>(list));
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]BorrowFormModel model)
        {
            if (!Roles.IsAdmin(User.Identity.Name) && !Roles.IsLibrarian(User.Identity.Name)) return Forbid();

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

        [Authorize]
        [HttpPut("{borrowId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ReturnBook(int borrowId)
        {
            if (!Roles.IsLibrarian(User.Identity.Name) && !Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var entity = Borrows.GetBorrow(borrowId);

            if (entity == null)
                return NotFound();

            entity.Status = BorrowStatus.Inactive;
            entity.ReturnLibrarian = Users.GetUser(User.Identity.Name);
            entity.ReturnTime = DateTime.UtcNow;

            Borrows.Update(entity);

            return Accepted();
        }
    }
}
