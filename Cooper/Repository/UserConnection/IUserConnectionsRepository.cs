using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.Models.UserConnectionsEnumTypes;

namespace Cooper.Repository
{
    public interface IUserConnectionsRepository
    {
        List<User> GetSpecifiedTypeUsersList(long userId, ConnectionType specifiedType);

        bool CreateSubscription(UserConnections userConnection);
        bool BanUser(UserConnections userConnection);
        bool UnbanUser(UserConnections userConnection);
        
        bool Delete(UserConnections userConnection);
    }
}
