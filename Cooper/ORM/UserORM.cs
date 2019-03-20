using Cooper.Models;
using Cooper.ORM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.ORM
{
    public class UserORM : IORM <User>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connect = DbConnecting.GetConnection();

        public bool AddEntity(User user)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        //connect.Open();
                        cmd.CommandText = "insert into users (idUser, Name) values(:id, :name)";
                        cmd.Parameters.Add("id", user.id);
                        cmd.Parameters.Add("name", user.Name);
                        //...
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

        public bool DeleteEntity(long id)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "delete from users where idUser = :id";
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

        public IEnumerable<User> GetAllEntities()
        {
            try
            {
                List<User> lstUser = new List<User>();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from users";
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                id = Convert.ToInt32(reader["idUser"]),
                                Name = reader["Name"].ToString()
                            };
                            //...
                            lstUser.Add(user);
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                }
                return lstUser;
            }
            catch
            {
                throw;
            }
        }

        public User GetEntityData(long id)
        {
            try
            {
                User user = new User();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select u.*, g.idGame from users u, usersgames ug, games g " +
                            "where u.idUser = :id and ug.idUser = ug.idUser and g.idGame = ug.idGame";
                        cmd.Parameters.Add("id", id);
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            user.id = Convert.ToInt32(reader["u.idUser"]);
                            user.Name = reader["u.Name"].ToString();
                            //...
                            user.GamesList.Add(new GameORM().GetEntityData(Convert.ToInt32(reader["g.idGame"])));
                        }
                        while (reader.Read())
                        {
                            user.GamesList.Add((Game)reader["g.*"]);
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                    return user;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateEntity(User user)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "update users set Name = :name where idUser = :id";
                        cmd.Parameters.Add("id", user.id);
                        cmd.Parameters.Add("name", user.Name);
                        //...
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
