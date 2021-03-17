using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Task91.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Hello(int id)
        {
            return $"id = {id}";
        }

        public IActionResult PersonInfo(string firstName, string secondName)
        {
            return Content($"Hi {firstName} {secondName}");
        }

        public IActionResult Index()
        {
            return ViewBag;
        }

        public JsonResult TestJson()
        {
            Person person = new Person
            {
                FirstName = "Mikita",
                SecondName = "Tkach"
            };

            return Json(person);
        }

        public IActionResult Redirect()
        {
            return Redirect("~/Home/About");
            //return RedirectPermanent(~/Home/About);
            //return RedirectToRoute("default");
            //return RedirectToAction("Home", "TestJson");
        }
        public IActionResult StatusCode()
        {
            return StatusCode(401);
            //return NotFound(); //404
            //return Unauthorized(); //401
            //return BadRequest(); //400
            //return Ok(); //200
        }

        class Person
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
        }


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
