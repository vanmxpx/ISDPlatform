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
    public class ChatDAO
    {
        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public ChatDAO()
        {
            crud = new CRUD();
            logger = LogManager.GetLogger("CooperLoger");

            table = "CHATS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "CHATNAME"
            };
        }

        #region Get methods

        public ChatDb Get(long id)
        {
            ChatDb chat = null;

            EntityORM entity = crud.Read(id, idColumn, attributes, table);

            if (entity != null)
                EntityMapping.Map(entity, out chat);

            return chat;
        }

        /// <summary>
        /// Return the ChatDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChatDb GetExtended(long id)
        {
            ChatDb chat = Get(id);

            //chat.PlayersList = GetPlayersList(id);

            return chat;
        }

        public IEnumerable<ChatDb> GetAll()
        {
            List<ChatDb> userReviews = new List<ChatDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.ReadAll(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to userReviews
            {
                EntityMapping.Map(entity, out ChatDb chat);
                userReviews.Add(chat);
            }

            return userReviews;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion
        #endregion

        public long Save(ChatDb chat)
        {
            EntityORM entity = EntityMapping.Map(chat, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            long idChat = crud.Create(table, idColumn, entity);

            return idChat;
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
                logger.Info($"Deleting chat with id={id} was failed.");
            }

        }

        public void Update(ChatDb chat)
        {
            EntityORM entity = EntityMapping.Map(chat, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            bool ifUpdated = crud.Update(chat.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"Game with id={chat.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating chat with id={chat.Id} was failed.");
            }
        }
    }
}
