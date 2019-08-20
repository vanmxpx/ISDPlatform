using System.Collections.Generic;
using Cooper.DAO.Models;

namespace Cooper.DAO
{
    public interface IMessageDAO
    {
        IList<MessageDb> GetAllMessagesByChatId(long chatId);
        bool Update(MessageDb message);
        bool Delete(long messageId);
    }
}
