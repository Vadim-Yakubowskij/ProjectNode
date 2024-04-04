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
        void create()
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
        void read()
        {
            try
            {
                string sql = "INSERT INTO \"tasks\" VALUES(NULL, \"сходить с федей в бар\", \"и пк\")";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            object id = rdr.GetValue(0);
                            object name = rdr.GetValue(1);
                            object more_details = rdr.GetValue(2);

                            Console.WriteLine("{0} \t{1} \t{2}", id, name, more_details);
                        }
                    }
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

                string sql = "UPDATE \"tasks\"SET name = \"сходить в зал\"WHERE id = 23;";
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

