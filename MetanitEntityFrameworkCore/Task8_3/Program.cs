using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Task8_3.Models;

namespace Task8_3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                //create view
                db.Database.ExecuteSqlRaw(@"CREATE VIEW View_ProductsByCompany AS 
                                            SELECT c.Name AS CompanyName, Count(p.Id) AS ProductCount, Sum(p.Price * p.TotalCount) AS TotalSum
                                            FROM Companies c
                                            INNER JOIN Products p on p.CompanyId = c.Id
                                            GROUP BY c.Name");
                //add information
                Company c1 = new Company { Name = "Apple" };
                Company c2 = new Company { Name = "Samsung" };
                Company c3 = new Company { Name = "Huawei" };
                db.Companies.AddRange(c1, c2, c3);

                Product p1 = new Product { Name = "iPhone X", Company = c1, Price = 2000, TotalCount = 10 };
                Product p2 = new Product { Name = "iPhone 8", Company = c1, Price = 1000, TotalCount = 5 };
                Product p3 = new Product { Name = "Galaxy S9", Company = c2, Price = 800, TotalCount = 12 };
                Product p4 = new Product { Name = "Galaxy A7", Company = c2, Price = 500, TotalCount = 23 };
                Product p5 = new Product { Name = "Honor 9", Company = c3, Price = 500, TotalCount = 3 };
                db.Products.AddRange(p1, p2, p3, p4, p5);
                db.SaveChanges();
            }

            using (AppDbContext db = new AppDbContext())
            {                
                var companyProducts = db.ProductsByCompany.ToList();
                foreach (var item in companyProducts)
                {
                    Console.WriteLine($"{item.CompanyName}-{item.ProductCount}({item.TotalSum})");
                }
            }
        }
    }
}
