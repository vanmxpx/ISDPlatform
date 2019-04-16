using System;
using System.Collections.Generic;
using Cooper.Models;c
using Cooper.DAO;
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class ChatRepository
    {
        private ChatDAO chatDAO;

        public ChatRepository()
        {
            chatDAO = new ChatDAO();
        }

        public IEnumerable<Chat> GetAll()
        {
            List<ChatDb> chats = (List<ChatDb>)chatDAO.GetAll();

            List<Chat> chats_newType = new List<Chat>();

            foreach (ChatDb chat in chats)
            {
                //Chat chat_newType = mapper.Map<Chat>(chat);
                Chat chat_newType = ChatMap(chat);

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
                chat_newTyped = Mapper.Map<Chat>(chat);
                //Chat chat_newTyped = ChatMap(chat);
            }

            return chat_newTyped;
        }

        public long Create(Chat chat)
        {
            ChatDb chatDb = ChatMap(chat);

            return chatDAO.Save(chatDb);
        }

        public void Update(Chat chat)
        {
            ChatDb chatDb = ChatMap(chat);

            chatDAO.Update(chatDb);
        }

        public void Delete(long id)
        {
            chatDAO.Delete(id);
        }

        #region Mapping
        private Chat ChatMap(ChatDb chat)
        {
            Chat chat_newType = new Chat();

            #region Transfer main attributes

            chat_newType.Id = chat.Id;
            chat_newType.ChatName = chat.ChatName;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return chat_newType;
        }

        private ChatDb ChatMap(Chat chat)
        {
            ChatDb chat_newType = new ChatDb();

            #region Transfer main attributes

            chat_newType.Id = chat.Id;
            chat_newType.ChatName = chat.ChatName;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return chat_newType;
        }

        #endregion
    }
}
