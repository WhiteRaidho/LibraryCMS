﻿using App.Models;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
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

        public List<Book> GetBooks(string search, string authorFullName, int? lib)
        {
            var predicate = PredicateBuilder.New<Book>(true);

            if(!String.IsNullOrEmpty(search))
            {
                var splited = search.Split(' ');
                foreach (string str in splited)
                {
                    predicate.And(p => p.Title.ToLower().Contains(str.ToLower()));
                    predicate.Or(p => p.AuthorFullName.ToLower().Contains(str.ToLower()));
                }
            }
            if(!String.IsNullOrEmpty(authorFullName))
            {
                predicate.And(p => p.AuthorFullName.ToLower().Contains(authorFullName.ToLower()));
            }

            if(lib != null && lib > 0)
            {
                predicate.And(p => p.Library.LibraryId == lib);
            }

            var books = Context.Books
                //.Include(b => b.Library)
                .AsExpandable()
                .Where(predicate)
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

        //public List<Book> GetBooks()
        //{
        //    var books = Context.Books
        //        .GroupBy(b => new { b.Title, b.AuthorName, b.AuthorSurname})
        //        .Select(g => g.First())
        //        .ToList();
        //    return books;
        //}

        //public List<Book> GetBooks(int LibraryID)
        //{
        //    var library = Find<Library>(LibraryID);
        //    var books = Context.Books
        //        .Where(b => b.Library == library)
        //        .GroupBy(b => new { b.Title, b.AuthorName, b.AuthorSurname })
        //        .Select(g => g.First())
        //        .ToList();
        //    return books;
        //}
    }
}
