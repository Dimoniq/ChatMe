using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Entities.Models
{
  [Table("User")]
  public class User
  {
    [Key] public Guid UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
  }
}