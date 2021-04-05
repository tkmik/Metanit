using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Task3_1.Models;

namespace Task3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Position manager = new Position { Name = "Manager" };
                Position developer = new Position { Name = "Developer" };
                db.Positions.AddRange(manager, developer);

                City washington = new City { Name = "Washington" };
                City seoul = new City { Name = "Seoul" };
                db.Cities.AddRange(washington, seoul);

                Country usa = new Country { Name = "USA", Capital = washington };
                Country stkorea = new Country { Name = "South Korea", Capital = seoul };
                db.Countries.AddRange(usa, stkorea);

                Company apple = new Company { Name = "Apple", Country = usa };
                Company samsung = new Company { Name = "Samsung", Country = stkorea };
                db.Companies.AddRange(apple, samsung);

                User tom = new User { Name = "Tom", Company = apple, Position = developer};
                User bob = new User { Name = "Bob", Company = samsung, Position = manager};
                User alice = new User { Name = "Alice", Company = apple, Position = manager};
                User kate = new User { Name = "Kate", Company = samsung, Position = developer };
                db.AddRange(tom, bob, alice, kate);

                db.SaveChanges();
                var users = db.Users
                    .Include(u => u.Company)
                        .ThenInclude(comp => comp.Country)
                            .ThenInclude(coun => coun.Capital)
                    .Include(u => u.Position)
                    .ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name} - {user.Position?.Name}");
                    Console.WriteLine($"{user.Company?.Name}({user.Company?.Country?.Name},{user.Company?.Country?.Capital?.Name})");
                    Console.WriteLine();
                }
            }
        }
    }
}
