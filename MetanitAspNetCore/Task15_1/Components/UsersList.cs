using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task15_1.Components
{
    public class UsersList : ViewComponent
    {
        List<string> users;
        public UsersList()
        {
            users = new List<string>
            {
                "Some", "Names", "Here"
            };
        }
        public IViewComponentResult Invoke()
        {
            return View(users);
        }
    }
}
