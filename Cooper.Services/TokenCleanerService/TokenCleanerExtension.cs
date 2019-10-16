using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TokenCleanerExtension
    {
        public static void AddTokenCleanerService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenCleaner, TokenCleaner>();
        }
    }
}