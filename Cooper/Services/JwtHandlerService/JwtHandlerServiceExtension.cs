using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtHandlerServiceExtension
    {
        public static void AddJWTHandler(this IServiceCollection services)
        {
            services.AddSingleton<IJwtHandlerService, JwtHandlerService>();
        }
    }
}
