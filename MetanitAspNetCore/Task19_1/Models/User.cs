using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task19_1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter correct name")]        
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter users age")]
        [Range(1, 100, ErrorMessage = "The Age must be from 1 to 100")]
        public int Age { get; set; }
    }
}
