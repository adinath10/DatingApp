using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)//dotnet run command look for this main method
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(context);
            }
            catch (System.Exception Ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(Ex, "An error occurred during migration");
            }

            await host.RunAsync();
        }

        // creates an instance of IHostBuilder which hosts a web application
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        // By using the CreateDefaultBuilder method, we will get inbuilt support for Dependency Injection.
        // using CreateDefaultBuilder method, we also get logging support & also loads the applications configurations.
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();  //pointing to startup class
                });
    }
}
