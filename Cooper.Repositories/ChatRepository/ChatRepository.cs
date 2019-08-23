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

                    chat_newTyped.Participants = ((IUserRepository)userRepository).GetUsersById(chat.Participants);

                    personalChats_newTyped.Add(chat_newTyped);
                }
                
            }
            
            return personalChats_newTyped;
        }

        public long Create(Chat chat)
        {
            ChatDb chatDb = mapper.Map(chat);
            
            long chatId = chatDAO.Save(chatDb);

            if (chatId != 0)
            {
                Message firstChatMsg = chat.Messages[0];
                firstChatMsg.ChatId = chatId;

                messageRepository.Create(firstChatMsg);
            }

            return chatId;
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
