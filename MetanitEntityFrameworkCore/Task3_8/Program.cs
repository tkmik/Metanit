using System;
using Task3_8.Models;

namespace Task3_8
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Student tom = new Student { Name = "Tom" };
                Student alice = new Student { Name = "Alice" };
                Student bob = new Student { Name = "Bob" };
                db.Students.AddRange(tom, alice, bob);

                Course algorithms = new Course { Name = "Algorithms" };
                Course basics = new Course { Name = "Basics of programming" };
                db.Courses.AddRange(algorithms, basics);

                tom.Courses.Add(algorithms);
                tom.Courses.Add(basics);
                alice.Courses.Add(algorithms);
                bob.Courses.Add(basics);

                db.SaveChanges();
            }
        }
    }
}
