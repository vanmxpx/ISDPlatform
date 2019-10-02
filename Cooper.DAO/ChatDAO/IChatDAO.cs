using System;
using System.Collections.Generic;
using System.Text;
using Cooper.DAO.Models;

namespace Cooper.DAO
{
    public interface IChatDAO
    {
        IList<ChatDb> GetPersonalChats(long userId);

        ChatDb GetOnetoOneChatByParticipantsId(IList<long> participantsId);

        long Save(ChatDb chat);

        bool Delete(long id);

        bool Update(ChatDb chat);
    }
}
