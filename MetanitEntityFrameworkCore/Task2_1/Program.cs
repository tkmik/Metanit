using Task2_1.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                User user = new User { Name = "Mik", Age = 10 };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
