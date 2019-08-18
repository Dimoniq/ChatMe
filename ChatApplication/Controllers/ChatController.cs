using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
  public class ChatController : Controller
  {
    public ChatController()
    {
    }

    public IActionResult Index(string username)
    {
      ViewBag.Username = username;
      return View();
    }

    public IActionResult Privacy()
    {
      return this.View();
    }
  }
}