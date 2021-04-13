using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_1.Models
{
    class User
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
