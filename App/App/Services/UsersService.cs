using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class UsersService : BaseService
    {
        public UsersService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
        public User GetUser(string id)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserID == id);
            return user;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await Context.Users.FindAsync(id);
            return user;
        }

        public User Authenticate(string username, string password)
        {
            var user = Context.Users.SingleOrDefault(x => x.UserName == username && x.UserPassword == password);
            if (user == null) return null;

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

            return user;
        }
    }
}
