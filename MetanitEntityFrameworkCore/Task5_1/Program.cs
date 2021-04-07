using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Task5_1.Models;

namespace Task5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Company google = new Company { Name = "google" };
                Company apple = new Company { Name = "apple" };
                db.Companies.AddRange(google, apple);

                User tom = new User { Name = "Tom", Age = 30, Company = apple };
                User bob = new User { Name = "Bob", Age = 25, Company = google };
                User alice = new User { Name = "Alice", Age = 36, Company = apple };
                User kate = new User { Name = "Kate", Age = 48, Company = google };

                db.Users.AddRange(tom, bob, alice, kate);
                db.SaveChanges();
            }
            //getting users by Id = 1
            using (AppDbContext db = new AppDbContext())
            {
                var users1 = from user in db.Users.Include(c => c.Company)
                             where user.CompanyId == 1
                             select user;
                var users2 = db.Users.Include(c => c.Company).Where(u => u.CompanyId == 1);

                Console.WriteLine("\nThe first request");
                foreach (var user in users1)
                    Console.WriteLine($"{user.Name}({user.Age}) - {user.Company.Name}");
                Console.WriteLine("\nThe second request");
                foreach (var user in users2)
                    Console.WriteLine($"{user.Name}({user.Age}) - {user.Company.Name}");
            }
            //getting users Where CompanyName is google
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nWhere company name is 'google'");
                var users1 = from user in db.Users
                             where user.Company.Name == "google"
                             select user;
                var users2 = db.Users.Where(c => c.Company.Name == "google");
                foreach (var user in users1)
                {
                    Console.WriteLine($"{user.Name}({user.Age})");
                }
            }
            //making query with Like
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nUsing Like");
                var users1 = db.Users.Where(n => EF.Functions.Like(n.Name, "%T%"));
                foreach (var user in users1)
                {
                    Console.WriteLine($"{user.Name}({user.Age})");
                }
                var users2 = db.Users.Where(u => EF.Functions.Like(u.Age.ToString(), "4[1-9]"));
            }
            //data projection
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\ndata projection");
                var users = db.Users.Select(u => new
                {
                    Name = u.Name,
                    Age = u.Age,
                    Company = u.Company.Name
                });
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name}({user.Age}) - {user.Company}");
                }
            }
            //using order
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing order");
                var users1 = from user in db.Users
                             orderby user.Name, user.Age descending
                             select user;
                var users2 = db.Users.OrderBy(u => u.Name).ThenByDescending(u => u.Age);
                foreach (var user in users1)
                    Console.WriteLine($"{user.Id}.{user.Name}({user.Age})");
            }
            //using Join
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing Join");
                var users1 = from user in db.Users
                             join comp in db.Companies on user.CompanyId equals comp.Id
                             select new { Name = user.Name, Company = comp.Name, Age = user.Age };
                var users2 = db.Users.Join(db.Companies,
                    user => user.CompanyId,
                    comp => comp.Id,
                    (user, comp) => new
                    {
                        Name = user.Name,
                        Company = comp.Name,
                        Age = user.Age
                    });
                foreach (var user in users1)
                {
                    Console.WriteLine($"{user.Name}({user.Age}) - {user.Company}");
                }
            }
            //using GroupBy
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing GroupBy");
                var groups1 = (from u in db.Users.Include(comp => comp.Company).AsEnumerable()
                               group u by u.Company.Name into gr
                               select gr);
                var groups2 = db.Users.Include(comp => comp.Company)
                    .AsEnumerable().GroupBy(uc => uc.Company.Name);
                foreach (var group in groups1)
                {
                    Console.WriteLine($"{group.Key}");
                    foreach (var item in group)
                    {
                        Console.WriteLine($"{item.Name}({item.Age})");
                    }
                }
            }
            //using Union
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing Union");
                var users = db.Users.Where(u => u.Age < 30)
                    .Union(db.Users.Where(u => u.Name.Contains("T")));
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name}({user.Age})");
                }
            }
            //using Intersect
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing Intersect");
                var users = db.Users.Where(u => u.Age > 25)
                .Intersect(db.Users.Where(u => u.Name.Contains("T")));
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name}({user.Age})");
                }
            }
            //using Except
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing Except");
                var selector1 = db.Users.Where(u => u.Age > 25);
                var selector2 = db.Users.Where(u => u.Name.Contains("T"));
                var users = selector1.Except(selector2);
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name}({user.Age})");
                }
            }
            //aggregate operations
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing Aggregate Operations");
                Console.WriteLine($"using Any");
                bool result1 = db.Users.Any(u => u.Company.Name == "google");
                Console.WriteLine(result1);

                Console.WriteLine($"using All");
                bool result2 = db.Users.All(u => u.Company.Name == "google");
                Console.WriteLine(result2);

                Console.WriteLine($"using Count");
                int result3 = db.Users.Count();
                Console.WriteLine(result3);

                Console.WriteLine($"using Max");
                int result4 = db.Users.Max(u => u.Age);
                Console.WriteLine(result4);

                Console.WriteLine($"using Min");
                int result5 = db.Users.Min(u => u.Age);
                Console.WriteLine(result5);

                Console.WriteLine($"using Average");
                double result6 = db.Users.Average(u => u.Age);
                Console.WriteLine(result6);

                Console.WriteLine($"\nusing Sum");
                int result7 = db.Users.Sum(u => u.Age);
                Console.WriteLine(result7);
            }
            //using AsNoTracking
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nusing AsNoTracking");
                var users = db.Users.ToList();
                int count = db.ChangeTracker.Entries().Count();               
                Console.WriteLine($"{count}");
                //db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                //the first situation
                var user1 = db.Users.FirstOrDefault();
                var user2 = db.Users.FirstOrDefault();
                Console.WriteLine($"Before User1: {user1.Name}   User2: {user2.Name}");

                user1.Name = "Tim";

                Console.WriteLine($"After User1: {user1.Name}   User2: {user2.Name}");               
            }
            using (AppDbContext db = new AppDbContext())
            {
                //the second situation
                var user1 = db.Users.FirstOrDefault();
                var user2 = db.Users.AsNoTracking().FirstOrDefault();
                Console.WriteLine($"Before User1: {user1.Name}   User2: {user2.Name}");

                user1.Name = "Tim";

                Console.WriteLine($"After User1: {user1.Name}   User2: {user2.Name}");
            }
        }
    }
}
