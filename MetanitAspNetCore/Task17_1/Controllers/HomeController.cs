using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task17_1.Models;

namespace Task17_1.Controllers
{
    public class HomeController : Controller
    {
        private Context db;

        public HomeController(Context context)
        {
            db = context;
        }
        [HttpGet]        
        public async Task<IActionResult> Index(int? company, string name, int page = 1, 
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 2;
            //Filtering
            IQueryable<User> users = db.Users;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(n => n.Name.Contains(name));
            }
            //Sorting
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(sort => sort.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(sort => sort.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(sort => sort.Age);
                    break;
                default:
                    users = users.OrderBy(sort => sort.Name);
                    break;
            }
            //Pagination
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel()
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name),
                Users = items
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
                if (!(user is null))
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
                if (!(user is null))
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
                if (!(user is null))
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id != null)
        //    {
        //        User user = new User { Id = id.Value };
        //        db.Entry(user).State = EntityState.Deleted;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return NotFound();
        //}
    }
}
