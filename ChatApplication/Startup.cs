using ChatApplication.Contracts;
using ChatApplication.Hubs;
using ChatApplication.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication
{
  public class Startup
  {
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
      services.AddDbContext<RepositoryContext>(options =>
        options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));
      services.AddSignalR();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseSignalR(config => config.MapHub<ChatHub>("/chatHub"));
      app.UseMvc(routes => { routes.MapRoute("default", "{controller=Login}/{action=Get}/{id?}"); });
    }
  }
}