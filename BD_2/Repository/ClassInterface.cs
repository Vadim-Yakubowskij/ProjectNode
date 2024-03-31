using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace BD_2
{
    class ClassInterface : Interface
    {
        private const string ConnectionString = "Data Source=C:\\Users\\1\\Documents\\GitHub\\node1\\BD_2\\BD\\Tasklist.db; FailIfMissing=False";
        static SQLiteConnection connection;
        static SQLiteCommand command;
        void create()
        {
            try
            {
                connection = new SQLiteConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Connected!");
                command = new SQLiteCommand(connection)
                {
                    CommandText = "INSERT INTO \"tasks\" VALUES(NULL, \"сходить с федей в бар\", \"и пк\")"
                };
                Console.WriteLine("Добавлен новый элемент в тасклист:");
                DataTable data = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(data);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
            Console.Read();
        }
        void read()
        {
            try
            {
                connection = new SQLiteConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Connected!");
                command = new SQLiteCommand(connection)
                {
                    CommandText = "SELECT * FROM \"tasks\";"
                };
                Console.WriteLine("Результат запроса:");
                DataTable data = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(data);
                Console.WriteLine($"Прочитано {data.Rows.Count} записей из таблицы БД");
                foreach (DataRow row in data.Rows)
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
        void Update()
        {
            try
            {

                string sql = "INSERT INTO \"tasks\" VALUES(NULL, \"сходить с федей в бар\", \"и пк\")";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (SQLiteException ex)
            {
            }
        }
        void delete()
        {
            try
            {
                string sql = "DELETE FROM \"tasks\" WHERE \"ID\" = 21";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
            }
        }
    }
}

