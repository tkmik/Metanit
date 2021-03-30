using Task1_1.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Task1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            //currenct directory
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //get information from this file
            builder.AddJsonFile("appsettings.json");
            //build configuration
            var config = builder.Build();
            string connectionString = config.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (AppDbContext context = new AppDbContext(options))
            {
                //context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                Person p1 = new Person { Name = "Mikita", Age = 25 };
                Person p2 = new Person { Name = "Dasha", Age = 21 };

                context.AddRangeAsync(p1, p2);
                context.SaveChangesAsync();

                Console.WriteLine("The Data has been saved!");
                var people = context.People.ToList();
                Console.WriteLine("The List of people");
                foreach (var person in people)
                {
                    Console.WriteLine($"Name:{person.Name}, Age:{person.Age}");
                }
            }
            Console.ReadKey();
        }
    }
}
