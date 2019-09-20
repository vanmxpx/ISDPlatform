using System;
using System.Collections.Generic;
using System.Text;
using Cooper.Services;
using Cooper.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SessionServiceExtension
    {
        public static void AddDatabaseSessionService(this IServiceCollection services)
        {
            services.AddScoped<ISessionService, OracleSessionService>();
        }
    }
}
