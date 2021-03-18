using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task10_1.Models;

namespace Task10_1.Controllers
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
            //ViewData["Message"] = "Hello from controller!";
            //ViewBag.Message = "This is home controller";
            return View(new List<string>() {"1","2","3" });
        }

        [HttpGet]
        public IActionResult Log()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Log(string name, int age)
        {
            string data = $"{name} - {age}";
            return Content(data);
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
    }
}
