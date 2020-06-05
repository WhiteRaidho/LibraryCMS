using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/location")]
    public class LocationController : ControllerBase
    {
        protected LocationService Location { get; }
        protected IMapper Mapper { get; }

        public LocationController(LocationService locationService, IMapper mapper)
        {
            Location = locationService;
            Mapper = mapper;
        }

        [HttpGet("{locationId}/location")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LocationViewModel>> GetLocation(int locationId)
        {
            var entity = Location.GetLocation(locationId);

            if (entity == null)
                return NotFound();

            return Mapper.Map<LocationViewModel>(entity);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LocationFormModel>> Fetch(int locationId)
        {
            var entity = Location.GetLocation(locationId);

            if (entity == null)
            {
                return NotFound();
            }

            return Mapper.Map<LocationFormModel>(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] LocationFormModel model)
        {
            var entity = Mapper.Map<Location>(model);

            Location.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { locationId = entity.LocationId }, Mapper.Map<LocationFormModel>(entity));

        }


        [HttpPut("{locationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]LocationFormModel model, int locationId)
        {
            var entity = Location.GetLocation(locationId);

            if (entity == null)
                return NotFound();

            Location.Update(entity, model);

            return Accepted();
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int locationId)
        {
            var entity = Location.GetLocation(locationId);

            if (entity == null)
                return NotFound();

            Location.Remove(entity);

            return Accepted();
        }
    }
}
