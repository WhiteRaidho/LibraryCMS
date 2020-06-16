using App.Models;
using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        protected LocationsService Locations { get; }
        protected LibrariesService Libraries { get; }
        protected IMapper Mapper { get; }


        public LibrariesController(LibrariesService librariesService, IMapper mapper, LocationsService locationsService)
        {
            Locations = locationsService;
            Libraries = librariesService;
            Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<LibraryListItemModel>>> GetList()
        {
            var list = Libraries.GetList();
            var result = Mapper.Map<IEnumerable<LibraryListItemModel>>(list).ToList();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{libraryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LibraryFormModel>> Fetch(int libraryId)
        {
            var entity = Libraries.GetLibrary(libraryId);

            if (entity == null)
                return NotFound();

            return Mapper.Map<LibraryFormModel>(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]LibraryFormModel model)
        {
            var entity = Mapper.Map<Library>(model);
            var location = Locations.GetLocation(model.LocationId);

            if (location == null)
                return NotFound();

            entity.Location = location;

            Libraries.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { libraryId = entity.LibraryId }, Mapper.Map<LibraryFormModel>(entity));
        }

        [HttpPut("{libraryId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]LibraryFormModel model, int libraryId)
        {
            var entity = Libraries.GetLibrary(libraryId);
            var location = Locations.GetLocation(model.LocationId);
            entity.Location = location;

            if (location == null)
                return NotFound();

            if (entity == null)
                return NotFound();

            Libraries.Update(entity, model);

            return Accepted();
        }

        [HttpDelete("{libraryId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int libraryId)
        {
            var entity = Libraries.GetLibrary(libraryId);

            if (entity == null)
                return NotFound();

            Libraries.Remove(entity);

            return Accepted();
        }
    }
}


