using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;

namespace Cooper.Repository
{
    public interface IUserConnectionsRepository
    {
        List<UserConnections> GetUserSubscribers(long userId);
        List<UserConnections> GetUserBlacklist(long userId);
        List<UserConnections> GetUserSubscriptions(long userId);

        bool CreateSubscription(UserConnections userConnection);
        bool BanUser(UserConnections userConnection);
        bool UnbanUser(UserConnections userConnection);
        
        bool Delete(UserConnections userConnection);
    }
}
