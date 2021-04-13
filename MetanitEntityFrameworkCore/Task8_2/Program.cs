using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task8_2.Models;

namespace Task8_2
{
    class Program
    {
        private static Func<AppDbContext, int, User> userById =
             EF.CompileQuery((AppDbContext db, int id) => 
                db.Users.Include(c => c.Company).FirstOrDefault(c => c.Id == id));

        private static Func<AppDbContext, string, int, IEnumerable<User>> usersByNameAndAge =
            EF.CompileQuery((AppDbContext db, string name, int age) =>
                    db.Users.Include(c => c.Company)
                            .Where(u => EF.Functions.Like(u.Name, name) && u.Age > age));

        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Company microsoft = new Company { Name = "Microsoft" };
                Company google = new Company { Name = "Google" };
                db.Companies.AddRange(microsoft, google);

                User tom = new User { Name = "Tom", Age = 30, Company = microsoft };
                User bob = new User { Name = "Bob", Age = 26, Company = google };
                User alice = new User { Name = "Alice", Age = 34, Company = microsoft };
                User kate = new User { Name = "Kate", Age = 47, Company = google };
                User tomas = new User { Name = "Tomas", Age = 35, Company = microsoft };
                User tomek = new User { Name = "Tomek", Age = 25, Company = google };

                db.Users.AddRange(tom, bob, alice, kate, tomas, tomek);
                db.SaveChanges();
            }
            using (AppDbContext db = new AppDbContext())
            {
                var user = userById(db, 1);
                Console.WriteLine($"{user.Name} - {user.Age} \n");

                var users = usersByNameAndAge(db, "%Tom%", 30).ToList();
                foreach (var u in users)
                    Console.WriteLine($"{u.Name} - {u.Age}");
            }
        }
    }
}
