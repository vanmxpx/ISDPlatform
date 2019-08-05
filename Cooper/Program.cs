using System;
using System.IO;
using Cooper.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using Oracle.ManagedDataAccess.Client;
using Cooper.Logging;

namespace Cooper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting.\r\n");
            Logger logger = CooperLogger.GetLogger("PROGRAM");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");
            logger.Info("SUCCESS");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>();
    }
}