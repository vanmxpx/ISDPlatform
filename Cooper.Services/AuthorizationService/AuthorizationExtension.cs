using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationExtension
    {
        public static void AddJWTAuthorization(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var configProvider = serviceProvider.GetService<IConfigProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = false,           // false - while development stage
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = configProvider.JwtToken.Issuer,
                            ValidAudience = configProvider.JwtToken.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configProvider.JwtToken.Key))
                        };
                    });
        }
    }
}