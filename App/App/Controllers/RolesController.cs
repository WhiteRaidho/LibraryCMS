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
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected RolesService Roles { get; }
        protected LibrariesService Libraries { get; }
        protected UsersService Users { get; }

        public RolesController(IMapper mapper, RolesService rolesService, LibrariesService librariesService, UsersService usersService)
        {
            Users = usersService;
            Mapper = mapper;
            Roles = rolesService;
            Libraries = librariesService;
        }

        [HttpGet("{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RoleFormModel>> Fetch(int roleId)
        {
            var entity = Roles.GetRole(roleId);

            if (entity == null)
                return NotFound();

            return Mapper.Map<RoleFormModel>(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]RoleFormModel model)
        {
            var entity = Mapper.Map<Role>(model);
            var user = Users.GetUser(model.UserId);
            var library = Libraries.GetLibrary(model.LibraryId);

            if (user == null)
                return NotFound();

            entity.User = user;

            if (library == null)
                return NotFound();

            entity.Library = library;

            Libraries.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { roleId = entity.RoleId }, Mapper.Map<RoleFormModel>(entity));
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]RoleFormModel model, int roleId)
        {
            var entity = Roles.GetRole(roleId);
            var user = Users.GetUser(model.UserId);
            var library = Libraries.GetLibrary(model.LibraryId);

            if (entity == null)
                return NotFound();

            if (user == null)
                return NotFound();

            if (library == null)
                return NotFound();

            entity.User = user;
            entity.Library = library;

            Roles.Update(entity, model);

            return Accepted();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int roleId)
        {
            var entity = Roles.GetRole(roleId);

            if (entity == null)
                return NotFound();

            Libraries.Remove(entity);

            return Accepted();
        }
    }
}
