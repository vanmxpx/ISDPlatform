using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageDAO messageDAO;
        private readonly ModelsMapper mapper;

        public MessageRepository(IConfigProvider configProvider)
        {
            messageDAO = new MessageDAO(configProvider);
            mapper = new ModelsMapper();
        }

        public IList<Message> GetAllMessagesByChatId(long chatId)
        {
            IList<MessageDb> messages = messageDAO.GetAllMessagesByChatId(chatId);
            IList<Message> messages_newType = null;

            if (messages != null)
            {
                messages_newType = new List<Message>();

                foreach (MessageDb message in messages)
                {
                    Message message_newType = mapper.Map(message);

                    messages_newType.Add(message_newType);
                }
            }

            return messages_newType;
        }

        public bool ReadNewMessages(Chat chat)
        {
            bool areRead = false;
            ChatDb chat_newTyped = mapper.Map(chat);

            var messages = new List<long>(capacity: chat.Messages.Count);
            
            for (int i = chat.Messages.Count-1; i != -1 && !chat.Messages[i].IsRead; i--)
            {
                messages.Add(chat.Messages[i].Id);
            }
            if (messages.Count == 0)
            {
                areRead = true;
            }
            else
            {
                areRead = messageDAO.ReadNewMessages(messages);
            }

            return areRead;
        }

        public long Create(Message message)
        {
            MessageDb messageDb = mapper.Map(message);

            return messageDAO.Save(messageDb);
        }

        public bool Update(Message message)
        {
            bool updated = false;
            MessageDb messageDb = mapper.Map(message);

            messageDAO.Update(messageDb);

            return updated;
        }

        public bool Delete(long messageId)
        {
            bool deleted = false;
            messageDAO.Delete(messageId);

            return deleted;
        }

    }
}
