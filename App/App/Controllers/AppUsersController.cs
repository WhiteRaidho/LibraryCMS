using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAppUser()
        {
            return await _context.Users.ToListAsync();
        }

        //[HttpGet]
        //public IEnumerable<AppUser> GetAppUsers()
        //{
        //    return _context.AppUser.ToList();
        //}

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAppUser(string id)
        {
            var AppUser = await _context.Users.FindAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }

            return AppUser;
        }

        // PUT: api/AppUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(string id, User AppUser)
        {
            if (id != AppUser.UserID)
            {
                return BadRequest();
            }

            _context.Entry(AppUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppUsers
        [HttpPost]
        public async Task<ActionResult<User>> PostAppUser(User AppUser)
        {
            _context.Users.Add(AppUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = AppUser.UserID }, AppUser);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteAppUser(string id)
        {
            var AppUser = await _context.Users.FindAsync(id);
            if (AppUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(AppUser);
            await _context.SaveChangesAsync();

            return AppUser;
        }

        private bool AppUserExists(string id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
