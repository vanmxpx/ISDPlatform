using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{
    public class UserORM : IORM<User>
    {
        //private string ConnectionString = "Put Your Connection string here";
        private OracleConnection Connection = DbConnecting.GetConnection();
        //static DbConnect connect = DbConnect.getInstance();
        //public OracleConnection Connection { get; set; } = connect.GetConnection();

        public long Add(User user)
        {
            long insertId = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "insert into users (Name, ...) values(:name, ...) returning idUser into :id";
                    cmd.Parameters.Add("name", user.Name);
                    //...
                    cmd.Parameters.Add(new OracleParameter
                    {
                        ParameterName = ":id",
                        OracleDbType = OracleDbType.Long,
                        Direction = ParameterDirection.Output
                    });
                    Connection.Open();
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
                    cmd.CommandText = "delete from users where idUser = :id";
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

        public IEnumerable<User> GetAll()
        {
            List<User> lstUser = new List<User>();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from users";
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            id = Convert.ToInt64(reader["idUser"]),
                            Name = reader["Name"].ToString()
                        };
                        //...
                        lstUser.Add(user);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return lstUser;
        }

        public User GetData(long id)
        {
            User user = new User();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.BindByName = true;
                    //cmd.CommandText = "select u.*, g.idGame from users u, usersgames ug, games g " +
                    //    "where u.idUser = :id and ug.idUser = ug.idUser and g.idGame = ug.idGame";
                    cmd.CommandText = "select * from users where idUser = :id";
                    cmd.Parameters.Add("id", id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.id = Convert.ToInt64(reader["u.idUser"]);
                        user.Name = reader["u.Name"].ToString();
                        //...
                        //user.GamesList.Add(new GameORM().GetEntityData(Convert.ToInt64(reader["g.idGame"])));
                    }
                    //while (reader.Read())
                    //{
                    //    user.GamesList.Add(new GameORM().GetEntityData(Convert.ToInt64(reader["g.idGame"])));
                    //}
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return user;
        }

        public int Update(User user)
        {
            int rowsUpdated = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "update users set Name = :name, ... where idUser = :id";
                    cmd.Parameters.Add("id", user.id);
                    cmd.Parameters.Add("name", user.Name);
                    //...
                    Connection.Open();
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
