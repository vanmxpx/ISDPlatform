using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UsersConnectionServiceExtension
    {
        public static void AddUserConnectionService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            UsersConnectionService userConnectionService = new UsersConnectionService(serviceProvider.GetService<IJwtHandlerService>(), serviceProvider.GetService<IConfigProvider>());
            services.AddSingleton<IUsersConnectionService>(userConnectionService);
        }
    }
}
