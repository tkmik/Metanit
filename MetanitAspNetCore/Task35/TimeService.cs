using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task35
{
    public class TimeService
    {
        public TimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
        public string Time { get; set; }
    }
}
