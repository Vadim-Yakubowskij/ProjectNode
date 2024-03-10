using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace node1
{
    /// <summary>
    /// Менеджер списков карт и провайдер данных
    /// Допустимо существование лишь одного экземпляра этого класса
    /// (зависимость от потока)
    /// </summary>
    public sealed class Manager
    {
        /// <summary>
        /// Название файла с данными
        /// </summary>
        public const string FileName = "data.xml";

        /// <summary>
        /// Название директории с данными
        /// </summary>
        public const string DirectoryName = "Node";

        /// <summary>
        /// Зарезервированный идентификатор закрепленного списка карт
        /// </summary>
        public const int EssentialIdA = 0;

        /// <summary>
        /// Зарезервированный идентификатор закрепленного списка карт
        /// </summary>
        public const int EssentialIdB = 1;

        /// <summary>
        /// Экземпляр класса одиночки
        /// </summary>
        private static Manager _instance = null;
        /// <summary>
        /// Глобальная точка доступа к классу
        /// Реализация шаблона Singleton
        /// </summary>
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Manager();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Полный путь до директории "Documents"
        /// </summary>
        private readonly string _source = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Списки карт
        /// </summary>
        private readonly SortedList<int, Tasklist> _list = new SortedList<int, Tasklist>();

        /// <summary>
        /// Закрепленные списки карт
        /// </summary>
        private readonly List<Tasklist> _listEssential = new List<Tasklist>
        {
            new Tasklist(EssentialIdA, "Задачи", true),
        };

        /// <summary>
        /// Конструктор менеджера
        /// Не может быть вызван извне
        /// </summary>
        private Manager()
        {
            LoadData();
        }

        /// <summary>
        /// Конструктор менеджера для отладки
        /// </summary>
        /// <param name="source">Путь до файла данных</param>
        /// <exception cref="ArgumentException"></exception>
        public Manager(string source)
        {
            if (source == _source)
            {
                throw new ArgumentException("Недопустимо использование источника по-умолчанию для отладки");
            }

            _source = source;
        }

        /// <summary>
        /// Доступ к файлу с данными
        /// </summary>
        public string Source => Path.Combine(SourceDirectory, FileName);

        /// <summary>
        /// Доступ к директории с данными
        /// </summary>
        public string SourceDirectory => Path.Combine(_source, DirectoryName);

        /// <summary>
        /// Доступ к спискам карт
        /// </summary>
        public SortedList<int, Tasklist> All => _list;

        /// <summary>
        /// Получить список карт по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор списка карт</param>
        /// <returns>Список карт</returns>
        public Tasklist GetList(int id)
        {
            return All[id];
        }

        /// <summary>
        /// Вставить список карт
        /// </summary>
        /// <param name="list">Список карт</param>
        public void SetList(Tasklist list)
        {
            All[list.PositionInWeek] = list;
        }

        /// <summary>
        /// Удалить список карт по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор списка карт</param>
        /// <returns>Статус успешности удаления</returns>
        public bool RemoveList(int id)
        {
            return All.Remove(id);
        }

        /// <summary>
        /// Проверить, существует ли список с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Статус проверки</returns>
        public bool ContainsList(int id)
        {
            return All.ContainsKey(id);
        }

        /// <summary>
        /// Сохранить данные
        /// </summary>
        public void SaveData()
        {
            CreateDataSource();


            List<TasklistData> data = new List<TasklistData>();

            foreach (KeyValuePair<int, Tasklist> item in All)
            {
                data.Add(item.Value.ToData());
            }

            WriteCardListData(data);
        }

        /// <summary>
        /// Создать файл для записи данных
        /// </summary>
        private void CreateDataSource()
        {
            Directory.CreateDirectory(SourceDirectory);

            if (File.Exists(Source))
            {
                return;
            }

            // Пустой файл
            File.Create(Source).Close();

            //TODO: Попробовать сделать фабрику для недели

            // Даже если сохранять нечего
            // В любом случае мы создадим корректную xml основу
            // И пустой файл превратится в читаемый программой источник
            //TODO: Создавать список дней недели без тасков - просто 7 тасклистов

            WriteCardListData(new List<TasklistData>() {
                new Tasklist(0,"Понидельник",true,new List<Task>(),0).ToData(),
                new Tasklist(1,"Вторник",true,new List<Task>(),1).ToData(),
                new Tasklist(2,"Среда",true,new List<Task>(),2).ToData(),
                new Tasklist(3,"Четверг",true,new List<Task>(),3).ToData(),
                new Tasklist(4,"Пятница",true,new List<Task>(),4).ToData(),
                new Tasklist(5,"Суббота",true,new List<Task>(),5).ToData(),
                new Tasklist(6,"Воскресенье",true,new List<Task>(),6).ToData(),
            });
        }

        /// <summary>
        /// Записать данные в файл
        /// </summary>
        /// <param name="data">Данные списков карт</param>
        private void WriteCardListData(List<TasklistData> data)
        {
            using (StreamWriter writer = new StreamWriter(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<TasklistData>));

                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Загрузить данные
        /// </summary>
        private void LoadData()
        {
            CreateDataSource();

            using (StreamReader reader = new StreamReader(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<TasklistData>));
                List<TasklistData> data = (List<TasklistData>)serializer.Deserialize(reader);

                // Очищаем списки карт перед загрузкой новых
                All.Clear();
                foreach (TasklistData item in data)
                {
                    SetList(Tasklist.FromData(item));
                }
            }

        }

    }
}
