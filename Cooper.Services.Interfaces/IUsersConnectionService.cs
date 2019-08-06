using Cooper.Models;

namespace Cooper.Services.Interfaces
{
    public interface IUsersConnectionService
    {
        UsersConnection CreateConnection(long userId, string subscriberToken, bool ban = false);

        UsersConnection CreateConnection(string userToken, long subscriberId, bool ban = false);

        long GetUserId(string userToken);
    }
}
