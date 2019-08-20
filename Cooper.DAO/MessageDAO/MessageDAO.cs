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
        private readonly DbConnect dbConnect;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public MessageDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            dbConnect = new DbConnect(configProvider);
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

            var whereAttributes = new Dictionary<string, object>()
            {
                {"IDCHAT", chatId }
            };

            IList<EntityORM> entities = Get(table, attributes, whereAttributes);

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
            
            bool isUpdated = crud.Update(message.Id, table, idColumn, entity);

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

        private IList<EntityORM> Get(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes)
        {
            IList<EntityORM> entities = null;

            try
            {
                dbConnect.OpenConnection();

                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string sqlExpression = String.Format("SELECT * FROM {0} WHERE {1}", table, where_part);

                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    entities = new List<EntityORM>();

                    do
                    {
                        var entity = new EntityORM();

                        foreach (string attribute in attributes)
                        {
                            object value = reader[attribute];
                            entity.attributeValue.Add(attribute, value);
                        }
                        entities.Add(entity);

                    } while (reader.Read());
                }

            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return entities;
        }
    }
}
