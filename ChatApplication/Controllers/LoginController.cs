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
    public IEnumerable<string> Get()
    {
      this.repoWrapper.User.Create(new User{Password = "Password", UserId = Guid.NewGuid(), Username = "Dmytro"});
      this.repoWrapper.Save();
      List<User> users = this.repoWrapper.User.FindAll().ToList();
      return new List<string>
      {
        "Hello",
        "World"
      };
    }
  }
}