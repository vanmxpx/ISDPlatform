using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationExtension
    {
        private static IConfigProvider configProvider;

        public static void AddConfigurationProvider(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigLoader loader = new ConfigLoader();
            configProvider = loader.GetConfigProvider(configuration);

            services.AddSingleton<IConfigProvider>(configProvider);
        }
    }
}
