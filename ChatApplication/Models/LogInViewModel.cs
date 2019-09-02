using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
  public class LogInViewModel
  {
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }

    [DisplayName("Remember me?")]
    public bool RememberMe { get; set; }
  }
}