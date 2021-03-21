using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task12_4.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [BindingBehavior(BindingBehavior.Required)]
        public string Name { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public Company Manufacturer { get; set; }
        [BindingBehavior(BindingBehavior.Never)]
        public decimal Price { get; set; }
    }
}
