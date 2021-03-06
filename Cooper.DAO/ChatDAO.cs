﻿using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Cooper.DAO
{
    public class ChatDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public ChatDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetCurrentClassLogger();

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

            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entities.Any()) {
                EntityMapping.Map(entities[0], out chat);
            }

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
            List<ChatDb> chats = new List<ChatDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes);

            foreach (EntityORM entity in entities)
            {
                EntityMapping.Map(entity, out ChatDb chat);
                chats.Add(chat);
            }

            return chats;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion

        #endregion

        public long Save(ChatDb chat)
        {
            EntityORM entity = EntityMapping.Map(chat, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");     

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
