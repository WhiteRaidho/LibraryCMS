using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class LibrariesService : BaseService
    {
        public LibrariesService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Library GetLibrary(int id)
        {
            var result = Context.Libraries.Include(x => x.Location).FirstOrDefault(y => y.LibraryId == id);
            return result;
        }

        public IEnumerable<Library> GetList()
        {
            var temp = Context.Libraries.Include(x => x.Location).ToList();

            return temp;
        }

        public Library Update(Library entity, LibraryFormModel model)
        {
            Mapper.Map(model, entity);

            Update(entity);
            SaveChanges();

            return entity;
        }
    }
}
