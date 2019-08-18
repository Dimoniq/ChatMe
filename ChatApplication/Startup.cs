﻿using ChatApplication.Contracts;
using ChatApplication.Hubs;
using ChatApplication.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
      services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
      services.AddEntityFrameworkSqlite().AddDbContext<RepositoryContext>();
      services.AddSignalR();
      services.AddMvc();
      services.AddDefaultIdentity<IdentityUser>()
        .AddDefaultUI(UIFramework.Bootstrap4)
        .AddEntityFrameworkStores<RepositoryContext>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, RepositoryContext repositoryContext)
    {
      app.UseSignalR(config => config.MapHub<ChatHub>("/chatHub"));
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes => { routes.MapRoute("default", "{controller=Login}/{action=LogIn}/{id?}"); });

      if (this.configuration.GetValue<bool>("ShouldCreateDBIfNotExists"))
      {
        repositoryContext.Database.EnsureCreated();
        repositoryContext.Database.Migrate();
      }
    }
  }
}