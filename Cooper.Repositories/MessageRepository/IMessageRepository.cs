using System;
using System.Collections.Generic;
using System.Text;
using Cooper.Models;

namespace Cooper.Repositories
{
    public interface IMessageRepository
    {
        IList<Message> GetAllMessagesByChatId(long chatId);
        bool ReadNewMessages(Chat chat);
        long Create(Message message);
        bool Update(Message message);
        bool Delete(long messageId);
    }
}
