using Cooper.Models;
using Cooper.ORM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.DAO
{
    public class ChatDAO : IORM <Chat>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connection = DbConnecting.GetConnection();

        public bool AddEntity(Chat chat)
        {
            int rowsUpdated = 0;
            try
            {
                using (connection)
                {
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        //connection.Open();
                        cmd.CommandText = "insert into chats (idChat, chatName) values(:id, :name)";
                        cmd.Parameters.Add("id", chat.id);
                        cmd.Parameters.Add("name", chat.ChatName);
                        connection.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteEntity(long id)
        {
            int rowsUpdated = 0;
            try
            {
                using (connection)
                {
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "delete from chats where idChat = :id";
                        cmd.Parameters.Add("id", id);
                        connection.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Chat> GetAllEntities()
        {
            try
            {
                List<Chat> lstChat = new List<Chat>();
                using (connection)
                {
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from chats";
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Chat chat = new Chat();
                            chat.id = Convert.ToInt32(reader["idChat"]);
                            chat.ChatName = reader["chatName"].ToString();
                            lstChat.Add(chat);
                        }
                        reader.Dispose();
                        connection.Close();
                    }
                }
                return lstChat;
            }
            catch
            {
                throw;
            }
        }

        public Chat GetEntityData(long id)
        {
            try
            {
                Chat chat = new Chat();
                using (connection)
                {
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select c.*, u.idUser from chats c, userschats uc, users u " +
                            "where c.idChat = :id and ch.idChat = c.idChat and u.idUser = uc.idUser";
                        cmd.Parameters.Add("id", id);
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            chat.id = Convert.ToInt32(reader["c.idChat"]);
                            chat.ChatName = reader["c.chatName"].ToString();
                            //...
                            chat.UsersList.Add(new UserDAO().GetEntityData(Convert.ToInt32(reader["u.idUser"])));
                        }
                        while (reader.Read())
                        {
                            chat.UsersList.Add(new UserDAO().GetEntityData(Convert.ToInt32(reader["u.idUser"])));
                        }
                        reader.Dispose();
                        connection.Close();
                    }
                    return chat;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateEntity(Chat chat)
        {
            int rowsUpdated = 0;
            try
            {
                using (connection)
                {
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "update chats set chatName = :name where idChat = :id";
                        cmd.Parameters.Add("name", chat.ChatName);
                        cmd.Parameters.Add("id", chat.id);
                        connection.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }

    }
}
