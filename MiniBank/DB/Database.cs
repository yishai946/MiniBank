using MySql.Data.MySqlClient;
using System;

namespace MiniBank.DB
{
    public class Database
    {
        private readonly string connectionString;

        public Database()
        {
            var server = Environment.GetEnvironmentVariable(Strings.ServerVarName);
            var db = Environment.GetEnvironmentVariable(Strings.DBVarName);
            var uid = Environment.GetEnvironmentVariable(Strings.UIDVarName);
            var pwd = Environment.GetEnvironmentVariable(Strings.PWDVarName);

            connectionString = string.Format(connectionString, server, db, uid, pwd);
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
