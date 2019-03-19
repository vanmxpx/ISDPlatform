using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Cooper.DAO
{
    public class GameDataAccessLayer
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection connect = DbConnecting.GetConnection();
        //To View all games details
        public IEnumerable<Game> GetAllGames()
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
        //To Add new game record 
        public bool AddGame(Game game)
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
        //To Update the records of a particluar game
        public bool UpdateGame(Game game)
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
        //Get the details of a particular game
        public Game GetGameData(int id)
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
        //To Delete the record on a particular employee
        public bool DeleteGame(int id)
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
    }
}
