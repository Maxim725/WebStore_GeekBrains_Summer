using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.DAL;

namespace WebStore_GeekBrains_Summer
{
    // Microsoft рекомендует инициализировать БД между методами Build() и Run()
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuilderWebHost(args);

            // Рекомендация Microsoft так писать инициализацию бд
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    WebStoreContext context = services.GetRequiredService<WebStoreContext>();
                    DbInitializer.Initialize(context);
                    DbInitializer.InitializeUsers(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Oops. Db Initialize has been failed");
                }
            }

            host.Run();

        }

        private static IWebHost BuilderWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build()
        //        .Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
