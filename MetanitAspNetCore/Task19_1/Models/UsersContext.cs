﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task19_1.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
