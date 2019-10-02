using Cooper.Models;

namespace Cooper.Services.Interfaces
{
    public interface IUsersConnectionService
    {
        UsersConnection CreateConnection(long userId, string subscriberToken, ISession session, bool ban = false);

        UsersConnection CreateConnection(string userToken, long subscriberId, ISession session, bool ban = false);

        long GetUserId(string userToken, ISession session);
    }
}
