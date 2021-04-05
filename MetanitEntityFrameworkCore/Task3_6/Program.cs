using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Task3_6.Models;

namespace Task3_6
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                User user1 = new User { Login = "login1", Password = "12345" };
                User user2 = new User { Login = "login2", Password = "pass" };
                db.Users.AddRange(user1, user2);

                UserProfile profile1 = new UserProfile { Age = 22, Name = "Tom", User = user1 };
                UserProfile profile2 = new UserProfile { Age = 27, Name = "Alice", User = user2 };
                db.Profiles.AddRange(profile1, profile2);
                db.SaveChanges();
            }
            using (AppDbContext db = new AppDbContext())
            {
                foreach (User user in db.Users.Include(u => u.Profile).ToList())
                {
                    Console.WriteLine($"{user.Profile?.Name}, {user.Profile?.Age}");
                    Console.WriteLine($"{ user.Login} - { user.Password}\n");
                }
            }
        }
    }
}
