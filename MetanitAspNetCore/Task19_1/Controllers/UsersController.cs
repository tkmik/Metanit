using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task19_1.Models;

namespace Task19_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersContext db;
        public UsersController(UsersContext context)
        {
            db = context;

            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Nikita", Age = 10 });
                db.Users.Add(new User { Name = "Dasha", Age = 15 });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(search => search.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user is null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(search => search.Id == user.Id))
            {
                return NotFound();
            }

            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(search => search.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            db.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
