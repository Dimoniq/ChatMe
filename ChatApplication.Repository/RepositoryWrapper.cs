using ChatApplication.Contracts;

namespace ChatApplication.Repository
{
  public class RepositoryWrapper : IRepositoryWrapper
  {
    private readonly RepositoryContext repositoryContext;
    private IUserRepository user;

    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
      this.repositoryContext = repositoryContext;
    }

    public IUserRepository User => this.user ?? (this.user = new UserRepository(this.repositoryContext));

    public void Save()
    {
      this.repositoryContext.SaveChanges();
    }
  }
}