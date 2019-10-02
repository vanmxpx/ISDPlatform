using System;
using System.Collections.Generic;
using System.Text;
using Cooper.Models;

namespace Cooper.Repositories
{
    public interface IChatRepository
    {
        IList<Chat> GetPersonalChatsByUserId(long userId);

        long Create(Chat chat);

        bool Update(Chat chat);

        bool Delete(long chatId);

        Chat GetOnetoOneChatByParticipants(IList<User> participants);
    }
}
