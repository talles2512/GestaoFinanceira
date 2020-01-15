using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace MyFinance.Util
{
    public class DAL
    {
        private string connectionString = "Server=localhost;Database=bdgesfin;Uid=root;Pwd=;";
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public DataTable Reader(string query)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }

        public int NoQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            int rows = command.ExecuteNonQuery();
            return rows;

        }
    }
}
