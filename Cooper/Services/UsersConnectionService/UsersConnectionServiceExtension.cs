﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cooper.Services;
using Cooper.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UsersConnectionServiceExtension
    {
        private static IUsersConnectionService userConnectionService;
        public static void AddUserConnectionService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            UsersConnectionService userConnectionService = new UsersConnectionService(serviceProvider.GetService<IJwtHandlerService>(), serviceProvider.GetService<IConfigProvider>());
            services.AddSingleton<IUsersConnectionService>(userConnectionService);
        }
    }
}
