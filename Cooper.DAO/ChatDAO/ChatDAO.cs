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
using System.Data;

namespace Cooper.DAO
{
    public class ChatDAO : IChatDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;
        private readonly ISession session;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public ChatDAO(ISession session)
        {
            crud = new CRUD(session);
            this.session = session;
            logger = LogManager.GetCurrentClassLogger();

            table = "CHATS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "CHATNAME", "PHOTOURL"
            };
        }
        
        public IList<ChatDb> GetPersonalChats(long userId)
        {
            IList<ChatDb> personalChats = null;

            var chatAttributes = new HashSet<string>()
            {
                "ID"
            };

            string sqlExpression = String.Format("SELECT ID FROM {0} WHERE EXISTS ( SELECT IDCHAT FROM USERSCHATS WHERE {0}.ID = USERSCHATS.IDCHAT AND IDUSER = {1})", table, userId);

            IList<EntityORM> chatsEntities = ExecuteQuery(chatAttributes, sqlExpression);

            if (chatsEntities != null && chatsEntities.Count != 0)
            {
                personalChats = new List<ChatDb>(capacity: chatsEntities.Count);
                var chatsIndexes = new Dictionary<long, int>(capacity: chatsEntities.Count);    // helping collection

                for (int i = 0; i < chatsEntities.Count; i++)
                {
                    ChatDb chat;
                    Mapping.EntityMapping.Map(chatsEntities[i], out chat);
                    chat.Participants = new List<long>();

                    personalChats.Add(chat);

                    chatsIndexes.Add(chat.Id, i);
                }

                var user_attributes = new HashSet<string> { "IDUSER", "IDCHAT" };
                string[] chatsId = personalChats.Select(t => t.Id.ToString()).ToArray();

                IList<EntityORM> participantsEntities = crud.Read(
                    table: "USERSCHATS",
                    attributes: user_attributes,
                    whereRequest: new WhereRequest("IDCHAT", Operators.In, chatsId)) as List<EntityORM>;

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

            string sqlExpression = "SELECT ID, CHATNAME, PHOTOURL FROM CHATS " +
                                    "WHERE EXISTS " +
                                    "(SELECT record1.idchat, record1.iduser FROM USERSCHATS record1 " +
                                    "WHERE " +
                                    "(SELECT COUNT(IDCHAT) FROM USERSCHATS record2 " +
                                    $"WHERE record1.idchat = record2.idchat AND (IDUSER = {participantsId[0]} OR IDUSER = {participantsId[1]})) = 2 " +
                                    "AND CHATS.ID = record1.IDCHAT) ";

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

        public bool Delete(long id)
        {
            bool isDeleted = crud.Delete(id, table, idColumn);

            if (isDeleted)
            {
                logger.Info($"Game with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting chat with id={id} was failed.");
            }

            return isDeleted;
        }

        public bool Update(ChatDb chat)
        {
            EntityORM entity = EntityMapping.Map(chat, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            bool isUpdated = crud.Update(table, entity, new WhereRequest(idColumn, Operators.Equal, chat.Id.ToString()));

            if (isUpdated)
            {
                logger.Info($"Game with id={chat.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating chat with id={chat.Id} was failed.");
            }

            return isUpdated;
        }

        private IList<EntityORM> ExecuteQuery(HashSet<string> attributes, string sqlExpression)
        {
            // TODO: It may be better to move it to CRUD

            IList<EntityORM> entities = null;

            try
            {
                var connection = (OracleConnection)session.GetConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OracleCommand command = new OracleCommand(sqlExpression, connection);
                command.Transaction = (OracleTransaction)session.GetTransaction();

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

            return entities;
        }
    }
}
