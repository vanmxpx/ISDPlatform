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

        public ChatRepository(IMessageRepository messageRepository, IRepository<User> userRepository, ISession session)
        {
            chatDAO = new ChatDAO(session);
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

                    DefineOneToOneChatProperties(chat_newTyped, userId);
                    
                    personalChats_newTyped.Add(chat_newTyped);
                }
                
            }
            
            return personalChats_newTyped;
        }

        private void DefineOneToOneChatProperties(Chat chat, long userId)
        {
            chat.PhotoURL = (userId == chat.Participants[0].Id) ? chat.Participants[1].PhotoURL : chat.Participants[0].PhotoURL;

            chat.ChatName = (userId == chat.Participants[0].Id) ? 
                (chat.Participants[1].Name == "") ? chat.Participants[1].Nickname : chat.Participants[1].Name :
                (chat.Participants[0].Name == "") ? chat.Participants[0].Nickname : chat.Participants[0].Name;

            chat.UnreadMessagesAmount = 0;

            for (int i = chat.Messages.Count-1; i != -1 && !chat.Messages[i].IsRead && chat.Messages[i].SenderId != userId; i--)
            {
                chat.UnreadMessagesAmount++;
            }
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

        public bool Update(Chat chat)
        {
            bool updated = false;
            ChatDb chatDb = mapper.Map(chat);

            updated = chatDAO.Update(chatDb);

            return updated;
        }

        public bool Delete(long chatId)
        {
            bool deleted = false;

            deleted = chatDAO.Delete(chatId);

            return deleted;
        }
    }
}
