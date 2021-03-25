using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task16_1.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина имени должна быть от 5 до 50 символов")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action:"CheckEmail", controller:"Home", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Range(14, 100, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        //[CreditCard]
        //[Phone]
        //[Url]

    }
}
