using Cooper.Services;
using Cooper.Services.Interfaces;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmtpClientExtension
    {
        public static void AddSmtpClientExtensionService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddTransient<ISmtpClient>(smtp => new GmailSmtpClient(serviceProvider.GetService<IConfigProvider>()));
        }
    }
}
