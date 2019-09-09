using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UsersConnectionServiceExtension
    {
        public static void AddUserConnectionService(this IServiceCollection services)
        {
            services.AddSingleton<IUsersConnectionService, UsersConnectionService>();
        }
    }
}
