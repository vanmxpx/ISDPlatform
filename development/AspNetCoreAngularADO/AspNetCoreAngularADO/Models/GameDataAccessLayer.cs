using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AspNetCoreAngularADO.Models
{
    public class GameDataAccessLayer
    {
        private readonly string connectionString = "User Id=system;Password=QAZse4321;" + "Data Source=localhost:1521/xe;" +
        "Data Source=<ip or hostname>:1521/<service name>;";

        //To View all Games details
        public IEnumerable<Game> GetAllGames()
        {
            List<Game> lstGame = new List<Game>();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display game names from 
                        // the GameS table
                        cmd.CommandText = "select name from games idgame = :id";

                        // Assign id to the game number 2 
                        OracleParameter id = new OracleParameter("id", 2);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Game Game = new Game();
                            Game.idGame = Convert.ToInt32(reader["idGame"]);
                            Game.Name = reader["Name"].ToString();
                            lstGame.Add(Game);
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return lstGame;
        }
    }
}
