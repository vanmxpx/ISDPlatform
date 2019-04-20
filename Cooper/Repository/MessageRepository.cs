using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class MessageRepository
    {
        private MessageDAO messageDAO;

        public MessageRepository()
        {
            messageDAO = new MessageDAO();
        }

        public IEnumerable<Message> GetAll()
        {
            List<MessageDb> messages = (List<MessageDb>)messageDAO.GetAll();

            List<Message> messages_newType = new List<Message>();

            foreach (MessageDb message in messages)
            {
                //Message message_newType = mapper.Map<Message>(message);
                Message message_newType = MessageMap(message);

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
                message_newTyped = Mapper.Map<Message>(message);
                //Message message_newTyped = MessageMap(message);
            }

            return message_newTyped;
        }

        public long Create(Message message)
        {
            MessageDb messageDb = MessageMap(message);

            return messageDAO.Save(messageDb);
        }

        public void Update(Message message)
        {
            MessageDb messageDb = MessageMap(message);

            messageDAO.Update(messageDb);
        }

        public void Delete(long id)
        {
            messageDAO.Delete(id);
        }

        #region Mapping
        private Message MessageMap(MessageDb message)
        {
            Message message_newType = new Message();

            #region Transfer main attributes

            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;

            #endregion

            #region Transfering interop attributes

            message_newType.IdChat = new Chat() { Id = message.IdChat };
            message_newType.IdSender = new User() { Id = message.IdSender };

            #endregion

            return message_newType;
        }

        private MessageDb MessageMap(Message message)
        {
            MessageDb message_newType = new MessageDb();

            #region Transfer main attributes

            message_newType.Id = message.Id;
            message_newType.Content = message.Content;
            message_newType.CreateDate = message.CreateDate;
            message_newType.IsRead = message.IsRead;


            #endregion

            #region Transfering interop attributes

            message_newType.IdChat = message.IdChat.Id;
            message_newType.IdSender = message.IdSender.Id;

            #endregion

            return message_newType;
        }

        #endregion
    }
}
