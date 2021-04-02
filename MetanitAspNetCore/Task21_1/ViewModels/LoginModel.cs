using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task21_1.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter the Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
