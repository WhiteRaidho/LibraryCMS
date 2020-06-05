using App.Models;
using App.ViewModels;
using AutoMapper;

namespace App.Services
{
    public class LocationService : BaseService
    {
        public LocationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
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
