using Cooper.Configuration;
using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TokenCleanerExtension
    {
        public static void AddTokenCleanerService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            TokenCleaner tokenCleaner = new TokenCleaner(serviceProvider.GetService<IConfigProvider>());
            services.AddSingleton<ITokenCleaner>(tokenCleaner);
        }
    }
}