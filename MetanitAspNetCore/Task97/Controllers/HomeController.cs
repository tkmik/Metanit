using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task97.Models;

namespace Task97.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ITimeService _timeService;

        public HomeController(ILogger<HomeController> logger, 
            IWebHostEnvironment appEnvironment,
            ITimeService timeService)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _timeService = timeService;
        }

        public IActionResult GetFile()
        {
            //Path
            string filePath = Path.Combine(_appEnvironment.ContentRootPath, 
                "Files/PDFs/Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf");
            //Type
            string fileType = "application/pdf";
            //Name of File
            string fileName = "Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf";

            return PhysicalFile(filePath, fileType, fileName);
        }

        public IActionResult GetBytes()
        {
            string filePath = Path.Combine(_appEnvironment.ContentRootPath, 
                "Files/PDFs/Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(filePath);
            string fileType = "application/pdf";
            string fileName = "Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf";

            return File(mas, fileType, fileName);
        }

        public FileResult GetStream()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, 
                "Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/octet-stream";
            string file_name = "Andrew Troelsen. Pro C# 5.0 and the .NET 4.5 Framework.pdf";

            return File(fs, file_type, file_name);
        }

        public string Index()
        {
            return _timeService.Time;
        }

        //public void Index()
        //{            
        //    string table = "";
        //    foreach (var header in Request.Headers)
        //    {
        //        table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
        //    }
        //    Response.WriteAsync($"<table>{table}</table>");
        //}

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
