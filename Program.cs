using System;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace KPO_laba4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Получение соединения...");
            MySqlConnection conn = DBUtils.GetDbConnection();

            // Создание схемы БД
            CreateShemaDB createShemaDb = new CreateShemaDB(conn, "timetable");
            createShemaDb.Create_Shema();

            // Подключение к схеме
            conn = DBUtils.GetDbConnection();
            createShemaDb.UpdateConnection(conn);

            // Создание таблиц
            createShemaDb.CreateTable();

            // Добавить значения в таблицу
            InsertDB insert = new InsertDB(conn);
            insert.Insert_cabinet("Д616");
            insert.Insert_teacher("Сергеева", "Ирина", "Альбертовна", 2, 18);
            insert.Insert_thing("Психология и педагогика", 4, 7, 7);
            insert.CloseConnection();

            // Изменение значений таблицы
            UpdateDB update = new UpdateDB(conn);
            update.Update_cabinet(4, "Д417");
            update.Update_teacher(4, "Фёдоров", "Вячеслав", "Викторович", 2, 18);
            update.Update_thing(4, "Надёжность программного обеспечения", 5, 4, 4);
            update.CloseConnection();

            // Запросы
            QueryDB query = new QueryDB(conn);
            Console.WriteLine("\nПреподаватели, работающие в заданный день недели в заданной аудитории:");
            query.Query_first("SELECT teacher.surname, teacher.name, teacher.midname FROM" +
            "((teacher INNER JOIN thing ON teacher.idTeacher = thing.idThing) INNER JOIN cabinet ON thing.idCabinet = cabinet.idCabinet)" +
            "INNER JOIN week ON week.idWeek = thing.idWeek WHERE nameCabinet = @cabinet AND week.nameDay = @day");
            Console.WriteLine("-------------------------");

            Console.WriteLine("\nПреподаватели, которые не ведут занятия в заданный день недели:");
            query.Query_second("SELECT teacher.surname, teacher.name, teacher.midname FROM" +
            "(teacher INNER JOIN thing ON teacher.idTeacher = thing.idTeacher) INNER JOIN week ON week.idWeek = thing.idWeek WHERE NOT week.nameDay = @day");
            Console.WriteLine("-------------------------");

            Console.WriteLine("\nДни недели, в которые ведутся заданное количество занятий:");
            query.Query_third("SELECT week.nameDay, Count(thing.idThing) AS \"Количество занятий\" FROM thing INNER JOIN week ON week.idWeek = thing.idWeek GROUP BY week.nameDay HAVING Count(thing.idThing) = @number", 2);
            Console.WriteLine("-------------------------");
            
            Console.WriteLine("\nДни недели, в которые занято заданное количество аудиторий:");
            query.Query_fourth("SELECT week.nameDay, Count(cabinet.idCabinet) AS \"Количество аудиторий\" FROM" +
            "(week INNER JOIN thing ON week.idWeek = thing.idWeek) INNER JOIN cabinet ON cabinet.idCabinet = thing.idCabinet GROUP BY week.nameDay HAVING Count(cabinet.idCabinet) = @number", 2);
            query.CloseConnection();

        }
    }
}
