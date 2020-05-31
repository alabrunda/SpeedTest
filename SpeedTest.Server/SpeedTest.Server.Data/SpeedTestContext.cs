using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SpeedTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedTest.Data
{
    public class SpeedTestContext : DbContext
    {
        public DbSet<SpeedTestCheckIn> SpeedTestCheckIns {get; set;}

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                   .AddConsole();
        });

        public SpeedTestContext(DbContextOptions<SpeedTestContext> options) : base(options)
        {
            
        }        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        "Data Source=localhost;Initial Catalog=SpeedTest;Persist Security Info=True;User ID=sa;Password=MySAPA55");
        //    base.OnConfiguring(optionsBuilder);
        //}
    } 
}
