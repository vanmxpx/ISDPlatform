using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.Models.UserConnectionsEnumTypes;

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
