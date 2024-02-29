using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace node1
{
    class Manager
    {
        private readonly string _source = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public const string FileName = "data.xml";
        public const string DirectoryName = "Timelon";
        public string Source => Path.Combine(SourceDirectory, FileName);
        public string SourceDirectory => Path.Combine(_source, DirectoryName);

        private readonly SortedList<int, Tasklist> _list = new SortedList<int, Tasklist>();
        public SortedList<int, Tasklist > All => _list;

        private static Manager _instance = null;
        public void SetList(Tasklist list)
        {
            All[list.Id] = list;
        }
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
        private Manager()
        {
            LoadData();
        }
        private void WriteTasklist(List<Tasklist> data)
        {
            using (StreamWriter writer = new StreamWriter(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Tasklist>));

                serializer.Serialize(writer, data);
            }
        }
        private void CreateDataSource()
        {
            Directory.CreateDirectory(SourceDirectory);

            if (File.Exists(Source))
            {
                return;
            }


            File.Create(Source).Close();


            WriteTasklist(new List<Tasklist>());
        }

        private void LoadData()
        {
            CreateDataSource();

            using (StreamReader reader = new StreamReader(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Tasklist>));
                List<Tasklist> data = (List<Tasklist>)serializer.Deserialize(reader);

                // Очищаем списки карт перед загрузкой новых
                All.Clear();

                foreach (Tasklist item in data)
                {
                    SetList(item);
                }
            }
        }

        public void SaveData()
        {
            CreateDataSource();


            List<Tasklist> data = new List<Tasklist>();

            foreach (KeyValuePair<int, Tasklist> item in All)
            {
                data.Add(item.Value);
            }

            WriteCardList(data);
        }
        private void WriteCardList(List<Tasklist> data)
        {
            using (StreamWriter writer = new StreamWriter(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Tasklist>));

                serializer.Serialize(writer, data);
            }
        }
    }
}
