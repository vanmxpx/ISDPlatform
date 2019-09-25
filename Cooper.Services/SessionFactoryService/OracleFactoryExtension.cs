using System;
using System.Collections.Generic;
using System.Text;
using Cooper.Services;
using Cooper.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OracleFactoryExtension
    {
        public static void AddOracleSessionFactory(this IServiceCollection services)
        {
            services.AddSingleton<ISessionFactory, OracleSessionFactory>();
        }
    }
}
