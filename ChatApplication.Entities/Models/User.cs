using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Entities.Models
{
  [Table("User")]
  public class User
  {
    [Key] public Guid UserId { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "The username has to be at least 3 characters long")]
    [MaxLength(20, ErrorMessage = "The username should not be longer than 20 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(4, ErrorMessage = "Password has to be at least 4 characters long")]
    [MaxLength(32, ErrorMessage = "The password should not be longer than 32 characters")]
    public string Password { get; set; }
  }
}