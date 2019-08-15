using ChatApplication.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repository
{
  public class RepositoryContext : DbContext
  {
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}