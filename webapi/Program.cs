using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using webapi.Data;
using webapi.Models;

namespace webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            InitHost(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void InitHost(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                   .Database.Migrate();

                    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                        .Database.Migrate();

                    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();

                    var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                    userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin"
                    }, "password").GetAwaiter().GetResult();

                    foreach (var client in Config.GetClients())
                    {
                        var old = context.Clients.FirstOrDefault(x => x.ClientId == client.ClientId);
                        if (old == null)
                            context.Clients.Add(client.ToEntity());
                    }

                    foreach (var resource in Config.GetApiResources())
                    {
                        var old = context.ApiResources.FirstOrDefault(x => x.Name == resource.Name);
                        if (old == null)
                            context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error");
                }
            }
        }
    }
}
