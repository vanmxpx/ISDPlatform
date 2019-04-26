using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;

namespace Cooper.Repository
{
    public class ChatRepository
    {
        private ChatDAO chatDAO;
        private ModelsMapper mapper;

        public ChatRepository()
        {
            chatDAO = new ChatDAO();
            mapper = new ModelsMapper();
        }

        public IEnumerable<Chat> GetAll()
        {
            List<ChatDb> chats = (List<ChatDb>)chatDAO.GetAll();

            List<Chat> chats_newType = new List<Chat>();

            foreach (ChatDb chat in chats)
            {
                Chat chat_newType = mapper.Map(chat);

                chats_newType.Add(chat_newType);
            }

            return chats_newType;
        }
        
        public Chat Get(long id)
        {
            ChatDb chat = chatDAO.GetExtended(id);
            Chat chat_newTyped = null;

            if (chat != null)
            {
                chat_newTyped = mapper.Map(chat);
            }

            return chat_newTyped;
        }

        public long Create(Chat chat)
        {
            ChatDb chatDb = mapper.Map(chat);

            return chatDAO.Save(chatDb);
        }

        public void Update(Chat chat)
        {
            ChatDb chatDb = mapper.Map(chat);

            chatDAO.Update(chatDb);
        }

        public void Delete(long id)
        {
            chatDAO.Delete(id);
        }

    }
}
