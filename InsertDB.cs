using System;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace KPO_laba4
{
    class InsertDB: IDB
    {
        MySqlConnection connection;

        public InsertDB(MySqlConnection conn)
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

        public void Insert_cabinet(string nameCabinet)
        {
            MySqlCommand cmd = new MySqlCommand();
            string query = "INSERT INTO `cabinet` (`nameCabinet`) VALUES (@nameCabinet)";
            cmd.Connection = connection;
            cmd.CommandText = query;
            MySqlParameter nameCabinet_param = new MySqlParameter("@nameCabinet", MySqlDbType.VarChar);
            nameCabinet_param.Value = nameCabinet;
            cmd.Parameters.Add(nameCabinet_param);

            try {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                connection.Close();
            }
        }

        public void Insert_teacher(string surname, string name, string midname, int numPairs, int numStud)
        {
            MySqlCommand cmd = new MySqlCommand();
            string query = "INSERT INTO `teacher` (`surname`, `name`, `midname`, `numPairs`, `numStud`) VALUES (@surname, @name, @midname, @numPairs, @numStud)";
            cmd.Connection = connection;
            cmd.CommandText = query;
            MySqlParameter surname_param = new MySqlParameter("@surname", MySqlDbType.VarChar);
            surname_param.Value = surname;
            cmd.Parameters.Add(surname_param);
            MySqlParameter name_param = new MySqlParameter("@name", MySqlDbType.VarChar);
            name_param.Value = name;
            cmd.Parameters.Add(name_param);
            MySqlParameter midname_param = new MySqlParameter("@midname", MySqlDbType.VarChar);
            midname_param.Value = midname;
            cmd.Parameters.Add(midname_param);
            MySqlParameter numPairs_param = new MySqlParameter("@numPairs", MySqlDbType.Int32);
            numPairs_param.Value = numPairs;
            cmd.Parameters.Add(numPairs_param);
            MySqlParameter numStud_param = new MySqlParameter("@numStud", MySqlDbType.Int32);
            numStud_param.Value = numStud;
            cmd.Parameters.Add(numStud_param);

            try {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                connection.Close();
            }
        }

        public void Insert_thing(string nameThing, int idWeek, int idCabinet, int idTeacher)
        {
            MySqlCommand cmd = new MySqlCommand();
            string query = "INSERT INTO `thing` (`nameThing`, `idWeek`, `idCabinet`, `idTeacher`) VALUES (@nameThing, @idWeek, @idCabinet, @idTeacher)";
            cmd.Connection = connection;
            cmd.CommandText = query;
            MySqlParameter nameThing_param = new MySqlParameter("@nameThing", MySqlDbType.VarChar);
            nameThing_param.Value = nameThing;
            cmd.Parameters.Add(nameThing_param);
            MySqlParameter idWeek_param = new MySqlParameter("@idWeek", MySqlDbType.Int32);
            idWeek_param.Value = idWeek;
            cmd.Parameters.Add(idWeek_param);
            MySqlParameter idCabinet_param = new MySqlParameter("@idCabinet", MySqlDbType.Int32);
            idCabinet_param.Value = idCabinet;
            cmd.Parameters.Add(idCabinet_param);
            MySqlParameter idTeacher_param = new MySqlParameter("@idTeacher", MySqlDbType.Int32);
            idTeacher_param.Value = idTeacher;
            cmd.Parameters.Add(idTeacher_param);

            try {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                connection.Close();
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}