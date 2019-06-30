using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Services;
using Cooper.Configuration;

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