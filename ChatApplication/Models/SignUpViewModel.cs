using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
  public class SignUpViewModel
  {
    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "The username has to be from 3 to 20 characters long")]
    public string Username { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 4, ErrorMessage = "The password has to be from 4 to 32 characters long")]
    public string Password { get; set; }

    [DisplayName("Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
    public string ConfirmPassword { get; set; }
  }
}