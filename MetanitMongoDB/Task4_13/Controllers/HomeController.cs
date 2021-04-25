using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task4_13.Models;

namespace Task4_13.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        //public HomeController(AppDbContext context)
        //{
        //    db = context;
        //}
        public async Task<IActionResult> Index(ComputerFilter filter)
        {
            var computers = await db.GetComputersAsync(filter.Year, filter.ComputerName);
            var model = new ComputerList
            {
                Computers = computers,
                Filter = filter
            };
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Computer computer)
        {
            if (ModelState.IsValid)
            {
                await db.CreateAsync(computer);
                return RedirectToAction("Index");
            }
            return View(computer);
        }
        public async Task<IActionResult> Edit(string id)
        {
            Computer computer = await db.GetComputerAsync(id);
            if (computer is null)
            {
                return NotFound();
            }
            return View(computer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Computer computer)
        {
            if (ModelState.IsValid)
            {
                await db.UpdateAsync(computer);
                return RedirectToAction("Index");
            }
            return View(computer);
        }
        public async Task<IActionResult> Delete(string id)
        {
            await db.RemoveAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AttachImage(string id)
        {
            Computer computer = await db.GetComputerAsync(id);
            if (computer is null)
            {
                return NotFound();
            }
            return View(computer);
        }
        [HttpPost]
        public async Task<IActionResult> AttachImage(string id, IFormFile uploadedFile)
        {
            if (uploadedFile is not null)
            {
                await db.StoreImageAsync(id, uploadedFile.OpenReadStream(), uploadedFile.FileName);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> GetImage(string id)
        {
            var image = await db.GetImageAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }
    }
}
