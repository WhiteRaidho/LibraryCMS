using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class LibrariesService : BaseService
    {
        public LibrariesService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Library GetLibrary(int id)
        {
            return Find<Library>(id);
        }
    }
}
