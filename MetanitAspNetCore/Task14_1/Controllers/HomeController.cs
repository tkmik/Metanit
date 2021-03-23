using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task14_1.Models;

namespace Task14_1.Controllers
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



        IEnumerable<Company> companies = new List<Company>
        {
            new Company { Id = 1, Name = "Tesla" },
            new Company { Id = 2, Name = "BMW" },
            new Company { Id = 3, Name = "Mercedes" }
        };

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        public string Create(Car car)
        {
            Company company = companies.FirstOrDefault(c => c.Id == car.CompanyId);
            return $"{company.Name} {car.Brand}";
        }
    }
}
