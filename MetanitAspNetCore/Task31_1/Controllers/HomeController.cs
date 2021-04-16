﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task31_1.Models;

namespace Task31_1.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository repo;
        public HomeController(IUserRepository rep)
        {
            repo = rep;
        }
        public IActionResult Index()
        {
            return View(repo.GetUsers());
        }
        public ActionResult Details(int id)
        {
            User user = repo.Get(id);
            if (user is not null)
            {
                return View(user);
            }
            return NotFound();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            repo.Create(user);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            User user = repo.Get(id);
            if (user is not null)
            {
                return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            repo.Update(user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            User user = repo.Get(id);
            if (user is not null)
            {
                return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
