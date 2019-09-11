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
                "ID", "CHATNAME"
            };
        }
        
        public IList<ChatDb> GetPersonalChats(long userId)
        {
            IList<ChatDb> personalChats = null;

            var chatAttributes = new HashSet<string>()
            {
                "ID"
            };

            string sqlExpression = String.Format("SELECT IDCHAT as ID FROM {0} INNER JOIN USERSCHATS ON {0}.ID = USERSCHATS.IDCHAT WHERE IDUSER = {1}", table, userId);

            IList<EntityORM> chatsEntities = ExecuteQuery(chatAttributes, sqlExpression);

            if (chatsEntities != null && chatsEntities.Count != 0)
            {
                personalChats = new List<ChatDb>(capacity: chatsEntities.Count);
                var chatsIndexes = new Dictionary<long, int>(capacity: chatsEntities.Count);    // helping collection

                for (int i = 0; i  < chatsEntities.Count; i++)
                {
                    ChatDb chat;
                    Mapping.EntityMapping.Map(chatsEntities[i], out chat);
                    chat.Participants = new List<long>();

                    personalChats.Add(chat);

                    chatsIndexes.Add(chat.Id, i);
                }

                var user_attribute = new HashSet<string> { "IDUSER", "IDCHAT" };
                sqlExpression = "SELECT IDUSER, IDCHAT FROM USERSCHATS ";

                for (int i = 0; i < personalChats.Count; i++)
                {
                    sqlExpression += (i == 0) ? "WHERE " : "OR ";
                    sqlExpression += $"IDCHAT = {personalChats[i].Id} ";
                }

                IList<EntityORM> participantsEntities = ExecuteQuery(user_attribute, sqlExpression);

                if (participantsEntities != null)
                {

                    foreach (var participantEntity in participantsEntities)
                    {
                        long participantId = Convert.ToInt64(participantEntity.attributeValue["IDUSER"]);
                        long chatId = Convert.ToInt64(participantEntity.attributeValue["IDCHAT"]);

                        personalChats[chatsIndexes[chatId]].Participants.Add(participantId);
                    }
                }
            }

            return personalChats;

        }

        public ChatDb GetOnetoOneChatByParticipantsId(IList<long> participantsId)
        {
            ChatDb chat = null;

            string sqlExpression = "SELECT chat.id, chat.chatname FROM CHATS chat " +
                                    "INNER JOIN " +
                                    "(SELECT record1.idchat, record1.iduser FROM USERSCHATS record1 " +
                                    "WHERE " +
                                    "(SELECT COUNT(IDCHAT) FROM USERSCHATS record2 " +
                                    $"WHERE record1.idchat = record2.idchat AND (IDUSER = {participantsId[0]} OR IDUSER = {participantsId[1]})) = 2 AND ROWNUM = 1) " +
                                    "record on chat.id = record.idchat ";

            IList<EntityORM> chats = ExecuteQuery(attributes, sqlExpression);


            if (chats != null && chats.Count == 1)   // we must have only one one-to-one chat
            {
                Mapping.EntityMapping.Map(chats[0], out chat);
            }

            return chat;
        }

        public long Save(ChatDb chat)
        {
            EntityORM entity = EntityMapping.Map(chat, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");     

            long idChat = crud.Create(table, idColumn, entity);
            
            if (idChat != 0)
            {
                string relatedTable = "USERSCHATS";

                foreach (var idUser in chat.Participants)
                {
                    EntityORM userChat = new EntityORM();
                    userChat.attributeValue.Add("IDUSER", idUser);
                    userChat.attributeValue.Add("IDCHAT", idChat);

                    crud.Create(relatedTable, idColumn, userChat);
                }
            }

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

            bool ifUpdated = crud.Update(table, entity, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, chat.Id) });

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
