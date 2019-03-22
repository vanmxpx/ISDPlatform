using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Oracle.ManagedDataAccess.Client;
using Cooper.Controllers;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace Cooper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new XmlLoggingConfiguration("nlog.config");
            Console.WriteLine("Starting.\r\n");
            using (var _db = new OracleConnection("Pooling = false; User Id=SYSTEM;Password=qQ1111qQ;Data Source=localhost:1521 /cooper;"))
            {
                Console.WriteLine("Open connection...");
                _db.Open();
                Console.WriteLine("Connected to:" + _db.ServerVersion);
                Console.WriteLine(_db.ConnectionString);
                Console.WriteLine("\r\nDone. Press key for exit");
                Console.ReadKey();
            }
            LogManager.Configuration = config;
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
