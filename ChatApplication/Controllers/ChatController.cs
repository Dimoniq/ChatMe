using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
  public class ChatController : Controller
  {
    [Authorize]
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