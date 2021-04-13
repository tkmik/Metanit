using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task28_1.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Culture Culture { get; set; }
    }
}
