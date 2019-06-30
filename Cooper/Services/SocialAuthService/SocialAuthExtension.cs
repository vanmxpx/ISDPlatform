using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Services;
using Cooper.Configuration;

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