using Cooper.DAO.Models;
using Cooper.Models.UserConnectionsEnumTypes;
using System.Collections.Generic;

namespace Cooper.DAO
{
    public interface IUsersConnectionDAO
    {
        List<UserDb> GetSpecifiedTypeUsersList(object userId, ConnectionType connectionType);

        bool CreateConnection(UsersConnectionDb usersConnection);
        bool Unsubscribe(UsersConnectionDb usersConnection);

        bool BanUser(UsersConnectionDb usersConnection);

        bool UnbanUser(UsersConnectionDb usersConnection);
    }
}
