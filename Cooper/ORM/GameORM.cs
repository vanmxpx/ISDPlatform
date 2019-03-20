using Cooper.Models;
using Cooper.ORM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.ORM
{
    public class GameORM : IORM <Game>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connect = DbConnecting.GetConnection();

        public bool AddEntity(Game game)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        //connect.Open();
                        cmd.CommandText = "insert into games (idGame, Name) values(:id, :name)";
                        cmd.Parameters.Add("id", game.id);
                        cmd.Parameters.Add("name", game.Name);
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
                        cmd.CommandText = "delete from games where idGame = :id";
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

        public IEnumerable<Game> GetAllEntities()
        {
            try
            {
                List<Game> lstGame = new List<Game>();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from games";
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Game game = new Game();
                            game.id = Convert.ToInt32(reader["idGame"]);
                            game.Name = reader["Name"].ToString();
                            //...
                            lstGame.Add(game);
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                }
                return lstGame;
            }
            catch
            {
                throw;
            }
        }

        public Game GetEntityData(long id)
        {
            try
            {
                Game game = new Game();
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        connect.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from games where idGame = :id";
                        cmd.Parameters.Add("id", game.id);
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            game.id = Convert.ToInt32(reader["idGame"]);
                            game.Name = reader["Name"].ToString();
                            //...
                        }
                        reader.Dispose();
                        connect.Close();
                    }
                    return game;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateEntity(Game game)
        {
            int rowsUpdated = 0;
            try
            {
                using (connect)
                {
                    using (OracleCommand cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = "update games set Name = :name where idGame = :id";
                        cmd.Parameters.Add("id", game.id);
                        cmd.Parameters.Add("name", game.Name);
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
