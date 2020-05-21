using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using AutoMapper;
using App.Services;
using App.ViewModels;

namespace App.Controllers
{
    [ApiController]
    [Route("api")]
    public class BooksController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected LibrariesService Libraries { get; }
        protected BooksService Books { get; }

        public BooksController(IMapper mapper, LibrariesService librariesService, BooksService booksService)
        {
            Mapper = mapper;
            Libraries = librariesService;
            Books = booksService;
        }

        // GET: api/Books
        [HttpGet("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BookListItemViewModel>>> GetBooks()
        {
            var books = Books.GetBooks();
            var result = Mapper.Map<IEnumerable<BookListItemViewModel>>(books).ToList();
            return (ActionResult<IEnumerable<BookListItemViewModel>>)result;
        }

        [HttpGet("{libraryId}/books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BookListItemViewModel>>> GetBooks(int libraryId)
        {
            var lib = Libraries.GetLibrary(libraryId);
            if (lib == null) return NotFound();

            var books = Books.GetBooks(libraryId);
            var result = Mapper.Map<IEnumerable<BookListItemViewModel>>(books).ToList();
            return (ActionResult<IEnumerable<BookListItemViewModel>>)result;
        }

        [HttpGet("books/{authorName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BookListItemViewModel>>> GetBooks(string authorName)
        {
            var books = Books.GetBooks(authorName);
            var result = Mapper.Map<IEnumerable<BookListItemViewModel>>(books).ToList();
            return (ActionResult<IEnumerable<BookListItemViewModel>>)result;
        }

        [HttpGet("books/{authorName}/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookViewModel>> GetBook(string authorName, string bookTitle)
        {
            var book = Books.GetBook(authorName, bookTitle);
            var result = Mapper.Map<BookViewModel>(book);
            return (ActionResult<BookViewModel>)result;
        }
        

        //// PUT: api/Books/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Books
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        //}

        //// DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Book>> DeleteBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return book;
        //}

        //private bool BookExists(int id)
        //{
        //    return _context.Books.Any(e => e.BookId == id);
        //}
    }
}
