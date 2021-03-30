using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task18_1.Models;

namespace Task18_1.Pages
{
    public class PersonModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public Person Person { get; set; }

        private List<Person> People;
        public List<Person> DisplayedPeople { get; set; }
        public PersonModel()
        {
            People = new List<Person>()
            {
                new Person { Name="Test", Age=10 },
                new Person { Name="Someone", Age=18 }
            };
        }
        public void OnGet()
        {
            Message = "Enter the data";
            DisplayedPeople = People;
        }
        public void OnPost()
        {
            Message = $"Name:{Person.Name}, Age:{Person.Age}";
        }
        public void OnGetByName(string name)
        {
            DisplayedPeople = People.Where(search => search.Name.Contains(name)).ToList();
        }

        public void OnGetByAge(int age)
        {
            DisplayedPeople = People.Where(search => search.Age == age).ToList();
        }

        public void OnPostGreaterThan(int age)
        {
            DisplayedPeople.Where(search => search.Age > age).ToList();
        }

        public void OnPostLessThan(int age)
        {
            DisplayedPeople = People.Where(search => search.Age < age).ToList();
        }
    }
}
