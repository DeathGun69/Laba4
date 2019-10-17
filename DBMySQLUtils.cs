using System;
using MySql.Data.MySqlClient;

namespace KPO_laba4
{
    class DBMySQLUtils
    {

        public static MySqlConnection GetDbConnection(string host, int port, string database, string username, string password)
        {
            // строка подключения к БД
            String connStr = "Server = " + host + "; Database = " + database + "; port = " + port + "; User ID = " + username + "; password = " + password;

            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);

            return conn;
        }
    }
}