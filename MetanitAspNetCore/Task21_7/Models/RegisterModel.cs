using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task21_7.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter the Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The confirmed password is wrong")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }

    }
}
