using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class ReviewsService : BaseService
    {
        private BooksService Books { get; set; }

        public ReviewsService(ApplicationDbContext context, IMapper mapper, BooksService books) : base(context, mapper)
        {
            Books = books;
        }

        public Review GetReview(int id)
        {
            var entity = Context.Reviews
                .FirstOrDefault(r => r.ReviewId == id);

            return entity;
        }

        public List<Review> GetReviewsForBook(string authorFullName, string bookTitle)
        {
            var result = Context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.Book.Title == bookTitle && r.Book.AuthorFullName == authorFullName)
                .ToList();
            return result;
        }

        public float GetRatingForBook(Book book)
        {
            var rating = Context.Reviews
                .Include(r => r.Book)
                .Where(r => r.Book.Title == book.Title && r.Book.AuthorFullName == book.AuthorFullName)
                .Average(r => (float)r.Rate);
            return rating;
        }
    }
}
