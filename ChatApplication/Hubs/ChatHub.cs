using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
  public class ChatHub : Hub
  {
    public async Task SendMessage(string user, string message)
    {
      await this.Clients.Others.SendAsync("ReceiveMessage", user, message);
      Console.WriteLine($"Received message: {message} from {user}. Publishing to all subscribers."); 
    }

    public async Task Login(string user)
    {
      await this.Clients.Others.SendAsync("UserLoggedIn", user);
    }
  }
}