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
        private readonly IMessageRepository messageRepository;
        private readonly IRepository<User> userRepository;

        public ChatRepository(IConfigProvider configProvider, IMessageRepository messageRepository, IRepository<User> userRepository)
        {
            chatDAO = new ChatDAO(configProvider);
            mapper = new ModelsMapper();

            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        public IList<Chat> GetPersonalChatsByUserId(long userId)
        {
            IList<ChatDb> personalChats = chatDAO.GetPersonalChats(userId);

            IList<Chat> personalChats_newTyped = null;

            if (personalChats != null)
            {
                personalChats_newTyped = new List<Chat>(capacity: personalChats.Count);


                foreach (var chat in personalChats)
                {
                    Chat chat_newTyped = mapper.Map(chat);
                    chat_newTyped.Messages = messageRepository.GetAllMessagesByChatId(chat_newTyped.Id);

                    for (int i = 0; i < chat_newTyped.Participants.Count; i++)
                    {
                        chat_newTyped.Participants[i] = userRepository.Get(chat_newTyped.Participants[i].Id);
                    }

                    personalChats_newTyped.Add(chat_newTyped);
                }
                
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
