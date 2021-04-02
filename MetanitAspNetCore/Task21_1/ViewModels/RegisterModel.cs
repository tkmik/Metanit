using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task21_1.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter the Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The entered Password is wrong!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
