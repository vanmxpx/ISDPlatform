using Cooper.Configuration;
using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SocialAuthExtension
    {
        public static void AddSocialAuthService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddTransient<ISocialAuth>(socialAuth => new SocialAuth(serviceProvider.GetService<IConfigProvider>()));
        }
    }
}
