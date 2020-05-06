﻿using System;
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

namespace App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected UsersService Users { get; }

        public UsersController(IMapper mapper, UsersService usersService)
        {
            Mapper = mapper;
            Users = usersService;
        }

        #region GetMe()
        // GET: api/Users/me
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserViewModel>> GetMe()
        {
            var me = Users.GetUser("admin"); // TODO Get user by id from auth
            if (me == null) return NotFound();

            var result = Mapper.Map<UserViewModel>(me);
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
            return Ok(user);
        }
        #endregion

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(string id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(string id, User user)
        //{
        //    if (id != user.UserID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteUser(string id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        //private bool UserExists(string id)
        //{
        //    return _context.Users.Any(e => e.UserID == id);
        //}
    }
}
