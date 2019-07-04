using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggerServiceExtension
    {
        public static void AddNlogLogger(this IServiceCollection services)
        {
            Logger logger = LogManager.GetLogger("CooperLogger");

            services.AddSingleton<ILogger>(logger);
        }
    }
}
