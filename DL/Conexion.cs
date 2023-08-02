using System;
using System.Data;
using System.Data.SQLite;

namespace DL
{
    public class Conexion
    {

        public static string GetConnection()
        {
            SQLiteConnectionStringBuilder connection = new SQLiteConnectionStringBuilder
            {
                DataSource = "/Users/macbookmba12/Documents/ProgramacionNCapas.db",
                Version = 3
            };
            return connection.ToString();
        }

    }
}

