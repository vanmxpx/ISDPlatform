using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
namespace Cooper.Services
{
    public interface IUserConnectionService
    {
        UserConnection CreateConnection(long userId, string subscriberToken);
    }
}
