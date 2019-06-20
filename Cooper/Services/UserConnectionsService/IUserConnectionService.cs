using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
namespace Cooper.Services
{
    public interface IUserConnectionService
    {
        UserConnections CreateConnection(long userId, string subscriberToken, bool ban = false);

        long GetUserId(string userToken);
    }
}
