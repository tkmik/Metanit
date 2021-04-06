using System;
using System.Linq;
using Task4_1.Models;

namespace Task4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Table per Hierarchy
            using (AppDbContext db = new AppDbContext())
            {
                User user1 = new User { Name = "Tom" };
                User user2 = new User { Name = "Bob" };
                db.Users.Add(user1);
                db.Users.Add(user2);

                Employee employee = new Employee { Name = "Sam", Salary = 500 };
                db.Employees.Add(employee);
                Manager manager = new Manager { Name = "Robert", Departament = "IT" };
                db.Managers.Add(manager);

                db.SaveChanges();

                var users = db.Users.ToList();
                Console.WriteLine("All users");
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }

                Console.WriteLine("\nAll employees");
                foreach (var emp in db.Employees.ToList())
                {
                    Console.WriteLine(emp.Name);
                }

                Console.WriteLine("\nAll managers");
                foreach (var man in db.Managers.ToList())
                {
                    Console.WriteLine(man.Name);
                }
            }
        }
    }
}
