using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Services;
using Cooper.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TokenCleanerExtension
    {
        private static ITokenCleaner userConnectionService;
        public static void AddTokenCleanerService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            TokenCleaner tokenCleaner = new TokenCleaner(serviceProvider.GetService<IConfigProvider>());
            services.AddSingleton<ITokenCleaner>(tokenCleaner);
        }
    }
}