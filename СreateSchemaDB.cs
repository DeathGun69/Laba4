using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace KPO_laba4
{
    class CreateShemaDB: IDB
    {
        MySqlConnection connection;
        string nameSchema;
        
        public CreateShemaDB(MySqlConnection conn, string shema)
        {
            nameSchema = shema;
            connection = conn;

            try 
            {
                // Подключение к БД
                Console.WriteLine("Открывающееся соединение...");
                conn.Open(); 
                Console.WriteLine("Соединение успешно!");

            } catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
                conn.Close();
                conn.Dispose();
            }
        }

        // Обновить подключение к БД
        public void UpdateConnection(MySqlConnection conn)
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

        // Создание схемы
        public void Create_Shema()
        {
            // Проверить существует ли схема, если нет то создать
            string query = "CREATE SCHEMA IF NOT EXISTS "+ nameSchema + " DEFAULT CHARACTER SET utf8;";
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = query;

            try {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                connection.Close();
            }
        }

        // Создание таблиц
        public void CreateTable()
        {
            string query = "/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;" +
                            "/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;" +
                            "/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;" +
                            "/*!50503 SET NAMES utf8 */;" +
                            "/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;" +
                            "/*!40103 SET TIME_ZONE='+00:00' */;" +
                            "/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;" +
                            "/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;" +
                            "/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;" +
                            "/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;" +

                            "/*!40101 SET @saved_cs_client     = @@character_set_client */;" +
                            "/*!50503 SET character_set_client = utf8mb4 */;" +
                            "CREATE TABLE IF NOT EXISTS `cabinet` (" +
                            "`idCabinet` int(11) NOT NULL AUTO_INCREMENT," +
                            "`nameCabinet` varchar(10) DEFAULT NULL," +
                            "PRIMARY KEY (`idCabinet`)" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;" +
                            "/*!40101 SET character_set_client = @saved_cs_client */;" +

                            "LOCK TABLES `cabinet` WRITE;" +
                            "/*!40000 ALTER TABLE `cabinet` DISABLE KEYS */;" +
                            "INSERT IGNORE INTO `cabinet` VALUES (1,'Д514'),(2,'Д523'),(3,'Д216'),(4,'Д618'),(5,'Д507'),(6,'А509');" +
                            "/*!40000 ALTER TABLE `cabinet` ENABLE KEYS */;" +
                            "UNLOCK TABLES;" +

                            "/*!40101 SET @saved_cs_client     = @@character_set_client */;" +
                            "/*!50503 SET character_set_client = utf8mb4 */;" +
                            "CREATE TABLE IF NOT EXISTS `teacher` (" +
                            "`idTeacher` int(11) NOT NULL AUTO_INCREMENT," +
                            "`surname` varchar(45) DEFAULT NULL," +
                            "`name` varchar(45) DEFAULT NULL," +
                            "`midname` varchar(45) DEFAULT NULL," +
                            "`numPairs` int(11) DEFAULT NULL," +
                            "`numStud` int(11) DEFAULT NULL," +
                            "PRIMARY KEY (`idTeacher`)" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;" +
                            "/*!40101 SET character_set_client = @saved_cs_client */;" +

                            "LOCK TABLES `teacher` WRITE;" +
                            "/*!40000 ALTER TABLE `teacher` DISABLE KEYS */;" +
                            "INSERT IGNORE INTO `teacher` VALUES (1,'Кашковский','Виктор','Владимирович',3,18),(2,'Краковский','Юрий','Мечеславович',3,18),(3,'Лучников','Владимир','Александрович',2,18),(4,'Кириллова','Татьяна','Климентьевна',2,18),(5,'Курганская','Ольга','Викторовна',2,18),(6,'Ермаков','Анатолий','Анатольевич',4,18);" +
                            "/*!40000 ALTER TABLE `teacher` ENABLE KEYS */;" +
                            "UNLOCK TABLES;" +

                            "/*!40101 SET @saved_cs_client     = @@character_set_client */;" +
                            "/*!50503 SET character_set_client = utf8mb4 */;" +
                            "CREATE TABLE IF NOT EXISTS `thing` (" +
                            "`idThing` int(11) NOT NULL AUTO_INCREMENT," +
                            "`nameThing` varchar(45) DEFAULT NULL," +
                            "`idWeek` int(11) DEFAULT NULL," +
                            "`idCabinet` int(11) DEFAULT NULL," +
                            "`idTeacher` int(11) DEFAULT NULL," +
                            "PRIMARY KEY (`idThing`)," +
                            "KEY `idTeacher_idx` (`idTeacher`)," +
                            "KEY `idCabinet_idx` (`idCabinet`)," +
                            "KEY `idWeek_idx` (`idWeek`)," +
                            "CONSTRAINT `idCabinet` FOREIGN KEY (`idCabinet`) REFERENCES `cabinet` (`idCabinet`)," +
                            "CONSTRAINT `idTeacher` FOREIGN KEY (`idTeacher`) REFERENCES `teacher` (`idTeacher`)," +
                            "CONSTRAINT `idWeek` FOREIGN KEY (`idWeek`) REFERENCES `week` (`idWeek`)" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;" +
                            "/*!40101 SET character_set_client = @saved_cs_client */;" +

                            "LOCK TABLES `thing` WRITE;" +
                            "/*!40000 ALTER TABLE `thing` DISABLE KEYS */;" +
                            "INSERT IGNORE INTO `thing` VALUES (1,'Архитектура ВС',2,1,1),(2,'Разработка защищенных ПС',2,2,2),(3,'Тестирование ПО',3,3,3),(4,'Основы управленческой деятельности',4,4,4),(5,'Конструирование ПО',6,5,5),(6,'Теория принятия решений',5,6,6);" +
                            "/*!40000 ALTER TABLE `thing` ENABLE KEYS */;" +
                            "UNLOCK TABLES;" +

                            "/*!40101 SET @saved_cs_client     = @@character_set_client */;" +
                            "/*!50503 SET character_set_client = utf8mb4 */;" +
                            "CREATE TABLE IF NOT EXISTS `week` (" +
                            "`idWeek` int(11) NOT NULL AUTO_INCREMENT," +
                            "`nameDay` varchar(20) DEFAULT NULL," +
                            "PRIMARY KEY (`idWeek`)" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;" +
                            "/*!40101 SET character_set_client = @saved_cs_client */;" +

                            "LOCK TABLES `week` WRITE;" +
                            "/*!40000 ALTER TABLE `week` DISABLE KEYS */;" +
                            "INSERT IGNORE INTO `week` VALUES (1,'Понедельник'),(2,'Вторник'),(3,'Среда'),(4,'Четверг'),(5,'Пятница'),(6,'Суббота');" +
                            "/*!40000 ALTER TABLE `week` ENABLE KEYS */;" +
                            "UNLOCK TABLES;" +
                            "/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;" +

                            "/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;" +
                            "/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;" +
                            "/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;" +
                            "/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;" +
                            "/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;" +
                            "/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;" +
                            "/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;";
            
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = query;

            try {
                cmd.ExecuteNonQuery();
                connection.Close();
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