using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task29_1.Models;

namespace Task29_1
{
    public class Chat : Hub
    {
        public async Task Send(User user)
        {
            user.Age += 5;
            await Clients.Caller.SendAsync("Receive", user);
        }
    }   
}
