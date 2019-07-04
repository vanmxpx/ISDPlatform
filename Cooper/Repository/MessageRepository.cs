using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;
using Cooper.Configuration;
using NLog;

namespace Cooper.Repository
{
    public class MessageRepository
    {
        private MessageDAO messageDAO;
        private ModelsMapper mapper;
        private readonly ILogger logger;

        public MessageRepository(IConfigProvider configProvider, ILogger logger)
        {
            messageDAO = new MessageDAO(configProvider, logger);
            mapper = new ModelsMapper();

            this.logger = logger;

        }

        public IEnumerable<Message> GetAll()
        {
            List<MessageDb> messages = (List<MessageDb>)messageDAO.GetAll();

            List<Message> messages_newType = new List<Message>();

            foreach (MessageDb message in messages)
            {
                Message message_newType = mapper.Map(message);

                messages_newType.Add(message_newType);
            }

            return messages_newType;
        }

        public Message Get(long id)
        {
            MessageDb message = messageDAO.GetExtended(id);
            Message message_newTyped = null;

            if (message != null)
            {
                message_newTyped = mapper.Map(message);
            }

            return message_newTyped;
        }

        public long Create(Message message)
        {
            MessageDb messageDb = mapper.Map(message);

            return messageDAO.Save(messageDb);
        }

        public void Update(Message message)
        {
            MessageDb messageDb = mapper.Map(message);

            messageDAO.Update(messageDb);
        }

        public void Delete(long id)
        {
            messageDAO.Delete(id);
        }

    }
}
