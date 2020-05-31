using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpeedTest.Data;

namespace SpeedTest.Server.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateDatabase(host);  
            host.Run();
        }

        private static void MigrateDatabase(IHost host)
        {

            using(var scope = host.Services.CreateScope())
            {
                //need to create SpeedTestContext without DI using DbContextOptions already defined
                for (int i = 0; i < 12; i++)//try to connect to DB for one minute after startup
                {
                    try
                    {
                        var db = scope.ServiceProvider.GetRequiredService<SpeedTestContext>();
                        db.Database.Migrate();//Create Database if it doesn't already exist.
                    }
                    catch
                    {
                        System.Console.WriteLine("Unable to connect to DB on startup.  Wait 5 seconds.");
                        Thread.Sleep(5000);
                    }
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
