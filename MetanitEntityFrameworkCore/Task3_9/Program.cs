using System;
using Task3_9.Models;

namespace Task3_9
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                User user1 = new User
                {
                    Login = "login1",
                    Password = "pass1234",
                    Profile = new UserProfile { Age = 23, Name = "Tom" }
                };
                User user2 = new User
                {
                    Login = "login2",
                    Password = "5678word2",
                    Profile = new UserProfile { Age = 27, Name = "Alice" }
                };
                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
        }
    }
}
