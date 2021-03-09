using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task31.Services
{
    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Sne by SMS";
        }
    }
}
