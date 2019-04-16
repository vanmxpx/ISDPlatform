using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;

namespace Cooper.DAO
{
    public class MessageDAO
    {
        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public MessageDAO()
        {
            crud = new CRUD();
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

            EntityORM entity = crud.Read(id, idColumn, attributes, table);

            if (entity != null)
                EntityMapping.Map(entity, out message);

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
            List<MessageDb> userReviews = new List<MessageDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.ReadAll(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to userReviews
            {
                EntityMapping.Map(entity, out MessageDb message);
                userReviews.Add(message);
            }

            return userReviews;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion
        #endregion

        public long Save(MessageDb message)
        {
            EntityORM entity = EntityMapping.Map(message, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

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

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched
            
            bool ifUpdated = crud.Update(message.Id, table, idColumn, entity);

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
