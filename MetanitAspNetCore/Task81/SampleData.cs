using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task81.Models;

namespace Task81
{
    public static class SampleData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone 
                    {
                        Name = "S20 FE",
                        Company = "Samsung",
                        Price = 400
                    },
                    new Phone 
                    {
                        Name = "IPhone 5s",
                        Company = "Apple",
                        Price = 870
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
