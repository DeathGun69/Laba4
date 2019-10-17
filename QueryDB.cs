using System;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace KPO_laba4
{
    class QueryDB: IDB
    {
        MySqlConnection connection;

        public QueryDB(MySqlConnection conn)
        {
            connection = conn;

            try {
                // Подключение к БД
                Console.WriteLine("Открывающееся соединение...");
                conn.Open(); 
                Console.WriteLine("Соединение успешно!");
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
                conn.Close();
                conn.Dispose();
            }
        }

        /* Вывести информацию о преподавателях, 
        работающих в заданный день недели в заданной аудитории. */
        public void Query_first(string query)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            cmd.Connection = connection;
            cmd.CommandText = query;

            MySqlParameter cabinet = new MySqlParameter("@cabinet", MySqlDbType.VarChar);
            cabinet.Value = "А509";
            cmd.Parameters.Add(cabinet);
            MySqlParameter day = new MySqlParameter("@day", MySqlDbType.VarChar);
            day.Value = "Пятница";
            cmd.Parameters.Add(day);

            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int surN_int = reader.GetOrdinal("surname");
                        int name_int = reader.GetOrdinal("name");
                        int midN_int = reader.GetOrdinal("midname");

                        string surName = reader.GetString(surN_int);
                        string name = reader.GetString(name_int);
                        string midName = reader.GetString(midN_int);

                        Console.WriteLine("\nФамилия: " + surName);
                        Console.WriteLine("Имя: " + name);
                        Console.WriteLine("Отчество: " + midName);
                    }
                }
            }
        }

        /* Вывести информацию о преподавателях, 
        которые не ведут занятия в заданный день недели. */
        public void Query_second(string query)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            cmd.Connection = connection;
            cmd.CommandText = query;

            MySqlParameter day = new MySqlParameter("@day", MySqlDbType.VarChar);
            day.Value = "Четверг";
            cmd.Parameters.Add(day);

            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int surN_int = reader.GetOrdinal("surname");
                        int name_int = reader.GetOrdinal("name");
                        int midN_int = reader.GetOrdinal("midname");

                        string surName = reader.GetString(surN_int);
                        string name = reader.GetString(name_int);
                        string tename3 = reader.GetString(midN_int);

                        Console.WriteLine("\nФамилия: " + surName);
                        Console.WriteLine("Имя: " + name);
                        Console.WriteLine("Отчество: " + tename3);
                    }
                }
            }
        }

        /* Вывести дни недели, 
        в которых проводится заданное количество занятий. */
        public void Query_third(string query, int number)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            cmd.Connection = connection;
            cmd.CommandText = query;

            MySqlParameter number_param = new MySqlParameter("@number", MySqlDbType.Int32);
            number_param.Value = number;
            cmd.Parameters.Add(number_param);

            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int nDayID = reader.GetOrdinal("nameDay");
                        int numID = reader.GetOrdinal("Количество занятий");

                        string nameDay = reader.GetString(nDayID);
                        string numberID = reader.GetString(numID);

                        Console.WriteLine("\nДень недели: " + nameDay);
                        Console.WriteLine("Количество занятий: " + numberID);
                    }
                }
            }
        }

        /* Вывести дни недели, 
        в которых занято заданное количество аудиторий. */
        public void Query_fourth(string query, int number)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            cmd.Connection = connection;
            cmd.CommandText = query;

            MySqlParameter number_param = new MySqlParameter("@number", MySqlDbType.Int32);
            number_param.Value = number;
            cmd.Parameters.Add(number_param);

            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int nDayID = reader.GetOrdinal("nameDay");
                        int numID = reader.GetOrdinal("Количество аудиторий");

                        string nameDay = reader.GetString(nDayID);
                        string numberID = reader.GetString(numID);

                        Console.WriteLine("\nДень недели: " + nameDay);
                        Console.WriteLine("Количество аудиторий: " + numberID);
                    }
                }
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}