using Cooper.Models;
using Cooper.Models.UserConnectionsEnumTypes;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public interface IUsersConnectionRepository
    {
        List<User> GetSpecifiedTypeUsersList(long userId, ConnectionType specifiedType);

        bool CreateSubscription(UsersConnection usersConnection);
        bool BanUser(UsersConnection usersConnection);
        bool UnbanUser(UsersConnection usersConnection);
        bool Unsubscribe(UsersConnection usersConnection);
    }
}
