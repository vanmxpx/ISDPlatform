using System;
using System.Collections.Generic;
using System.Text;
using Cooper.Models;

namespace Cooper.Repositories
{
    public interface IMessageRepository
    {
        IList<Message> GetAllMessagesByChatId(long chatId);
        bool Update(Message message);
        bool Delete(long messageId);
    }
}
