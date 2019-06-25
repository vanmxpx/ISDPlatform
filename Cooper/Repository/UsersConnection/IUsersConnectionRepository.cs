using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.Models.UserConnectionsEnumTypes;

namespace Cooper.Repository
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
