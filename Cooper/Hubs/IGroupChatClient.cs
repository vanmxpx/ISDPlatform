using Cooper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Hubs {
    public interface IGroupChatClient
    {
        Task ReceiveMessage(string receiver, Message message);

        Task ReceiveMessage(Message message);
    }
}