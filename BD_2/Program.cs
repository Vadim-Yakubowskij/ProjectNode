using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BD_2
{
    internal class Program
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;
        static SQLiteCommand commandCreate;

        static void Main(string[] args)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=C:\\Users\\1\\Documents\\GitHub\\node1\\BD_2\\BD\\Tasklist.db; FailIfMissing=False");
                connection.Open();
                Console.WriteLine("Connected!");
                /*commandCreate = new SQLiteCommand(connection)
                {
                    CommandText = "INSERT INTO \"tasks\" VALUES(NULL, \"дописать базу данных\", \"CRUD\")"
                };*/
                command = new SQLiteCommand(connection)
                {
                    CommandText = "SELECT * FROM \"tasks\";"
                };
                Console.WriteLine("Выполнено добавление нового элемента");
                DataTable data = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(commandCreate);
                //adapter.Fill(data);
                Console.WriteLine("Результат запроса:");
                DataTable data2 = new DataTable();
                SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(command);
                adapter2.Fill(data2);
                Console.WriteLine($"Прочитано {data2.Rows.Count} записей из таблицы БД");
                foreach (DataRow row in data2.Rows)
                {
                    Console.WriteLine($"id = {row.Field<Int64>("id")} name = {row.Field<string>("name")} more details = {row.Field<string>("more details")}");
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
            Console.Read();
        }
    }
}