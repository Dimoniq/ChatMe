using ChatApplication.Contracts;
using ChatApplication.Entities.Models;

namespace ChatApplication.Repository
{
  public class UserRepository : RepositoryBase<User>, IUserRepository
  {
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
  }
}