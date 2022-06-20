//task1.cs
//first modification
//final modification : branch exam created

using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SQLiteDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            string key = "1";
            do
            {
                Console.WriteLine("\nPress enter to continue. ");
                Console.ReadLine();
                Console.Clear();
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();
                CreateTable(sqlite_conn);
                Console.WriteLine("\n--------------------------------------------- ");

                Console.WriteLine("Select option: ");
                Console.WriteLine("              1 -> display all data with average ");
                Console.WriteLine("              2 -> insert data ");
                Console.WriteLine("              0 -> exit ");
                key = Console.ReadLine();

                if (key == "1")
                {
                    ReadData(sqlite_conn);

                }
                
                else if (key == "2")
                {
                    InsertData(sqlite_conn);
                    
                }
                else if (key == "0")
                {
                    Console.WriteLine("Exiting ...");
                }

                
            } while (key != "0");



        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=databaseTemperatures.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database no found");
            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            string strTemperature = "CREATE TABLE if not exists  Temperature(id INTEGER PRIMARY KEY AUTOINCREMENT, date VARCHAR(20), avgtemp DOUBLE)";
           
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = strTemperature;
            sqlite_cmd.ExecuteNonQuery();

            

        }

        static void InsertData(SQLiteConnection conn)
        {
            Console.Write("Enter the date of temperature: ");
            string date = Console.ReadLine();
            date = date.ToUpper();

            Console.Write("Enter the temperature: ");
            string temperature = Console.ReadLine();
            
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Temperature(date, avgtemp) VALUES('" + date + "','" + temperature + "' ); ";
            
            sqlite_cmd.ExecuteNonQuery();
            //------------------------
            SQLiteDataReader sqlite_datareader;
           
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Temperature ";
            int temperature_id = 0;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                temperature_id = (int)sqlite_datareader.GetInt64(0);
            }
            

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT temperature.id, temperature.date, temperature.avgtemp FROM Temperature  ";
            Double avg = 0;
            int count = 0;

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            Console.WriteLine("{0, -3} {1,12} {2,12} ", "ID", "DATE", "TEMPERATURE");
            Console.WriteLine("---------------------------------------------");
            string dateLowestTemp = "";
            Double lowestCelsious = 0;
           
            while (sqlite_datareader.Read())
            {
                int col0 = (int)sqlite_datareader.GetInt32(0);
                string col1 = sqlite_datareader.GetString(1);
                col1 = col1.ToUpper();
                double col2 = sqlite_datareader.GetDouble(2);
                
                avg += col2;
                count += 1;
                

                if(lowestCelsious > col2)
                {
                    lowestCelsious = col2;
                    dateLowestTemp = col1;
                }
                else
                {
                    lowestCelsious = col2;
                    dateLowestTemp = col1;
                }

                Console.WriteLine("{0, -3} {1,12} {2,12} ", col0, col1, col2);
               
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("AVERAGE     ------>       {0,10}", avg / count);
            Console.WriteLine("LOWEST TEEMPERATURE  ------>       {0,10} -- {1,13}", lowestCelsious, dateLowestTemp);
            conn.Close();
           
        }
        
    }
}