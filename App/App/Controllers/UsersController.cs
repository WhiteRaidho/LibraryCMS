using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using App.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected UsersService Users { get; }
        protected BorrowsService Borrows { get; }
        protected ReviewsService Reviews { get; }
        protected RolesService Roles { get; }

        public UsersController(IMapper mapper, UsersService usersService, BorrowsService borrowsService, ReviewsService reviewsService, RolesService rolesService)
        {
            Borrows = borrowsService;
            Reviews = reviewsService;
            Mapper = mapper;
            Users = usersService;
            Roles = rolesService;
        }

        #region GetList()
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetList()
        {
            if (!Roles.IsAdmin(User.Identity.Name)) return Forbid();

            var list = Users.GetList();
            if (list == null) return NotFound();

            var result = Mapper.Map<IEnumerable<UserViewModel>>(list);
            return Ok(result);
        }
        #endregion

        #region GetMe()
        // GET: api/Users/me
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserViewModel>> GetMe()
        {
            var me = Users.GetUser(User.Identity.Name);
            if (me == null) return NotFound();

            var result = Mapper.Map<UserViewModel>(me);

            result.IsLibrarian = Roles.IsLibrarian(me.UserID);

            return (ActionResult<UserViewModel>)result;
        }
        #endregion

        #region Authenticate()
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = Users.Authenticate(model.Username, model.Password);
            if (user == null) return BadRequest(new { message = "Username or password is incorrect" });

            var token = CreateToken(user);
            return Ok(token);
        }

        private TokenViewModel CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fwiwdaaunykelhmeeegaoribyynrihotlwppcnimxvmevxbzmmesprmqogdtnwpbdzpajwgshkzmdlnarihjwhwqqjjhojoouaedbnwympcqpvfaumhgymexrcqkcfmgijrcyhjvbvumugdofutxvoggrejkoaiyzyspfhsfoysmcsyeyyhvlnrxswpbrqzozhmeatcavdhqhdfzgfbewnntgigxdkuahtpipswhiodarupccumlltsfibfbcher");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return new TokenViewModel()
            {
                Token = user.Token,
                Refresh = Users.GetRefreshToken(user),
                Expires = token.ValidTo
            };
        }
        #endregion

        #region RefreshToken()
        [HttpGet("authenticate/refresh")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenViewModel>> RefreshToken([FromHeader(Name = "Authorization")]string authorization)
        {
            if(!String.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var jwtVal = authorization.Replace("Bearer ", String.Empty);
                var jwt = new JwtSecurityTokenHandler().ReadToken(jwtVal) as JwtSecurityToken;

                if(jwt != null)
                {
                    string userId = jwt.Claims.ToList()[0].Value;
                    User user = Users.GetUser(userId);

                    if (user != null) return CreateToken(user);
                }
            }
            return BadRequest(new { message = "Bad authorization method" });
        }

        [AllowAnonymous]
        [HttpPost("authenticate/recover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenViewModel>> RecoverToken([FromBody]TokenRequestViewModel model)
        {
            if(!String.IsNullOrEmpty(model.Token))
            {
                var user = Users.GetUserByRefreshToken(model.Token);

                if (user != null) return CreateToken(user);
            }

            return BadRequest(new { message = "Błędny token odświeżania" });
        }
        #endregion

        #region Register()
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            var user = Users.GetUserByName(model.UserName);
            if (user != null) return BadRequest(new { message = "Taki użytkownik już istnieje" });

            User u = Mapper.Map<User>(model);
                        
            user = Users.Create(u);

            if (user == null) return BadRequest(new { message = "Coś poszło nie tak" });

            return Ok();
        }
        #endregion

        #region GetPropertiesForProfile()
        [Authorize]
        [HttpGet("profileView")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProfileViewModel>> GetPropertiesForProfile()
        {
            ProfileViewModel entity = new ProfileViewModel();
            var booksCount = Borrows.GetUserInactiveBooksCount(User.Identity.Name);
            var avgRating = Reviews.GetAvgUserRate(User.Identity.Name);

            entity.BooksCount = booksCount;
            entity.AvgRatings = avgRating;

            return entity;
        }
        #endregion
    }
}
