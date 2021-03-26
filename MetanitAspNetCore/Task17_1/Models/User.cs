using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task17_1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Field 'Name' is empty!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Field 'Age' is empty!")]
        public int Age { get; set; }
    }
}
