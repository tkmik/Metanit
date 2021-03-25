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
        //[Required(ErrorMessage = "Введите имя")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Длина имени должна быть от 5 до 50 символов")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Неккоретный электронный адрес")]
        [Display(Name = "Электронная почта")]
        //[Remote(action:"CheckEmail", controller:"Home", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Пароль")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повтор пароля")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Не указан возраст")]
        [Range(14, 100, ErrorMessage = "Недопустимый возраст")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        //[CreditCard]
        //[Phone]
        //[Url]

    }
}
