using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task33_1.Models;

namespace Task33_1.Controllers
{
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {
        static readonly List<Phone> data;
        static PhonesController()
        {
            data = new List<Phone>
            {
                new Phone { Id = Guid.NewGuid().ToString(), Name = "IPhone 5S", Price = 800 },
                new Phone { Id = Guid.NewGuid().ToString(), Name = "Samsung S20 FE", Price = 1600 }
            };
        }
        [HttpGet]
        public IEnumerable<Phone> Get()
        {
            return data;
        }
        [HttpPost]
        public IActionResult Post(Phone phone)
        {
            phone.Id = Guid.NewGuid().ToString();
            data.Add(phone);
            return Ok(phone);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Phone phone = data.FirstOrDefault(x => x.Id == id);
            if (phone == null)
            {
                return NotFound();
            }
            data.Remove(phone);
            return Ok(phone);
        }
    }
}
