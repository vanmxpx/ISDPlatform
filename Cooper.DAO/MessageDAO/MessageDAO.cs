using System;
using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace Cooper.DAO
{
    public class MessageDAO : IMessageDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public MessageDAO(ISession session)
        {
            crud = new CRUD(session);
            logger = LogManager.GetLogger("CooperLoger");

            table = "MESSAGES";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "IDSENDER", "IDCHAT", "CONTENT", "CREATEDATE", "ISREAD"
            };
        }

        public IList<MessageDb> GetAllMessagesByChatId(long chatId)
        {
            IList<MessageDb> messages = null;

            IList<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, new WhereRequest("IDCHAT", Operators.Equal, chatId.ToString()));

            if (entities != null)
            {
                messages = new List<MessageDb>();
                foreach (var entity in entities)
                {
                    EntityMapping.Map(entity, out MessageDb message);
                    messages.Add(message);
                }
            }

            return messages;
        }

        public bool ReadNewMessages(IList<long> messages)
        {
            bool areRead = crud.Update(
                table,
                new EntityORM() { attributeValue = { { "ISREAD", "\'y\'" } } },
                new WhereRequest("ID", Operators.In, messages.Select(_ => _.ToString()).ToArray()));
            return areRead;
        }
        
        public long Save(MessageDb message)
        {
            EntityORM entity = EntityMapping.Map(message, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");     

            long idMessage = crud.Create(table, idColumn, entity);

            return idMessage;
        }

        public bool Delete(long messageId)
        {
            bool isDeleted = crud.Delete(messageId, table, idColumn);

            if (isDeleted)
            {
                logger.Info($"Game with messageId={messageId} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting message with id={messageId} was failed.");
            }

            return isDeleted;
        }

        public bool Update(MessageDb message)
        {
            EntityORM entity = EntityMapping.Map(message, attributes);

            // Making sure that ID value is not touched
            entity.attributeValue.Remove("ID");
            
            bool isUpdated = crud.Update(table, entity, new WhereRequest(idColumn, Operators.Equal, message.Id.ToString()));

            if (isUpdated)
            {
                logger.Info($"Message with id={message.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating message with messageId={message.Id} was failed.");
            }

            return isUpdated;
        }
    }
}
