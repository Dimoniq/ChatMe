using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient
{
  class Program
  {
    public static async Task Main(string[] args)
    {
      HubConnection hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chatHub").Build();
      hubConnection.On<string, string>("ReceiveMessage",
        (user, message) => Console.WriteLine($"{user}: {message}"));
      hubConnection.On<string>("UserLoggedIn", user => Console.WriteLine($"User {user} logged in."));

      await hubConnection.StartAsync();

      Console.WriteLine("Hello, please enter your name:");
      var name = Console.ReadLine();
      await hubConnection.InvokeAsync("Login", name);
      Console.WriteLine($"Hello, {name}. Enter your first message...");

      while (true)
      {
        string text = Console.ReadLine();
        await hubConnection.InvokeAsync("SendMessage", name, text);
      }
    }
  }
}