using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task26_1.Models;

namespace Task26_1.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController()
        {

        }
        public HomeController(IRepository r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            //ViewData["Message"] = "Hello!";
            //return View("Index");
            return View(repo.GetAll());
        }
        public IActionResult GetUser(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            User user = repo.Get(id.Value);
            if (user is null)
                return NotFound();
            return View(user);
        }

        public IActionResult AddUser() => View();

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                repo.Create(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
