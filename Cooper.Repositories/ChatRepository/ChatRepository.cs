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
        private readonly IChatDAO chatDAO;
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

        public Chat GetOnetoOneChatByParticipants(IList<User> participants)
        {
            if (participants == null || participants.Count != 2)
            {
                return null;
            }

            IList<long> participantsId = new List<long>(capacity: 2);

            for (int i = 0; i < participants.Count; i++)
            {
                participantsId.Add(participants[i].Id);
            }

            ChatDb chat = chatDAO.GetOnetoOneChatByParticipantsId(participantsId);
            
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
            
            long chatId = chatDAO.Save(chatDb);

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
