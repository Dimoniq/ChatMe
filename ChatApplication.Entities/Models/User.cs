using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Entities.Models
{
  [Table("user")]
    public class User
    {
      [Key]
      public Guid UserId { get; set; }

      [Required(ErrorMessage = "Username is required")]
      [MinLength(3, ErrorMessage = "The username has to be at least 3 characters long")]
      public string Username { get; set; }

      [Required(ErrorMessage = "Password is required")]
      [MinLength(4, ErrorMessage = "Password has to be at least 4 characters long")]
      public string Password { get; set; }
    }
}
