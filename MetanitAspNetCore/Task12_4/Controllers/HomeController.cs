using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task12_4.Models;

namespace Task12_4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }   
        [HttpGet]
        public IActionResult GetData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetData([FromForm] string[] items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += item + " ";
            }
            return Content(result);
        }
        [HttpPost]
        public IActionResult GetPhone([FromQuery] Phone myPhone)
        {
            return Content($"Name:{myPhone?.Name}");
        }
    }
}
