using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Services
{
    public class BorrowsService : BaseService
    {


        public BorrowsService(ApplicationDbContext context, IMapper mapper ) : base(context, mapper)
        {
        }

        public Borrow GetBorrow(int bookId)
        {
            var entity = Context.Borrows
                .Include(x => x.Book)
                .FirstOrDefault(x => x.Book.BookId == bookId);

            return entity;
        }

        public Borrow GetUserBorrows(string userId)
        {
            var entity = Context.Borrows
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.UserID == userId);

            return entity;
        }
    }
}