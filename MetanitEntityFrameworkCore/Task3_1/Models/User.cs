using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_1.Models
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

     //   public int CompanyId { get; set; } // cascad deleting
        public int? CompanyId { get; set; } // not cascad deleting; allow null
        public Company Company { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
    }
}
