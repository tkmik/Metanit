using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_1.Models
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(user => user.Id);
            builder.Property(user => user.Name).IsRequired();
            builder.Property(user => user.Name).HasMaxLength(50);
            builder.Property(user => user.Age).HasDefaultValue(18);
        }
    }
}
