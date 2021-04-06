using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_2.Models
{
    [Table("Employees")]
    class Employee : User
    {
        public int Salary { get; set; }
    }
}
