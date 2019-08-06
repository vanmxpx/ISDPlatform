using Cooper.Models;
using System.Collections.Generic;

namespace Cooper.Repositories.CommonChats
{
    public interface ICommonChatRepository
    {
        IEnumerable<CommonMessage> getMessages();
        void addMessage(CommonMessage message);
    }
}
