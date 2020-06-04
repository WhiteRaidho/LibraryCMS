using App.Services;
using App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        protected LibrariesService Libraries { get; }
        protected IMapper Mapper { get; }


        public LibrariesController(LibrariesService librariesService, IMapper mapper)
        {
            Libraries = librariesService;
            Mapper = mapper;
        }

        [HttpGet("libraries")]
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

        [HttpGet("libraries/{id}")]
        public async Task<ActionResult<LibraryListItemModel>> GetLibrary(int id)
        {
            var lib = Libraries.GetLibrary(id);

            if (lib == null) return NotFound();

            var result = Mapper.Map<LibraryListItemModel>(lib);
            return Ok(result);
        }
    }
}


