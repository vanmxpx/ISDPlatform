using Cooper.Services;
using Cooper.Services.Interfaces;
using System;
using NLog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtHandlerServiceExtension
    {
        public static void AddJWTHandler(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IJwtHandlerService jwtHandler = new JwtHandlerService(serviceProvider.GetService<ILogger>());
            services.AddSingleton<IJwtHandlerService>(jwtHandler);
        }
    }
}
