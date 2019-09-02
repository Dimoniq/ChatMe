using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatApplication.Controllers
{
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> logger;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
      ILogger<AccountController> logger)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.logger = logger;
    }

    [HttpGet]
    public IActionResult LogIn()
    {
      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInViewModel userToLogIn)
    {
      if (!this.ModelState.IsValid)
      {
        return this.View(userToLogIn);
      }

      var result =
        await this.signInManager.PasswordSignInAsync(userToLogIn.Username, userToLogIn.Password, userToLogIn.RememberMe,
          false);

      if (!result.Succeeded)
      {
        this.ModelState.AddModelError("Summary", "The username or password is wrong.");
        this.logger.LogWarning($"Login attempt for {userToLogIn.Username} failed");
        return this.View(userToLogIn);
      }

      this.logger.LogInformation($"The user {userToLogIn.Username} was successfully logged in");
      return this.RedirectToAction("Index", "Chat");
    }


    [HttpGet]
    public IActionResult SignUp()
    {
      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel userToSignUp)
    {
      if (!this.ModelState.IsValid)
      {
        return this.View(userToSignUp);
      }

      var newUser = new IdentityUser
      {
        UserName = userToSignUp.Username
      };

      var result = await this.userManager.CreateAsync(newUser, userToSignUp.Password);

      if (result.Succeeded)
      {
        await this.signInManager.SignInAsync(newUser, false);
        this.logger.LogInformation($"{userToSignUp.Username} registered as a new user. ");
        return this.RedirectToAction("Index", "Chat", new {username = userToSignUp.Username});
      }

      foreach (var error in result.Errors.Select(x => x.Description))
      {
        this.ModelState.AddModelError("Summary", error);
        this.logger.LogInformation($"SignUp error: {error}");
      }

      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> LogOut(string returnUrl = null)
    {
      this.logger.LogInformation($"{this.User.Identity.Name} singed out");
      await this.signInManager.SignOutAsync();

      if (string.IsNullOrWhiteSpace(returnUrl))
      {
        return this.RedirectToAction("Index", "Chat");
      }

      return this.Redirect(returnUrl);
    }
  }
}