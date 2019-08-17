using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Contracts;
using ChatApplication.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
  public class LoginController : Controller
  {
    private readonly IRepositoryWrapper repoWrapper;

    public LoginController(IRepositoryWrapper repoWrapper)
    {
      this.repoWrapper = repoWrapper;
    }

    [HttpGet]
    public IActionResult LogIn()
    {
      return this.View();
    }

    [HttpPost]
    public IActionResult LogIn(User userToLogIn)
    {
      if (this.ModelState.IsValid)
      {
        if (this.IsAuthorizedUser(userToLogIn))
        {
          return this.RedirectToAction("Index", "Chat", new {username = userToLogIn.Username });
        }
        else
        {
          ModelState.AddModelError("Summary", "The user with this username and password does not exist.");
        }
      }

      return this.View(userToLogIn);
    }

    private bool IsAuthorizedUser(User userToLogIn)
    {
      return this.repoWrapper.User.FindByCondition(user =>
               user.Username.Equals(userToLogIn.Username, StringComparison.InvariantCultureIgnoreCase) &&
               user.Password.Equals(userToLogIn.Password)).Any();
    }


    [Route("SignUp")]
    public IActionResult SignUp()
    {
      return this.View();
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      this.repoWrapper.User.Create(new User {Password = "Password", UserId = Guid.NewGuid(), Username = "Dmytro"});
      this.repoWrapper.Save();
      List<User> users = this.repoWrapper.User.FindAll().ToList();
      return users.Select(u => $"Id: {u.UserId}, Username: {u.Username}, password: {u.Password}");
    }
  }
}