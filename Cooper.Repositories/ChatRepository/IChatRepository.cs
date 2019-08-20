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

        void Update(Chat chat);

        void Delete(long chatId);
    }
}
