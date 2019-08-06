using Cooper.Configuration;
using Cooper.Services;
using Cooper.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationExtension
    {
        private static IConfigProvider configProvider;

        public static void AddConfigurationProvider(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigLoader loader = new ConfigLoader();
            configProvider = loader.GetConfigProvider(configuration);
            services.AddSingleton<IConfigProvider>(configProvider);
        }
    }
}
