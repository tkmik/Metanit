using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task10_1.Models
{
    public class SimpleTimeService : ITimeService
    {
        public string GetTime()
        {
            return DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
