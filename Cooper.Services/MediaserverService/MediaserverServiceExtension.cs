using Cooper.Services;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MediaserverExtension
    {
        public static void AddMediaserverService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<IMediaserver>(mediaserver => new Mediaserver(serviceProvider.GetService<IConfigProvider>(), new HttpContextAccessor()));
        }
    }
}