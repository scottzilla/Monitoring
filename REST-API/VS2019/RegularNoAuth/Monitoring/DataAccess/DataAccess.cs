using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitoring.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
/* https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlclient.sqlcommand?view=sqlclient-dotnet-core-1.1 */

/* We may end up going with EF Core however for now let's get this working with SqlCommand objects (perhaps Dapper ):
https://www.iamtimcorey.com/blog/137806/entity-framework */

namespace Monitoring.Persistance
{
    public static class SqlCommandUtility
    {

        private static readonly string _tableName = "[dbo].Monitoring";
        private static readonly string _noStringEntry = "N/A";

        public static List<MonitorRecord> ExecuteDataReader(string query,IConfiguration config)
        {
            List<MonitorRecord> monitorRecords = new List<MonitorRecord>();
            
            using(SqlCommand command = new SqlCommand(query, GetSqlConnection(config)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    MonitorRecord monitorRecord = new MonitorRecord();
                    monitorRecord.ID = reader.GetInt32(0);
                    monitorRecord.AppID = reader.GetInt32(1);
                    monitorRecord.AppArea = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    monitorRecord.Information = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    monitorRecord.Comments = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    monitorRecord.Error = reader.GetBoolean(5);
                    monitorRecord.ModifiedDate = reader.GetDateTime(6);

                    monitorRecords.Add(monitorRecord);
                }
            }

            return monitorRecords;

        }

        public static List<MonitorRecord> GetEntriesByAppId(int Id,IConfiguration config)
        {
            List<MonitorRecord> monitorRecords = new List<MonitorRecord>();
            string query = $"select * from {_tableName} where appId={Id}";

            return ExecuteDataReader(query,config);
        }

        public static List<MonitorRecord> GetAll(IConfiguration config)
        {
            string commandText = $"select * from {_tableName}";
            return ExecuteDataReader(commandText,config);

        }

        public static void AddMonitorRecordEntry(MonitorRecord monitorRecord,IConfiguration config)
        {
            string commandText = $"INSERT INTO [{_tableName}] VALUES (@AppId, @AppArea, @Information,@Comments,@Error,getdate())";

            using(SqlCommand command = new SqlCommand(commandText, GetSqlConnection(config)))
            {
                command.Parameters.AddWithValue("@AppId",monitorRecord.AppID);
                command.Parameters.AddWithValue("@AppArea", PrepareInputString(monitorRecord.AppArea));
                command.Parameters.AddWithValue("@Information", PrepareInputString(monitorRecord.Information));
                command.Parameters.AddWithValue("@Comments", PrepareInputString(monitorRecord.Comments));
                command.Parameters.AddWithValue("@Error",monitorRecord.Error);
                command.Connection.Open();

                command.ExecuteNonQuery();
            }

        }

        public static int DeleteMonitorRecordsBasedOnAppId(int Id,IConfiguration config)
        {
            string commandText = $"delete from {_tableName} where id={Id}";
            return ExecuteNonQuery(commandText,config);
        }

        public static int ExecuteNonQuery(string commandText,IConfiguration config)
        {
            using(SqlConnection conn = GetSqlConnection(config))
            {
                using(SqlCommand com = new SqlCommand(commandText, conn))
                {
                    com.Connection.Open();
                    return com.ExecuteNonQuery();
                }
            }
        }

        public static SqlConnection GetSqlConnection(IConfiguration config)
        {
            string connectionString=config.GetConnectionString("DefaultConnection");            
            return new SqlConnection(connectionString);
        }

        public static string PrepareInputString(string input)
        {
            
            return string.IsNullOrWhiteSpace(input) ? _noStringEntry : input;
        }

    }
}
