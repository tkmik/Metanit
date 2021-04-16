using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task30_1.Controllers
{
    public class HomeController : Controller
    {
        [EnableCors("AllowAllOrigin")]
        public IActionResult Index()
        {
            return Content("Hello from Controller");
        }
    }
}
