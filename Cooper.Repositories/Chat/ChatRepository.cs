using Cooper.Configuration;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDAO chatDAO;
        private readonly ModelsMapper mapper;

        public ChatRepository(IConfigProvider configProvider)
        {
            chatDAO = new ChatDAO(configProvider);
            mapper = new ModelsMapper();
        }

        public IList<Chat> GetPersonalChatsByUserId(long userId)
        {
            var personalChats = new List<ChatDb>();

            var personalChats_newTyped = new List<Chat>(capacity: personalChats.Count);

            foreach (var chat in personalChats)
            {
                personalChats_newTyped.Add(mapper.Map(chat));
            }


            return personalChats_newTyped;
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

        public void Delete(long chatId)
        {
            chatDAO.Delete(chatId);
        }

    }
}
