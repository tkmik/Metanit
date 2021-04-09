using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task6_1.Models;

namespace Task6_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //using (AppDbContext db = new AppDbContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();

            //    Company google = new Company { Name = "Google" };
            //    Company microsoft = new Company { Name = "Microsoft" };
                
            //    db.Companies.AddRange(google, microsoft);

            //    User tom = new User { Name = "Tom", Age = 40, Company = microsoft };
            //    User bob = new User { Name = "Bob", Age = 23, Company = google };
            //    User alice = new User { Name = "Alice", Age = 57, Company = microsoft };
            //    User kate = new User { Name = "Kate", Age = 34, Company = google };
            //    User tomas = new User { Name = "Tomas", Age = 29, Company = microsoft };
            //    User tomek = new User { Name = "Tomek", Age = 30, Company = google };

            //    await db.Users.AddRangeAsync(tom, bob, alice, kate, tomas, tomek);
            //    await db.SaveChangesAsync();
            //}
            using (AppDbContext db = new AppDbContext())
            {
                var param = new SqlParameter("@name", "%T%");
                var usersContainT = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE @name", param).OrderBy(sel => sel.Name).ToList();
                var name = "%Tom%";
                var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE {0}", name).ToList();
                var age = 30;
                var usersAge = db.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Age > {30}").ToList();
                
                foreach (var user in usersAge)
                {
                    Console.WriteLine($"{user.Name}");
                }
                string newComp = "Apple";
                int numberOfRowInserted = await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Companies (Name) VALUES ({newComp})");

                string appleInc = "Apple Inc.";
                string apple = "Apple";
                int numberOfRowUpdated = await db.Database.ExecuteSqlInterpolatedAsync($"UPDATE Companies SET Name={appleInc} WHERE Name={apple}");

                int numberOfRowDeleted = await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Companies WHERE Name={appleInc}");
            }
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nFunctions");
                var users1 = db.Users.FromSqlInterpolated($"SELECT * FROM GetUsersByAge ({30})").ToList();
                var users2 = db.GetUsersByAge(30);
                foreach (var user in users2)
                {
                    Console.WriteLine($"{user.Name}");
                }
            }

            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("\nProcedures");
                SqlParameter param = new SqlParameter("@name", "Microsoft");
                var users = db.Users.FromSqlRaw("GetUsersByCompany @name", param).ToList();
                foreach (var p in users)
                    Console.WriteLine($"{p.Name} - {p.Age}");
            }

            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine();
                var param = new SqlParameter
                {
                    ParameterName = "@userName",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw("GetUserWithMaxAge @userName OUT", param);
                Console.WriteLine(param.Value);
            }
        }
    }
}
