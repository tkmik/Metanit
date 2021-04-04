using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Task2_1.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User { Id = 1, Name = "Mikita", Age = 25, Position = "M"},
                });
            //Fluent API
            //modelBuilder.Entity<User>().Ignore(field => field.Position); // way1
            //modelBuilder.Entity<User>().Property(user => user.Id).HasColumnName("user_id");
            //modelBuilder.Entity<User>().HasKey(user => user.Id);
            //modelBuilder.Entity<User>().HasKey(key => new { key.Id, key.Name} );
            //modelBuilder.Entity<User>().HasAlternateKey(key => key.Name);
            //modelBuilder.Entity<User>().HasIndex(key => key.Name).IsUnique();
            //modelBuilder.Entity<User>().Property(user => user.Age).HasDefaultValue(18);
            //modelBuilder.Entity<User>().Property(user => user.Name).IsRequired();
            //modelBuilder.Entity<User>().Property(user => user.Name).HasMaxLength(50);
            //modelBuilder.Entity<User>().Property(user => user.Name).HasColumnType("varchar(50)");
        }
    }
}
