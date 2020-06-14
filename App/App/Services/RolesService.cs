using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class RolesService : BaseService
    {
        public RolesService(IMapper mapper, ApplicationDbContext context) : base(context, mapper)
        {
        }

        public Role GetRole(int roleId)
        {
            var entity = Context.Roles
                .Include(x => x.Library)
                .Include(y => y.User)
                .FirstOrDefault(z => z.RoleId == roleId);

            return entity;
        }

        public Role Update(Role entity, RoleFormModel model)
        {
            Mapper.Map(model, entity);

            Update(entity);
            SaveChanges();

            return entity;
        }

        public List<Role> GetUserRoles(string userId)
        {
            var result = Context.Roles
                .Include(u => u.User)
                .Include(l => l.Library)
                .Where(x => x.User.UserID == userId)
                .ToList();

            return result;
        }

        public List<Role> GetUserRoles(string userId, int libraryId)
        {
            var result = Context.Roles
                .Include(u => u.User)
                .Include(l => l.Library)
                .Where(x => x.User.UserID == userId && x.Library.LibraryId == libraryId)
                .ToList();

            return result;
        }

        public bool IsLibrarian(string userId)
        {
            var result = Context.Roles
                .Include(u => u.User)
                .Where(x => x.User.UserID == userId && x.UserRole == UserRole.Librarian)
                .FirstOrDefault();

            return result != null;
        }

        public bool IsAdmin(string userId)
        {
            var result = Context.Users
                .Where(x => x.UserID == userId)
                .FirstOrDefault();
            if (result == null) return false;
            return result.IsAdmin;
        }
    }
}
