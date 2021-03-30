using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Task18_9.Models;

namespace Task18_9.Pages.People
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Person> People { get; set; }
        public IndexModel(AppDbContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
            People = _context.People.AsNoTracking().ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.People.FindAsync(id);
            if (!(product is null))
            {
                _context.People.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
