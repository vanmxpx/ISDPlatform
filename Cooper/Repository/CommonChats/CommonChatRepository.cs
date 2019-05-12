using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;

namespace Cooper.Repository.CommonChats
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
                        TimeSpan.FromMinutes(1440));
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
