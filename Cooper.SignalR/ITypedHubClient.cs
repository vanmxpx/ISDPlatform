using Cooper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(CommonMessage commonMessage);
    }
}
