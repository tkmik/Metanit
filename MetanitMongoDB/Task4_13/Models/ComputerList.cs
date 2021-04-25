using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4_13.Models
{
    public class ComputerList
    {
        public IEnumerable<Computer> Computers { get; set; }
        public ComputerFilter Filter { get; set; }
    }
}
