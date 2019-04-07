using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Oracle.ManagedDataAccess.Client;
using Cooper.Controllers;
using NLog;
using NLog.Targets;
using NLog.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using Cooper.Controllers;

namespace Cooper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting.\r\n"); 
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
