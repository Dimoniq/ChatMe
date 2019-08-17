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
    [StringLength(20, MinimumLength = 3, ErrorMessage = "The username has to be from 3 to 20 characters long")]
    [MinLength(3, ErrorMessage = "The username has to be at least 3 characters long")]
    [MaxLength(20, ErrorMessage = "The username should not be longer than 20 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, MinimumLength = 4, ErrorMessage = "The password has to be from 4 to 32 characters long")]
    public string Password { get; set; }
  }
}