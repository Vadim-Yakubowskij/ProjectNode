using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Policy;
using System.Configuration;

namespace BD_2
{
    class ClassInterface : Interface
    {
        private string ConnectionString = "Data Source = C:\\Users\\Вадим\\OneDrive\\Документы\\GitHub\\ProjectNode\\BD_2\\DB\\Tasklist.db";
        public ClassInterface()
        {
            string relativePath = @"BD\Tasklist.db";
            string parentDir = Path.GetDirectoryName(AppContext.BaseDirectory);
            string tmp = parentDir.Remove(parentDir.Length - 16, 16);
            string absolutePath = Path.Combine(tmp, relativePath);
            ConnectionString = string.Format("Data Source = {0};Version=3; FailIfMissing=False", absolutePath,true);
        }
        
        public void create(string name, string more)
        {
            try
            {
                string sql = $"INSERT INTO \"tasks\" (date_time,name,more_details,isCompelited) VALUES (date('now'), \"{name}\", \"{more}\",\"не выполнено\")";
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
        public void read()
        {
            try
            {
                string sql = "SELECT * FROM \"tasks\"";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                int index = 0;
                                int id = rdr.GetInt32(index++);
                                string date_time = rdr.GetString(index++);
                                string name = rdr.GetString(index++);
                                string more_details;
                                if (!rdr.IsDBNull(index))
                                {
                                    more_details = rdr.GetString(index++);
                                }
                                else
                                {
                                    more_details = String.Empty;
                                }

                                Console.WriteLine("{0} \t{1} \t{2} \t{3}", id, date_time, name, more_details);
                            }
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
        public void Update(int id, string name)
        {
            try
            {

                string sql = $"UPDATE \"tasks\"SET name = \"{name}\"WHERE id = {id};";
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
        public void delete(int id)
        {
            try
            {
                string sql = $"DELETE FROM \"tasks\" WHERE \"ID\" = {id}";
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

