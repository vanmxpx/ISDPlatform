﻿using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.DAO
{
    public class UserDataAccessLayer
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connect = DataAccessLayer.GetConnection();
        //To View all users details
        public IEnumerable<User> GetAllUsers()
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
                            User user = new User();
                            user.idUser = Convert.ToInt32(reader["idUser"]);
                            user.Name = reader["Name"].ToString();
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
        //To Add new user record 
        public bool AddUser(User user)
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
                        cmd.Parameters.Add("id", user.idUser);
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
        //To Update the records of a particluar user
        public bool UpdateUser(User user)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "update users set Name = :name where idUser = :id";
                        cmd.Parameters.Add("id", user.idUser);
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
        //Get the details of a particular user
        public User GetUserData(int id)
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
                        cmd.CommandText = "select * from users where idUser = :id";
                        cmd.Parameters.Add("id", user.idUser);
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            user.idUser = Convert.ToInt32(reader["idUser"]);
                            user.Name = reader["Name"].ToString();
                            //...
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
        //To Delete the record on a particular employee
        public bool DeleteUser(int id)
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
    }
}
