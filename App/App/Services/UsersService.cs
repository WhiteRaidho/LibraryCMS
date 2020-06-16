using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class UsersService : BaseService
    {
        IPasswordHasher<User> Hasher { get; }

        public UsersService(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher<User> hasher
            ) : base(context, mapper)
        {
            Hasher = hasher;
        }

        public List<User> GetList()
        {
            var list = Context.Users
                .ToList();

            return list;
        }

        #region GetUser()
        public User GetUser(string id)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserID == id);
            return user;
        }

        public User GetUserWithBorrows(string id)
        {
            var user = Context.Users
                .Include(x => x.Borrows)
                .ThenInclude(x => x.Book)
                .FirstOrDefault(u => u.UserID == id);
            return user;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await Context.Users.FindAsync(id);
            return user;
        }
        #endregion

        #region GetUserByName()
        public User GetUserByName(string username)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserName == username);
            return user;
        }
        #endregion


        public User Authenticate(string username, string password)
        {
            var user = Context.Users.SingleOrDefault(x => x.UserName == username && x.UserPassword == password);
            if (user == null) return null;

            return user;
        }

        public string GetRefreshToken(User user)
        {
            if (String.IsNullOrEmpty(user.RefreshToken))
            {
                user.RefreshToken = Hasher.HashPassword(user, Guid.NewGuid().ToString())
                    .Replace("+", string.Empty)
                    .Replace("=", string.Empty)
                    .Replace("/", string.Empty);

                Update(user);
            }

            return user.RefreshToken;
        }

        public User GetUserByRefreshToken(string token)
        {
            return Context.Users.SingleOrDefault(x => x.RefreshToken == token);
        }
    }
}
