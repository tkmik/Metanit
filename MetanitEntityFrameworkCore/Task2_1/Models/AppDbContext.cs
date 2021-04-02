using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2_1.Models
{
    class AppDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString = config.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            //modelBuilder.Entity<User>().Ignore(field => field.Position); // way1
            //modelBuilder.Entity<User>().Property(user => user.Id).HasColumnName("user_id");
            //modelBuilder.Entity<User>().HasKey(user => user.Id);
            //modelBuilder.Entity<User>().HasKey(key => new { key.Id, key.Name} );
            //modelBuilder.Entity<User>().HasAlternateKey(key => key.Name);
            //modelBuilder.Entity<User>().HasIndex(key => key.Name).IsUnique();
        }
    }
}
