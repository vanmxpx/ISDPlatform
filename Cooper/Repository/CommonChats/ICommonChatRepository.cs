using Cooper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Repository.CommonChats
{
    public interface ICommonChatRepository
    {
        IEnumerable<CommonMessage> getMessages();
        void addMessage(CommonMessage message);
    }
}
