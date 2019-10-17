using System;
using MySql.Data.MySqlClient;

namespace KPO_laba4
{
    class DBUtils
    {
        public static MySqlConnection GetDbConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "timetable";
            string username = "root";
            string password = "root";

            return DBMySQLUtils.GetDbConnection(host, port, database, username, password);
        }
    }
}