using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public BorrowsController(BorrowsService borrowsService, IMapper mapper, UsersService usersService)
        {
            Users = usersService;
            Mapper = mapper;
            Borrows = borrowsService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BorrowFormModel>> Fetch(int id)
        {
            var entity = Borrows.GetBorrow(id);

            if (entity == null)
                return NotFound();

            return Mapper.Map<BorrowFormModel>(entity);
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult> Create([FromBody] BorrowFormModel model)
        //{
        //    var entity = Mapper.Map<Borrow>(model);
        //    entity.User = Users.GetUser(model.UserId);
        //    entity.Librarian = Users.

        //    Borrows.create(entity);

        //    return CreatedAtAction(nameof(Fetch), new { id = entity.BorrowId }, Mapper.Map<ReviewFormModel>(entity));
        //}
    }
}
