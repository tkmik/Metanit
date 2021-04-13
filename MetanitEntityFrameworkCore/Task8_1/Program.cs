using System;
using System.Threading.Tasks;
using Task8_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Task8_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    User user = await context.Users.FirstOrDefaultAsync();
                    if (!(user is null))
                    {
                        user.Name = "Mikita";
                        await context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw;
                }
            }
        }
    }
}
