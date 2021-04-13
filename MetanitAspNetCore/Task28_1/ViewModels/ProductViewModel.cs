using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task28_1.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "NameReuired")]
        [StringLength(20, ErrorMessage = "NameLenght", MinimumLength = 6)]
        public string Name { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "PriceRequired")]
        [Range(10, 100, ErrorMessage = "PriceRange")]
        public int Price { get; set; }
    }
}
