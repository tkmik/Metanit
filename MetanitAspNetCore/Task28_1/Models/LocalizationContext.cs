using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task28_1.Models
{
    public class LocalizationContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public LocalizationContext(DbContextOptions<LocalizationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
