using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatApplication.Hubs
{
  public class ChatHub : Hub
  {
    private readonly ILogger<ChatHub> logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
      this.logger = logger;
    }
    public async Task SendMessage(string user, string message)
    {
      await this.Clients.Others.SendAsync("ReceiveMessage", user, message);
      this.logger.LogInformation($"Received message: {message} from {user}. Publishing to all subscribers."); 
    }

    public async Task Login(string user)
    {
      await this.Clients.Others.SendAsync("UserLoggedIn", user);
    }
  }
}