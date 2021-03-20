using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Task12_1.Models;
using Task12_1.ViewModels;
using System.Linq;

namespace Task12_1.Controllers
{
    public class HomeController : Controller
    {
        List<Phone> phones;
        List<Company> companies;
        public HomeController()
        {
            Company apple = new Company { Id = 1, Name = "Apple", Country = "USA" };
            Company samsung = new Company { Id = 2, Name = "Samsung", Country = "South Corea" };
            Company google = new Company { Id = 3, Name = "Google", Country = "USA" };
            companies = new List<Company> { apple, samsung, google };

            phones = new List<Phone>
            {
                new Phone { Id = 1, Manufacturer = apple, Name="IPhone X", Price=50000 },
                new Phone { Id = 2, Manufacturer = apple, Name="IPhone 11", Price=70000 },
                new Phone { Id = 3, Manufacturer = samsung, Name="Galaxy S10", Price=50000 },
                new Phone { Id = 4, Manufacturer = samsung, Name="Galaxy S20", Price=65000 },
                new Phone { Id = 5, Manufacturer = google, Name="Pixel 2", Price=25000 },
                new Phone { Id = 6, Manufacturer = google, Name="Pixel XL", Price=60000 }
            };

        }
        public IActionResult Index(int? companyId)
        {
            List<CompanyModel> compModels = companies
                .Select(company => new CompanyModel { Id = company.Id, Name = company.Name })
                .ToList();

            compModels.Insert(0, new CompanyModel { Id = 0, Name = "All" });

            IndexViewModel ivm = new IndexViewModel { Companies = compModels, Phones = phones };

            if (companyId != null && companyId > 0)
                ivm.Phones = phones.Where(phone => phone.Manufacturer.Id == companyId);
            return View(ivm);
        }


        //public IActionResult Index()
        //{
        //    return View(phones);
        //}
    }
}
