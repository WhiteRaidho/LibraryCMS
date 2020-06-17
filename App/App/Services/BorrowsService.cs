﻿using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class BorrowsService : BaseService
    {


        public BorrowsService(ApplicationDbContext context, IMapper mapper ) : base(context, mapper)
        {
        }

        public List<Borrow> GetList()
        {
            var list = Context.Borrows
                .Include(x => x.Book)
                .Include(x => x.User)
                .Include(x => x.Librarian)
                .Include(x => x.ReturnLibrarian)
                .ToList();

            return list;
        }

        public Borrow GetBorrow(int bookId)
        {
            var entity = Context.Borrows
                .Include(x => x.Book)
                .FirstOrDefault(x => x.Book.BookId == bookId);

            return entity;
        }

        public bool IsBorrowed(string authorFullName, string bookTitle, string userId)
        {
            var entity = Context.Borrows
                .Include(x => x.User)
                .Include(x => x.Book)
                .FirstOrDefault(x => x.User.UserID == userId && x.Book.Title == bookTitle && x.Book.AuthorFullName == authorFullName);

            return entity != null;
        }

        public int GetUserInactiveBooksCount(String userId)
        {
            var entity = Context.Borrows
                .Where(x => x.User.UserID == userId && x.Status == BorrowStatus.Inactive)
                .Count();

            return entity;
        }
    }
}