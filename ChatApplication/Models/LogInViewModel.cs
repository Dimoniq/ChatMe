using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
  public class LogInViewModel
  {
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
  }
}