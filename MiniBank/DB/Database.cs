using MySql.Data.MySqlClient;

namespace MiniBank.DB
{
    public class Database
    {
        public string ConnectionString { private get; set; }
        
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
