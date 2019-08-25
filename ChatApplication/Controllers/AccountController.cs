using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Contracts;
using ChatApplication.Entities.Models;
using ChatApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
  public class AccountController : Controller
  {
    private readonly IRepositoryWrapper repoWrapper;

    public AccountController(IRepositoryWrapper repoWrapper)
    {
      this.repoWrapper = repoWrapper;
    }

    [HttpGet]
    public IActionResult LogIn()
    {
      return this.View();
    }

    [HttpPost]
    public IActionResult LogIn(LogInViewModel userToLogIn, [FromServices] SignInManager<IdentityUser> signInManager)
    {
      if (this.ModelState.IsValid)
      {
        if (this.IsAuthorizedUser(userToLogIn))
        {
          return this.RedirectToAction("Index", "Chat", new {username = userToLogIn.Username});
        }
        else
        {
          this.ModelState.AddModelError("Summary", "The user with this username and password does not exist.");
        }
      }

      return this.View(userToLogIn);
    }

    private bool IsAuthorizedUser(LogInViewModel userToLogIn)
    {
      return this.repoWrapper.User.FindByCondition(user =>
        user.Username.Equals(userToLogIn.Username, StringComparison.InvariantCultureIgnoreCase) &&
        user.Password.Equals(userToLogIn.Password)).Any();
    }


    [HttpGet]
    public IActionResult SignUp()
    {
      return this.View();
    }

    [HttpPost]
    public IActionResult SignUp(SignUpViewModel userToSignUp)
    {
      if (this.ModelState.IsValid)
      {
        this.repoWrapper.User.Create(new User {Password = userToSignUp.Password, UserId = Guid.NewGuid(), Username = userToSignUp.Username});
        this.repoWrapper.Save();

        return this.RedirectToAction("Index", "Chat", new {username = userToSignUp.Username});
      }

      return this.View(userToSignUp);
    }
  }
}