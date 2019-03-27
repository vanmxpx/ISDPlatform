using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{
    public class MessageORM : IORM<Message>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection Connection = DbConnecting.GetConnection();
        //static DbConnecting connect = DbConnecting.getInstance();
        //public OracleConnection Connection { get; set; } = connect.GetConnection();

        public long Add(Message message)
        {
            long insertId = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "insert into messages (idSender, idChat, Content, CreateDate, isRead) values(:iduser, :idchat, :content, :date, :isread) returning idMessage into :id";
                    cmd.Parameters.Add("idchat", message.idChat);
                    cmd.Parameters.Add("iduser", message.idUser);
                    cmd.Parameters.Add("content", message.Content);
                    cmd.Parameters.Add("date", message.CreateDate);
                    cmd.Parameters.Add("isread", message.isRead);
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
            int rowsDeleted = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "delete from messages where idMessage = :id";
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

        public IEnumerable<Message> GetAll()
        {
            List<Message> lstMessage = new List<Message>();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "select * from messages";
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Message message = new Message
                        {
                            id = Convert.ToInt64(reader["idMessage"]),
                            idChat = Convert.ToInt64(reader["idChat"]),
                            idUser = Convert.ToInt64(reader["idUser"]),
                            Content = Convert.ToString(reader["Content"]),
                            CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                            isRead = Convert.ToBoolean(reader["isRead"])
                        };
                        lstMessage.Add(message);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return lstMessage;
        }

        public Message GetData(long id)
        {
            Message message = new Message();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "select * from messages where idMessage = :id";
                    cmd.Parameters.Add("id", id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        message.id = Convert.ToInt64(reader["idMessage"]);
                        message.idChat = Convert.ToInt64(reader["idChat"]);
                        message.idUser = Convert.ToInt64(reader["idUser"]);
                        message.Content = Convert.ToString(reader["Content"]);
                        message.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                        message.isRead = Convert.ToBoolean(reader["isRead"]);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return message;
        }


        public int Update(Message message)
        {
            int rowsUpdated = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "update messages set Content = :content, CreateDate = :date, isRead = :isread where idMessage = :id";
                    cmd.Parameters.Add("content", message.Content);
                    cmd.Parameters.Add("date", message.CreateDate);
                    cmd.Parameters.Add("isread", message.isRead);
                    cmd.Parameters.Add("id", message.id);
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

