using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Task29_1
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Receive", message, Context.ConnectionId);
        }

        public override async Task OnConnectedAsync()
        {
            var context = this.Context.GetHttpContext();
            
            if (context.Request.Cookies.ContainsKey("name"))
            {
                string userName;
                if (context.Request.Cookies.TryGetValue("name", out userName))
                {
                    Debug.WriteLine($"name = {userName}");
                }
            }
            
            Debug.WriteLine($"UserAgent = {context.Request.Headers["User-Agent"]}");
           
            Debug.WriteLine($"RemoteIpAddress = {context.Connection.RemoteIpAddress.ToString()}");

            await base.OnConnectedAsync();

            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} has entered the chat");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} has left the chat");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
