using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    }
}
