using Cooper.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{
    public class StatisticsORM : IORM<Statistics>
    {
        //private string connectionString = "Put Your Connection string here";
        private OracleConnection Connection = DbConnecting.GetConnection();
        //static DbConnect connect = DbConnect.getInstance();
        //public OracleConnection Connection { get; set; } = connect.GetConnection();

        public long Add(Statistics statistics)
        {
            long insertId = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "insert into gamesstatistics (idGame, idUser, TimeSpent, RunsAmount, UserRecord) " +
                        "values(:idgame, :iduser, :time, :runs, :record) returning idStatistics into :id";
                    cmd.Parameters.Add("idgame", statistics.idGame);
                    cmd.Parameters.Add("iduser", statistics.idUser);
                    cmd.Parameters.Add("runs", statistics.TimeSpent);
                    cmd.Parameters.Add("runs", statistics.RunsAmount);
                    cmd.Parameters.Add("record", statistics.UserRecord);
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
                    cmd.CommandText = "delete from gamesstatistics where idStatistics = :id";
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

        public IEnumerable<Statistics> GetAll()
        {
            List<Statistics> lstStatistics = new List<Statistics>();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "select * from gamesstatistics";
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Statistics statistics = new Statistics
                        {
                            id = Convert.ToInt64(reader["idStatistics"]),
                            idGame = Convert.ToInt64(reader["idGame"]),
                            idUser = Convert.ToInt64(reader["idUser"]),
                            TimeSpent = Convert.ToDecimal(reader["TimeSpent"]),
                            RunsAmount = Convert.ToInt32(reader["RunsAmount"]),
                            UserRecord = Convert.ToInt32(reader["UserRecord"])
                        };
                        lstStatistics.Add(statistics);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return lstStatistics;
        }

        public Statistics GetData(long id)
        {
            Statistics statistics = new Statistics();
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "select * from gamesstatistics where idStatistics = :id";
                    cmd.Parameters.Add("id", id);
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        statistics.id = Convert.ToInt64(reader["idStatistics"]);
                        statistics.idGame = Convert.ToInt64(reader["idGame"]);
                        statistics.idUser = Convert.ToInt64(reader["idUser"]);
                        statistics.TimeSpent = Convert.ToDecimal(reader["TimeSpent"]);
                        statistics.RunsAmount = Convert.ToInt32(reader["RunsAmount"]);
                        statistics.UserRecord = Convert.ToInt32(reader["UserRecord"]);
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }
            return statistics;
        }


        public int Update(Statistics statistics)
        {
            int rowsUpdated = -1;
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    OracleCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "update gamesstatistics set TimeSpent = :time, RunsAmount = :runs, UserRecord = :record " +
                        "where idStatistics = :id";
                    cmd.Parameters.Add("time", statistics.TimeSpent);
                    cmd.Parameters.Add("runs", statistics.RunsAmount);
                    cmd.Parameters.Add("record", statistics.UserRecord);
                    cmd.Parameters.Add("id", statistics.id);
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

