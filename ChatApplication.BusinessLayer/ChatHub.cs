using System;
using System.Threading.Tasks;
using ChatApplication.BusinessLayer.BusinessObjects;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatApplication.BusinessLayer
{
  public class ChatHub : Hub
  {
    private static readonly ConnectionMapping<string> Connections =
      new ConnectionMapping<string>();

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

    public override Task OnConnectedAsync()
    {
      var name = this.Context.User.Identity.Name;
      Connections.Add(name, this.Context.ConnectionId);
      this.logger.LogInformation($"{name} connected with id {this.Context.ConnectionId}");
      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      var name = this.Context.User.Identity.Name;
      Connections.Remove(name, this.Context.ConnectionId);
      this.logger.LogInformation($"{name} disconnected with id {this.Context.ConnectionId}");

      return base.OnDisconnectedAsync(exception);
    }
  }
}