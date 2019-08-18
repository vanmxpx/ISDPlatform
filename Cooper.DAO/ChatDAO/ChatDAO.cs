using System;
using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Cooper.DAO
{
    public class ChatDAO : IChatDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;
        private readonly DbConnect dbConnect;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public ChatDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetCurrentClassLogger();
            dbConnect =  new DbConnect(configProvider);

            table = "CHATS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "CHATNAME", "ISONETOONE"
            };
        }
        
        public IList<ChatDb> GetPersonalChats(long userId)
        {
            IList<ChatDb> personalChats = null;

            var chatAttributes = new HashSet<string>()
            {
                "ID", "ISONETOONE",
            };

            string sqlExpression = String.Format("SELECT IDCHAT as ID, ISONETOONE FROM {0} INNER JOIN USERSCHATS ON {0}.ID = USERSCHATS.IDCHAT WHERE IDUSER = {1}", table, userId);

            IList<EntityORM> chatsEntities = ExecuteQuery(chatAttributes, sqlExpression);

            if (chatsEntities != null)
            {
                personalChats = new List<ChatDb>(capacity: chatsEntities.Count);

                foreach (var chatEntity in chatsEntities)
                {
                    ChatDb chat;
                    Mapping.EntityMapping.Map(chatEntity, out chat);

                    var user_attribute = new HashSet<string> { "IDUSER" };
                    sqlExpression = String.Format("SELECT IDUSER FROM USERSCHATS WHERE IDCHAT = {0}", chat.Id);
                    IList<EntityORM> participantsEntities = ExecuteQuery(user_attribute, sqlExpression);

                    if (participantsEntities != null)
                    {
                        chat.Participants = new List<long>(capacity: participantsEntities.Count);

                        foreach (var participantEntity in participantsEntities)
                        {
                            long userId1 = Convert.ToInt64(participantEntity.attributeValue["IDUSER"]);
                            chat.Participants.Add(userId1);
                        }
                    }

                    personalChats.Add(chat);
                }
            }

            return personalChats;

        }

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

        private IList<EntityORM> ExecuteQuery(HashSet<string> attributes, string sqlExpression)
        {
            IList<EntityORM> entities = null;

            try
            {
                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    long size = reader.RowSize;
                    entities = new List<EntityORM>(capacity: (int)reader.RowSize);

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
