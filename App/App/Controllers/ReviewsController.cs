﻿using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        protected BooksService Books { get;  }
        protected ReviewsService Reviews { get; }
        protected UsersService Users { get; }
        protected IMapper Mapper { get; }

        public ReviewsController(ReviewsService reviewsService, IMapper mapper, BooksService booksService, UsersService usersService)
        {
            Users = usersService;
            Books = booksService;
            Reviews = reviewsService;
            Mapper = mapper;
        }

        [Authorize]
        [HttpPost("{authorFullName}/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] ReviewFormModel model, string authorFullName, string bookTitle)
        {
            var entity = Mapper.Map<Review>(model);
            var reviews = Reviews.GetReviewsForBook(authorFullName, bookTitle);

            entity.User = Users.GetUserWithBorrows(User.Identity.Name);
            entity.Book = Books.GetBook(authorFullName, bookTitle);

            var isBorrowed = entity.User.Borrows.FirstOrDefault(b => b.Book.BookId == entity.Book.BookId);

            if (isBorrowed == null)
                return Forbid();

            if (Reviews.IsReviewed(entity.Book.AuthorFullName, entity.Book.Title, entity.User.UserID))
                return Conflict();

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

        [HttpGet("{authorFullName}/{bookTitle}")]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> GetReviewsForBook(string authorFullName, string bookTitle)
        {
            var list = Reviews.GetReviewsForBook(authorFullName, bookTitle);
            var result = Mapper.Map<IEnumerable<ReviewViewModel>>(list);
            return Ok(result);
        }
    }
}
