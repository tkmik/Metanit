using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task18_1.Pages
{
    public class ExchangeModel : PageModel
    {
        public string Message { get; set; }
        private readonly decimal currentRate = 64.1m;
        public void OnGet()
        {
            Message = "Enter the sum";
        }
        public void OnPost(int? sum)
        {
            if (sum is null || sum < 1000)
            {
                Message = "The sum is wrong";
            }
            else
            {
                decimal result = sum.Value / currentRate;
                Message = $"{sum} roubles equals {result:f2}$";
            }
        }
    }
}
