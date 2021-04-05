
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_9.Models
{
    [Owned]
    class UserProfile
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
