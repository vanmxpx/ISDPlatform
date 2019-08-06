using Microsoft.Extensions.Configuration;

namespace Cooper.Services.Interfaces
{
    public interface IConfigLoader
    {
        IConfigProvider GetConfigProvider(IConfiguration configuration);
    }
}
