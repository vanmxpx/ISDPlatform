using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.DAO
{
    public class ChatDataAccessLayer
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connect = DataAccessLayer.GetConnection();
        //To View all chats details
        public IEnumerable<Chat> GetAllChats()
        {
            try
            {
                List<Chat> lstChat = new List<Chat>();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from chats";
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Chat chat = new Chat();
                            chat.idChat = Convert.ToInt32(reader["idChat"]);
                            chat.ChatName = reader["chatName"].ToString();
                            lstChat.Add(chat);
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                }
                return lstChat;
            }
            catch
            {
                throw;
            }
        }
        //To Add new chat record 
        public bool AddChat(Chat chat)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        //connect.Open();
                        cmd.CommandText = "insert into chats (idChat, chatName) values(:id, :name)";
                        cmd.Parameters.Add("id", chat.idChat);
                        cmd.Parameters.Add("name", chat.ChatName);
                        connect.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connect.Close();
                    }
                }
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar chat
        public bool UpdateChat(Chat chat)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "update chats set chatName = :name where idChat = :id";
                        cmd.Parameters.Add("id", chat.idChat);
                        cmd.Parameters.Add("name", chat.ChatName);
                        connect.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connect.Close();
                    }
                }
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular chat
        public Chat GetChatData(int id)
        {
            try
            {
                Chat chat = new Chat();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from chats where idChat = :id";
                        cmd.Parameters.Add("id", chat.idChat);
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            chat.idChat = Convert.ToInt32(reader["idChat"]);
                            chat.ChatName = reader["chatName"].ToString();
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                    return chat;
                }
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record on a particular employee
        public bool DeleteChat(int id)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "delete from chats where idChat = :id";
                        cmd.Parameters.Add("id", id);
                        connect.Open();
                        rowsUpdated = cmd.ExecuteNonQuery();
                        connect.Close();
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
