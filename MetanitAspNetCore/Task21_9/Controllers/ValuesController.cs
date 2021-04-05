using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task21_9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Your Login: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Your role: admin");
        }
    }
}
