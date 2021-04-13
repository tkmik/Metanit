using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Task28_1.Models;
using Task28_1.ViewModels;

namespace Task28_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;
        //private readonly IStringLocalizer<HomeController> _localizer;
        //private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(IStringLocalizer localizer/*, IStringLocalizer<SharedResource> sharedLocalizer*/)
        {
            _localizer = localizer;
            //_sharedLocalizer = sharedLocalizer;
        }
        public string Test()
        {
            string message = _localizer["Message"];
            return message;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["Header"];
            ViewData["Message"] = _localizer["Message"];
            return View();
        }
        public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }
            return $"CurrentCulrute:{CultureInfo.CurrentCulture.Name}/CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Content("Model was added");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
    }
}
