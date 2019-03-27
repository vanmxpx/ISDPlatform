using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{
    public class GameORM : IORM<Game>
    {
        //private string ConnectionString = "Put Your Connection string here";
        private OracleConnection Connection = DbConnecting.GetConnection();
        //private static DbConnecting connect = DbConnecting.getInstance();
        //public OracleConnection Connection { get; set; } = connect.GetConnection();

        public long Add(Game game)
        {
            long insertId = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "insert into games (Name, ... ) values(:name, ...) returning idGame into :id";
                    cmd.Parameters.Add("name", game.Name);
                    //...
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
                    cmd.CommandText = "delete from games where idGame = :id";
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

        public IEnumerable<Game> GetAll()
        {
            List<Game> lstGame = new List<Game>();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from games";
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Game game = new Game
                        {
                            id = Convert.ToInt64(reader["idGame"]),
                            Name = reader["Name"].ToString()
                        };
                        lstGame.Add(game);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
                //DbConnecting.CloseConnection();
            }
            return lstGame;
        }

        public Game GetData(long id)
        {
            Game game = new Game();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from games where idGame = :id";
                    cmd.Parameters.Add("id", id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        game.id = Convert.ToInt64(reader["idGame"]);
                        game.Name = reader["Name"].ToString();
                        //...
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return game;
        }

        public int Update(Game game)
        {
            int rowsUpdated = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "update games set Name = :name, ... where idGame = :id";
                    cmd.Parameters.Add("id", game.id);
                    cmd.Parameters.Add("name", game.Name);
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

