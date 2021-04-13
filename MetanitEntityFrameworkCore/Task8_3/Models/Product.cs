using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_3.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int TotalCount { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
