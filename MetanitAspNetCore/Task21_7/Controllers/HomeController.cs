using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task21_7.Models;

namespace Task21_7.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Policy = "OnlyForLondon")]
        [Authorize(Policy = "AgeLimit")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "OnlyForMicrosoft")]
        public IActionResult About()
        {
            return Content("Only for Microsoft employees");
        }
    }
}
