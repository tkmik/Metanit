using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task12_1.Models;

namespace Task12_1.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
    }
}
