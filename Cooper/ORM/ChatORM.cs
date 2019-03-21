﻿using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{
    public class ChatORM : IORM<Chat>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connection = DbConnecting.GetConnection();

        public long Add(Chat chat)
        {
            long insertId = -1;
            using (connection)
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "insert into chats (chatName) values(:name) returning idChat into :id";
                    cmd.Parameters.Add("name", chat.ChatName);
                    cmd.Parameters.Add(new OracleParameter
                    {
                        ParameterName = ":id",
                        OracleDbType = OracleDbType.Long,
                        Direction = ParameterDirection.Output
                    });
                    cmd.ExecuteNonQuery();
                    insertId = Convert.ToInt64(cmd.Parameters[":id"].Value);
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return insertId;
        }

        public int Delete(long id)
        {
            int rowsDeleted = 0;
            using (connection)
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "delete from chats where idChat = :id";
                    cmd.Parameters.Add("id", id);
                    rowsDeleted = cmd.ExecuteNonQuery();
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return rowsDeleted;
        }

        public IEnumerable<Chat> GetAll()
        {
            List<Chat> lstChat = new List<Chat>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from chats";
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Chat chat = new Chat
                        {
                            id = Convert.ToInt64(reader["idChat"]),
                            ChatName = reader["chatName"].ToString()
                        };
                        lstChat.Add(chat);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return lstChat;
        }

        public Chat GetData(long id)
        {
            Chat chat = new Chat();
            using (connection)
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.BindByName = true;
                    /*cmd.CommandText = "select c.*, u.idUser from chats c, userschats uc, users u " +
                        "where c.idChat = :id and ch.idChat = c.idChat and u.idUser = uc.idUser";*/
                    cmd.CommandText = "select * from chats where idChat = :id";
                    cmd.Parameters.Add("id", id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        chat.id = Convert.ToInt32(reader["c.idChat"]);
                        chat.ChatName = reader["c.chatName"].ToString();
                        //...
                        //chat.UsersList.Add(new UserORM().GetEntityData(Convert.ToInt32(reader["u.idUser"])));
                    }
                    //while (reader.Read())
                    //{
                    //    chat.UsersList.Add(new UserORM().GetEntityData(Convert.ToInt32(reader["u.idUser"])));
                    //}
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return chat;
        }


        public int Update(Chat chat)
        {
            int rowsUpdated = 0;
            using (connection)
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "update chats set chatName = :name where idChat = :id";
                    cmd.Parameters.Add("name", chat.ChatName);
                    cmd.Parameters.Add("id", chat.id);
                    rowsUpdated = cmd.ExecuteNonQuery();
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return rowsUpdated;
        }

    }
}

