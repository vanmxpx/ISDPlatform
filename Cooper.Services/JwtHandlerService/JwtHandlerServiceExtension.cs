using Cooper.Services;
using Cooper.Services.Interfaces;

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
