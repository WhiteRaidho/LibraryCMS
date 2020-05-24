using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class BooksService : BaseService
    {

        public BooksService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Book GetBook(int id)
        {
            var book = Find<Book>(id);
            return book;
        }

        public List<Book> GetBooks(string authorFullName)
        {
            var books = Context.Books
                .Where(b => b.AuthorFullName == authorFullName)
                .GroupBy(b => new { b.Title, b.AuthorName, b.AuthorSurname })
                .Select(g => g.First())
                .ToList();
            return books;
        }

        public Book GetBook(string authorFullName, string bookTitle)
        {
            var book = Context.Books
                .Where(b => b.AuthorFullName == authorFullName)
                .Where(b => b.Title == b.Title)
                .FirstOrDefault();
            return book;
        }

        public List<Book> GetBooks()
        {
            var books = Context.Books
                .GroupBy(b => new { b.Title, b.AuthorName, b.AuthorSurname})
                .Select(g => g.First())
                .ToList();
            return books;
        }

        public List<Book> GetBooks(int LibraryID)
        {
            var library = Find<Library>(LibraryID);
            var books = Context.Books
                .Where(b => b.Library == library)
                .GroupBy(b => new Book { Title = b.Title, AuthorName = b.AuthorName, AuthorSurname = b.AuthorSurname })
                .Select(g => g.First())
                .ToList();
            return books;
        }
    }
}
