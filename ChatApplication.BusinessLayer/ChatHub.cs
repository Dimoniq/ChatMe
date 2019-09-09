using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatApplication.BusinessLayer
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
      await this.Clients.All.SendAsync("ReceiveMessage", user, message);
      this.logger.LogInformation($"Received message: {message} from {user}. Publishing to all subscribers."); 
    }
  }
}