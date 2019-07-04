using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Services;
using Cooper.Configuration;
using NLog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TokenCleanerExtension
    {
        public static void AddTokenCleanerService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            TokenCleaner tokenCleaner = new TokenCleaner(serviceProvider.GetService<IConfigProvider>(), serviceProvider.GetService<ILogger>());
            services.AddSingleton<ITokenCleaner>(tokenCleaner);
        }
    }
}