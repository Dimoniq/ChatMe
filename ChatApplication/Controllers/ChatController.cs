using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
  public class ChatController : Controller
  {
    public IActionResult Index()
    {
      return this.View();
    }

    public IActionResult Privacy()
    {
      return this.View();
    }
  }
}