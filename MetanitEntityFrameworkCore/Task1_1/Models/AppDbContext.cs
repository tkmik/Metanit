using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_1.Models
{
    class AppDbContext : DbContext
    {
        private readonly StreamWriter logStream = new StreamWriter("myLog.txt", true);
        public DbSet<Person> People { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=people;Trusted_Connection=True;");

            //optionsBuilder.LogTo(message => Debug.WriteLine(message));
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.LogTo(message => logStream.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name}, LogLevel.Information);
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create((builder) =>
        {
            //builder.AddConsole();
            builder.AddProvider(new MyLoggerProvider());
        });

        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
    }
}
