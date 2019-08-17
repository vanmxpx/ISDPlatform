using Cooper.Services;
using Cooper.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResetPasswordServiceExtension
    {
        public static void AddResetPasswordService(this IServiceCollection services)
        {
            services.AddTransient<IResetPasswordService, ResetPasswordService>();
        }
    }
}
