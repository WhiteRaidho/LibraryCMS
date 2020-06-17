using App.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .Where(r => r.Book.Title == book.Title && r.Book.AuthorFullName == book.AuthorFullName);

            if(rating.Any())
                return rating.Average(r => (float)r.Rate);
            return 0;
        }

        public bool IsReviewed(string authorFullName, string bookTitle, string userID)
        {
            var entity = Context.Reviews
                .Include(b => b.Book)
                .Include(u => u.User)
                .Where(r => r.User.UserID == userID && r.Book.AuthorFullName == authorFullName && r.Book.Title == bookTitle)
                .FirstOrDefault();

            return !(entity == null);
        }

        public double GetAvgUserRate(string userId)
        {
            var entity = Context.Reviews
                .Include(x => x.User)
                .Where(x => x.User.UserID == userId);
            
            if(entity.Any())
                return entity.Average(r => (float)r.Rate);

            return 0;
        }
    }
}
