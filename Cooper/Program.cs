using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace Cooper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting.\r\n");
            ConfigureNLog();
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void ConfigureNLog()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
            var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/file.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            // Step 3. Define rules
            config.AddRuleForAllLevels(fileTarget);
            config.AddRuleForAllLevels(consoleTarget);

            // Step 4. Activate the configuration
            LogManager.Configuration = config;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
           .UseNLog()
           .UseStartup<Startup>();
    }
}