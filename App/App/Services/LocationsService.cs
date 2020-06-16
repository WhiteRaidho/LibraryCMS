using App.Models;
using App.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class LocationsService : BaseService
    {
        public LocationsService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Location> GetList()
        {
            return Context.Locations.ToList();
        }

        public Location GetLocation(int id)
        {
            return Context.Find<Location>(id);
        }

        public Location Update(Location location, LocationFormModel model)
        {
            Mapper.Map(model, location);

            Update(location);
            SaveChanges();

            return location;
        }
    }
}
