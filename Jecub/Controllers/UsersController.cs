using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jecub;

namespace Jecub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Users/register
        [HttpPost("/register")]
        public async Task<IActionResult> Register(string login, string password)
        {
            _context.Users.Add(new User() {Login = login, Password = password });
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Users/login
        [HttpPost("/login")]
        public async Task<ActionResult<int>> Login(string login, string password)
        {

            var user = _context.Users.ToList().Find(e => e.Login == login && e.Password == password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Id);

        }

    }
}
