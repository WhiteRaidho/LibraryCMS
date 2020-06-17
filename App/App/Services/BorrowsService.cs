using App.Models;
using App.ViewModels;
using AutoMapper;
using LinqKit;
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

        public List<Borrow> GetList(string search)
        {
            var predicate = PredicateBuilder.New<Borrow>(true);

            if (!String.IsNullOrEmpty(search))
            {
                var splited = search.Split(' ');
                foreach (string str in splited)
                {
                    predicate.And(p => p.Book.Title.ToLower().Contains(str.ToLower()));
                    predicate.Or(p => p.Book.AuthorFullName.ToLower().Contains(str.ToLower()));
                    predicate.Or(p => p.User.LastName.ToLower().Contains(str.ToLower()));
                }
            }

            var list = Context.Borrows
                .Include(x => x.Book)
                .Include(x => x.User)
                .Include(x => x.Librarian)
                .Include(x => x.ReturnLibrarian)
                .Where(x => x.Status != BorrowStatus.Inactive)
                .AsExpandable()
                .Where(predicate)
                .ToList();

            return list;
        }

        public Borrow GetBorrow(int borrowId)
        {
            var entity = Context.Borrows
                .Include(x => x.Book)
                .Include(x => x.User)
                .Include(x => x.Librarian)
                .Include(x => x.ReturnLibrarian)
                .FirstOrDefault(x => x.BorrowId == borrowId);

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