using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task14_1.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
