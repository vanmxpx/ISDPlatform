using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Cooper.Services;

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
