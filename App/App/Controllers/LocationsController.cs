using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        protected LocationsService Locations { get; }
        protected IMapper Mapper { get; }

        public LocationsController(LocationsService locationsService, IMapper mapper)
        {
            Locations = locationsService;
            Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<LocationFormModel>>> GetList()
        {
            var entity = Locations.GetList();

            if (entity == null)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<LocationFormModel>>(entity));
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LocationFormModel>> Fetch(int locationId)
        {
            var entity = Locations.GetLocation(locationId);

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

            Locations.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { locationId = entity.LocationId }, Mapper.Map<LocationFormModel>(entity));

        }


        [HttpPut("{locationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]LocationFormModel model, int locationId)
        {
            var entity = Locations.GetLocation(locationId);

            if (entity == null)
                return NotFound();

            Locations.Update(entity, model);

            return Accepted();
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int locationId)
        {
            var entity = Locations.GetLocation(locationId);

            if (entity == null)
                return NotFound();

            Locations.Remove(entity);

            return Accepted();
        }
    }
}
