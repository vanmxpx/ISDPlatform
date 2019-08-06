using Cooper.Models;
using System;
using System.Collections.Generic;

namespace Cooper.Repositories.CommonChats
{
    public class CommonChatRepository : ICommonChatRepository
    {
        private IList<CommonMessage> messages = new List<CommonMessage>();

        public CommonChatRepository()
        {
            var timer = new System.Threading.Timer(
                         e => clearCommonChats(),
                        null,
                        TimeSpan.Zero,
                        TimeSpan.FromDays(1));
        }  

        public void addMessage(CommonMessage message)
        {
            messages.Add(message);
        }

        public IEnumerable<CommonMessage> getMessages()
        {
            return messages;
        }

        private void clearCommonChats()
        {
            messages.Clear();
        }
    }
}
