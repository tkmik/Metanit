using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task21_5.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter the Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password is wrong")]
        public string ConfirmPassword { get; set; }
    }
}
