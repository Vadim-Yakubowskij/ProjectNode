﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace BD
{
    class Repository
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;
        static void Create()
        {
            try
            {
                connection = new SQLiteConnection("Data Source=C:\\Users\\1\\Documents\\GitHub\\BD\\BD\\Tasklist.db; FailIfMissing=False");
                connection.Open();
                Console.WriteLine("Connected!");
                command = new SQLiteCommand(connection)
                {
                    CommandText = "INSERT INTO \"tasks\" VALUES(NULL, \"дописать базу данных\", \"CRUD\")"
                };
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
            catch(SQLiteException ex)
            {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
            Console.Read();
        }
        static void Read()
        {
            try
            {
                connection = new SQLiteConnection("Data Source=C:\\Users\\1\\Documents\\GitHub\\BD\\BD\\Tasklist.db; FailIfMissing=False");
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
        static void Update()
        {
            try
            {
                connection = new SQLiteConnection("Data Source=C:\\Users\\1\\Documents\\GitHub\\BD\\BD\\Tasklist.db; FailIfMissing=False");
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
        static void Delite()
        {
            try
            {
                connection = new SQLiteConnection("Data Source=C:\\Users\\1\\Documents\\GitHub\\BD\\BD\\Tasklist.db; FailIfMissing=False");
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










    }
}
