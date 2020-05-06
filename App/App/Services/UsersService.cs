using App.Models;
using App.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class UsersService : BaseService
    {
        public UsersService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
        public User GetUser(string id)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserID == id);
            return user;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await Context.Users.FindAsync(id);
            return user;
        }

    }
}
