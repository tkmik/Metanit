using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task24_1.Models;
using Task24_1.Services;

namespace Task24_1.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;
        public HomeController(UserService service)
        {
            userService = service;
            userService.Initialize();
        }   
        public async Task<IActionResult> Index(int id)
        {
            User user = await userService.GetUser(id);
            if (!(user is null))
                return Content($"User: {user.Name}");
            return Content("User not found");
        }
    }
}
