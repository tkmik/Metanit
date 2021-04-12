using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace Task7_2.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "server=localhost;user=root;password=12345678;database=usersdb5;";
            //ServerVersion version = ServerVersion.AutoDetect(connection);
            optionsBuilder.UseMySql(connection);
        }
    }
}
