using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task21_5.Models
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
