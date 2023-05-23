using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)//dotnet run command look for this main method
        {
            CreateHostBuilder(args).Build().Run();
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
