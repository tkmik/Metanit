using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task15_1.Models;

namespace Task15_1.Components
{
    public class Person : ViewComponent
    {
        public IViewComponentResult Invoke(User user)
        {
            return Content($"{user.Name}-{user.Age}");
        }
    }
}
