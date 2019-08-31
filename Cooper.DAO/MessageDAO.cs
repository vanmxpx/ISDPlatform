using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Cooper.DAO
{
    public class MessageDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public MessageDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetLogger("CooperLoger");

            table = "MESSAGES";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "IDSENDER", "IDCHAT", "CONTENT", "CREATEDATE", "ISREAD"
            };
        }

        #region Get methods

        public MessageDb Get(long id)
        {
            MessageDb message = null;

            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entities.Any()) {
                EntityMapping.Map(entities[0], out message);
            }

            return message;
        }

        /// <summary>
        /// Return the MessageDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageDb GetExtended(long id)
        {
            MessageDb message = Get(id);

            //message.PlayersList = GetPlayersList(id);

            return message;
        }

        public IEnumerable<MessageDb> GetAll()
        {
            List<MessageDb> messages = new List<MessageDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to messages
            {
                EntityMapping.Map(entity, out MessageDb message);
                messages.Add(message);
            }

            return messages;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion

        #endregion

        public long Save(MessageDb message)
        {
            EntityORM entity = EntityMapping.Map(message, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");     

            long idMessage = crud.Create(table, idColumn, entity);

            return idMessage;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

            if (ifDeleted)
            {
                logger.Info($"Game with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting message with id={id} was failed.");
            }

        }

        public void Update(MessageDb message)
        {
            EntityORM entity = EntityMapping.Map(message, attributes);

            // Making sure that ID value is not touched
            entity.attributeValue.Remove("ID");
            
            bool ifUpdated = crud.Update(table, entity, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, message.Id) });

            if (ifUpdated)
            {
                logger.Info($"Game with id={message.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating message with id={message.Id} was failed.");
            }
        }
    }
}
