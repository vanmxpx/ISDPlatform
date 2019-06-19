using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;

namespace Cooper.Repository
{
    public interface IUserConnectionRepository
    {
        List<UserConnection> GetUserSubscribers(long userId);
        List<UserConnection> GetUserBlacklist(long userId);
        List<UserConnection> GetUserSubscribtions(long userId);

        bool CreateSubscription(UserConnection userConnection);
        bool BanUser(UserConnection userConnection);
        bool UnbanUser(UserConnection userConnection);
        
        bool Delete(UserConnection userConnection);
    }
}
